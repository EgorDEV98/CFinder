using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CFinder.Application.Models.Result;
using CFinder.Application.ServerMethods.Base;

namespace CFinder.Application.ServerMethods.Override;

public class AnkrAddressCheckBalanceServerMethodOverride : AddressCheckBalanceServerMethod
{
    private const string ENDPOINT = "https://rpc.ankr.com/multichain/";
    private const string AUTH_KEY = "2bd798e6459d20a3b5b034b93a72f6a21d7a960279a1138e8477ec6fb2fce9f0";
    
    public override async Task<List<TokenDto>> CheckToken(string address, bool onlyWhiteListed = true)
    {
        var requestString =
            $"{{\"id\":1,\"jsonrpc\":\"2.0\",\"method\":\"ankr_getAccountBalance\",\"params\":{{\"nativeFirst\":true,\"onlyWhitelisted\":{onlyWhiteListed.ToString().ToLower()},\"walletAddress\":\"{address}\"}}}}";
        
        var stringContent = new StringContent(requestString, Encoding.UTF8, "application/json");
        
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
                    .ToList() ?? new List<TokenDto>();
            }
        }

        return new List<TokenDto>();
    }

    public override async Task<List<NFTDto>> CheckNFT(string address)
    {
        var requestString = $"{{\"id\":1,\"jsonrpc\":\"2.0\",\"method\":\"ankr_getNFTsByOwner\",\"params\":{{\"walletAddress\":\"{address}\",\"pageSize\":50 }}}}";
        var stringContent = new StringContent(requestString, Encoding.UTF8, "application/json");
        
        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(ENDPOINT, stringContent);
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

    private interface IJsonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; }
        [JsonPropertyName("jsonrpc")]
        public string JsonRPC { get;  }
    }
    private class TokenResponse : IJsonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get;}
        [JsonPropertyName("jsonrpc")]
        public string? JsonRPC { get; }
        [JsonPropertyName("result")]
        public TokenResult? Result { get; set; }
    }
    private class NFTResponse : IJsonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get;}
        [JsonPropertyName("jsonrpc")]
        public string? JsonRPC { get; }
        [JsonPropertyName("result")]
        public NFTResult Result { get; set; }
    }
    
    private class NFTResult
    {
        [JsonPropertyName("owner")]
        public string? Owner { get; set; }
        [JsonPropertyName("assets")]
        public List<AssetNFT> Assets { get; set; }
        [JsonPropertyName("nextPageToken")]
        public string? NextPageToken { get; set; }
    }
    private class TokenResult
    {
        [JsonPropertyName("totalBalanceUsd")]
        public string totalBalanceUsd { get; set; }
        [JsonPropertyName("totalCount")]
        public int totalCount { get; set; }
        [JsonPropertyName("assets")]
        public List<AssetToken> Assets { get; set; }
    }
    
    private class AssetNFT
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
    private class AssetToken
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
}