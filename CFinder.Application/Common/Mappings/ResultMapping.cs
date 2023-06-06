using CFinder.Application.Models.Result;
using CFinder.Domain.Log;

namespace CFinder.Application.Mappings;

public static class ResultMapping
{
    public static LogDto ToDto(this Log settings)
    {
        return new LogDto()
        {
            Id = settings.Id,
            Authentications = settings.Authentications.Select(x => new AuthenticationDto()
            {
                Application = x.Applcation,
                Login = x.Login,
                Password = x.Password,
                Url = x.Link
            }).ToArray(),
            Directory = settings.Directory,
            Files = settings.Files,
            Wallets = settings?.Wallets?.Select(x => new WalletDto()
            {
                Directory = x.Directory,
                Password = x.Password,
                Decrypted = x.Decrypted,
                Encrypted = x.Encrypted,
                Id = x.Id,
                Hashcat = x.Hashcat,
                Secret = x.Secret,
                Type = x.Type,
                HasBeenDecrypted = x.HasBeenDecrypted,
                Addresses = x.Addresses?.Select(adresses => new AddressInfoDto()
                {
                    Id = adresses.Id,
                    Address = adresses.Address ?? string.Empty,
                    Nfts = adresses.Nfts.Select(nft => new NFTDto()
                    {
                        Id = nft.Id,
                        Blockchain = nft.Blockchain,
                        Name = nft.Name,
                        Symbol = nft.Symbol,
                        CollectionName = nft.CollectionName,
                        ContractAddress = nft.ContractAddress,
                        ImageUrl = nft.ImageUrl,
                        TokenUrl = nft.TokenUrl
                    }).ToArray(),
                    Tokens = adresses.Tokens.Select(token => new TokenDto()
                    {
                        Id = token.Id,
                        Blockchain = token.Blockchain,
                        Type = token.Type,
                        Balance = token.Balance,
                        Thumbnail = token.Thumbnail,
                        BalanceUsd = token.BalanceUsd,
                        TokenName = token.TokenName,
                        TokenPrice = token.TokenPrice,
                        TokenSymbol = token.TokenSymbol
                    }).ToArray()
                }).ToArray()
            }).ToArray()
        };
    }
    
    public static Log ToDomain(this LogDto settings)
    {
        return new Log()
        {
            Id = settings.Id,
            Authentications = settings.Authentications?.Select(x => new Authentication()
            {
                Applcation = x.Application,
                Login = x.Login,
                Password = x.Password,
                Link = x.Url
            }).ToArray(),
            Directory = settings.Directory,
            Files = settings.Files,
            Wallets = settings?.Wallets?.Select(x => new Wallet()
            {
                Directory = x.Directory,
                Password = x.Password,
                Decrypted = x.Decrypted,
                Encrypted = x.Encrypted,
                Id = x.Id,
                Hashcat = x.Hashcat,
                Secret = x.Secret,
                Type = x.Type,
                HasBeenDecrypted = x.HasBeenDecrypted,
                Addresses = x.Addresses?.Select(adresses => new AddressInfo()
                {
                    Id = adresses.Id,
                    Address = adresses.Address ?? string.Empty,
                    Nfts = adresses.Nfts?.Select(nft => new NFT()
                    {
                        Id = nft.Id,
                        Blockchain = nft.Blockchain,
                        Name = nft.Name,
                        Symbol = nft.Symbol,
                        CollectionName = nft.CollectionName,
                        ContractAddress = nft.ContractAddress,
                        ImageUrl = nft.ImageUrl,
                        TokenUrl = nft.TokenUrl
                    }).ToArray(),
                    Tokens = adresses.Tokens?.Select(token => new Token()
                    {
                        Id = token.Id,
                        Blockchain = token.Blockchain,
                        Type = token.Type,
                        Balance = token.Balance,
                        Thumbnail = token.Thumbnail,
                        BalanceUsd = token.BalanceUsd,
                        TokenName = token.TokenName,
                        TokenPrice = token.TokenPrice,
                        TokenSymbol = token.TokenSymbol
                    }).ToArray()
                }).ToArray()
            }).ToArray()
        };
    }
}