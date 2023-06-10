using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CFinder.Application.Models.AddressCheckerModels;
using CFinder.Application.Models.Result;
using CFinder.Application.Models.Settings;
using CFinder.Application.ServerMethods.Base;
using CFinder.Domain.Settings;

namespace CFinder.Application.ServerMethods.Override;

public class AnkrAddressCheckBalanceServerMethodOverride : AddressCheckBalanceServerMethod
{
    private const string ENDPOINT = "https://rpc.ankr.com/multichain/";
    private const string AUTH_KEY = "2bd798e6459d20a3b5b034b93a72f6a21d7a960279a1138e8477ec6fb2fce9f0";

    /// <summary>
    /// Check AddressInfoDto Addresses
    /// </summary>
    /// <param name="addressInfoDtos">IEnumerable AddressInfoDto obj</param>
    /// <param name="checkerSettingsDto">BalanceCheckerSettingsDto obj</param>
    public async Task CheckAllAddresses(IEnumerable<AddressInfoDto> addressInfoDtos, BalanceCheckerSettingsDto checkerSettingsDto)
    {
        foreach (var addressInfoDto in addressInfoDtos)
        {
            var checkReuslt = await CheckTokenNFT(addressInfoDto.Address, checkerSettingsDto);

            addressInfoDto.Tokens = checkReuslt.Tokens;
            addressInfoDto.Nfts = checkReuslt.NFTs;
        }
    }

    /// <summary>
    /// All check
    /// </summary>
    /// <param name="address">address ethereum (0x...)</param>
    /// <param name="checkerSettingsDto">BalanceCheckerSettingsDto obj</param>
    /// <returns>TupleBalances obj</returns>
    public override async Task<TupleBalances> CheckTokenNFT(string? address, BalanceCheckerSettingsDto checkerSettingsDto)
    {
        var checkType = checkerSettingsDto.CheckCrypto;
        var balancesTyple = new TupleBalances();
        if (string.IsNullOrWhiteSpace(address))
            return balancesTyple;
        

        if (checkType == CheckCryptoMode.AllCheck || checkType == CheckCryptoMode.CheckOnlyTokens)
        {
            balancesTyple.Tokens = await CheckToken(address, checkerSettingsDto.OnlyWhiteList);

            await Task.Delay(checkerSettingsDto.DelayBeforeRequest);
        }

        if (checkType == CheckCryptoMode.AllCheck || checkType == CheckCryptoMode.CheckOnlyNfts)
        {
            balancesTyple.NFTs = await CheckNFT(address);
            
            await Task.Delay(checkerSettingsDto.DelayBeforeRequest);
        }

        return balancesTyple;
    }

    /// <summary>
    /// Check address token
    /// </summary>
    /// <param name="address">address ethereum (0x...)</param>
    /// <param name="onlyWhiteListed">only those authorized CoinGeco tokens</param>
    /// <returns>ListTokenDto or null</returns>
    public override async Task<ICollection<TokenDto>?> CheckToken(string address, bool onlyWhiteListed = true)
    {
        var requestToken = new TokenRequest(address, onlyWhiteListed);
        var stringContent = requestToken.GetStringContent();
        
        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(ENDPOINT + AUTH_KEY, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var stringContentResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<TokenResponse>(stringContentResponse);
                return objResponse?.Result?.Assets.Select(x => new TokenDto()
                {
                    Balance = x.Balance,
                    Blockchain = x.Blockchain,
                    Thumbnail = x.Thumbnail,
                    TokenPrice = x.TokenPrice,
                    TokenSymbol = x.TokenSymbol,
                    BalanceUsd = x.BalanceUsd,
                    TokenName = x.TokenName,
                    Type = x.TokenType
                })
                    .ToList() ?? null;
            }
        }

        return null;
    }

    /// <summary>
    /// Check address NFT
    /// </summary>
    /// <param name="address">address ethereum (0x...)</param>
    /// <returns>ListNFTDto or null</returns>
    public override async Task<ICollection<NFTDto>?> CheckNFT(string address)
    {
        var request = new NFTRequest(address);
        var stringContent = request.GetStringContent();

        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(ENDPOINT + AUTH_KEY, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var stringContentResponse = await response.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<NFTResponse>(stringContentResponse);
                return objResponse?.Result?.Assets.Select(x => new NFTDto()
                {
                    Blockchain = x.Blockchain,
                    Name = x.Name,
                    Symbol = x.Symbol,
                    CollectionName = x.CollectionName,
                    ContractAddress = x.ContractAddress,
                    ImageUrl = x.ImageUrl,
                    TokenUrl = x.TokenUrl
                })
                    .ToList() ?? new List<NFTDto>();
            }
        }

        return new List<NFTDto>();
    }
    

    #region AnkrRequest Classes

    private class BaseRequest
    {
        [JsonInclude] 
        [JsonPropertyName("id")] 
        public int Id { get; private set; } = 1;

        [JsonInclude]
        [JsonPropertyName("jsonrpc")]
        public string JsonRPC { get; private set; } = "2.0";
        
        [JsonPropertyName("method")]
        public virtual string Method { get; }
        
        [JsonPropertyName("pageSize")]
        public virtual int PageSize { get; }
    }
    private sealed class NFTRequest : BaseRequest
    {
        public NFTRequest(string address)
        {
            Params.Address = address;
        }
        
        [JsonPropertyName("params")] 
        public NFTParams Params { get; } = new();

        [JsonPropertyName("method")] 
        public override string Method => "ankr_getNFTsByOwner";

        [JsonPropertyName("pageSize")] 
        public override int PageSize => 50;
        public StringContent GetStringContent()
        {
            var serialize = JsonSerializer.Serialize(this);
            return new StringContent(serialize, Encoding.UTF8, "application/json");
        }
    }
    private sealed class TokenRequest : BaseRequest
    {
        public TokenRequest(string address, bool onlyWhiteList)
        {
            Params.Address = address;
            Params.OnlyWhiteList = onlyWhiteList;
        }

        [JsonPropertyName("method")] public override string Method => "ankr_getAccountBalance";
        [JsonPropertyName("params")] public TokenParams Params { get; } = new();

        [JsonIgnore] 
        public override int PageSize { get; } = 50;

        public StringContent GetStringContent()
        {
            var serialize = JsonSerializer.Serialize(this);
            return new StringContent(serialize, Encoding.UTF8, "application/json");
        }
    }
    private sealed class TokenParams
    {
        [JsonPropertyName("walletAddress")]
        public string Address { get; set; }

        [JsonPropertyName("nativeFirst")] 
        public bool NativeFirst { get; set; } = true;

        [JsonPropertyName("onlyWhitelisted")]
        public bool OnlyWhiteList { get; set; }
    }
    private sealed class NFTParams
    {
        [JsonPropertyName("walletAddress")]
        public string Address { get; set; }
    
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
    }

    #endregion
    
    #region AnkRequest Classes

    private interface IJsonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; }
        [JsonPropertyName("jsonrpc")]
        public string JsonRPC { get;  }
    }
    private sealed class TokenResponse : IJsonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get;}
        [JsonPropertyName("jsonrpc")]
        public string JsonRPC { get; }
        [JsonPropertyName("result")]
        public TokenResult? Result { get; set; }
    }
    private sealed class NFTResponse : IJsonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get;}
        [JsonPropertyName("jsonrpc")]
        public string JsonRPC { get; }
        [JsonPropertyName("result")]
        public NFTResult Result { get; set; }
    }
    private sealed class NFTResult
    {
        [JsonPropertyName("owner")]
        public string? Owner { get; set; }
        [JsonPropertyName("assets")]
        public List<AssetNFT> Assets { get; set; }
        [JsonPropertyName("nextPageToken")]
        public string? NextPageToken { get; set; }
    }
    private sealed class TokenResult
    {
        [JsonPropertyName("totalBalanceUsd")]
        public string totalBalanceUsd { get; set; }
        [JsonPropertyName("totalCount")]
        public int totalCount { get; set; }
        [JsonPropertyName("assets")]
        public List<AssetToken> Assets { get; set; }
    }
    private sealed class AssetNFT
    {
        [JsonPropertyName("blockchain")]
        public string? Blockchain { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("tokenId")]
        public string? TokenId { get; set; }
        [JsonPropertyName("tokenUrl")]
        public string? TokenUrl { get; set; }
        [JsonPropertyName("imageUrl")]
        public string? ImageUrl { get; set; }
        [JsonPropertyName("collectionName")]
        public string? CollectionName { get; set; }
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        [JsonPropertyName("contractType")]
        public string? ContractType { get; set; }
        [JsonPropertyName("contractAddress")]
        public string? ContractAddress { get; set; }
    }
    private sealed class AssetToken
    {
        [JsonPropertyName("blockchain")]
        public string? Blockchain { get; set; }
        [JsonPropertyName("tokenName")]
        public string? TokenName { get; set; }
        [JsonPropertyName("tokenSymbol")]
        public string? TokenSymbol { get; set; }
        [JsonPropertyName("tokenDecimals")]
        public int TokenDecimals { get; set; }
        [JsonPropertyName("tokenType")]
        public string? TokenType { get; set; }
        [JsonPropertyName("contractAddress")]
        public string? ContractAddress { get; set; }
        [JsonPropertyName("holderAddress")]
        public string? HolderAddress { get; set; }
        [JsonPropertyName("balance")]
        public string? Balance { get; set; }
        [JsonPropertyName("balanceRawInteger")]
        public string? BalanceRawInteger { get; set; }
        [JsonPropertyName("balanceUsd")]
        public string? BalanceUsd { get; set; }
        [JsonPropertyName("tokenPrice")]
        public string? TokenPrice { get; set; }
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
    }

    #endregion
}