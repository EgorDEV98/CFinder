using CFinder.Application.Models.Result;
using CFinder.Domain.Log;

namespace CFinder.Application.Common.Mappings;

public static class ResultMapping
{
    public static LogDto ToDto(this Log log)
    {
        return new LogDto()
        {
            Id = log.Id,
            Authentications = log.Authentications?.Select(auth => new AuthenticationDto()
            {
                Application = auth.Application,
                Login       = auth.Login,
                Password    = auth.Password,
                Link        = auth.Link
            }).ToArray(),
            Directory = log.Directory,
            Files = log.Files,
            Wallets = log.Wallets?.Select(wallet => new WalletDto()
            {
                Id        = wallet.Id,
                Directory =  wallet.Directory,
                Password  =  wallet.Password,
                Decrypted =  wallet.Decrypted,
                Encrypted =  wallet.Encrypted,
                Hashcat   =  wallet.Hashcat,
                Secret    =  wallet.Secret,
                Type      =  wallet.Type,
                HasBeenDecrypted = wallet.HasBeenDecrypted,
                Addresses = wallet.Addresses?.Select(adressesInfo => new AddressInfoDto()
                {
                    Id              = adressesInfo.Id,
                    Address         = adressesInfo.Address ?? string.Empty,
                    NumberOfAddress = adressesInfo.NumberOfAddress,
                    Nfts            = adressesInfo.Nfts?.Select(nft => new NFTDto()
                    {
                        Id              = nft.Id,
                        Blockchain      = nft.Blockchain,
                        Name            = nft.Name,
                        Symbol          = nft.Symbol,
                        CollectionName  = nft.CollectionName,
                        ContractAddress = nft.ContractAddress,
                        ImageUrl        = nft.ImageUrl,
                        TokenUrl        = nft.TokenUrl
                    }).ToArray(),
                    Tokens = adressesInfo.Tokens?.Select(token => new TokenDto()
                    {
                        Id          = token.Id,
                        Blockchain  = token.Blockchain,
                        Type        = token.Type,
                        Balance     = token.Balance,
                        Thumbnail   = token.Thumbnail,
                        BalanceUsd  = token.BalanceUsd,
                        TokenName   = token.TokenName,
                        TokenPrice  = token.TokenPrice,
                        TokenSymbol = token.TokenSymbol
                    }).ToArray()
                }).ToArray()
            }).ToArray()
        };
    }
    
    public static Log ToDomain(this LogDto log)
    {
        return new Log()
        {
            Id = log.Id,
            Authentications = log.Authentications?.Select(auth => new Authentication()
            {
                Application = auth.Application,
                Login       = auth.Login,
                Password    = auth.Password,
                Link        = auth.Link
            }).ToArray(),
            Directory = log.Directory,
            Files = log.Files,
            Wallets = log.Wallets?.Select(wallet => new Wallet()
            {
                Id        = wallet.Id,
                Directory =  wallet.Directory,
                Password  =  wallet.Password,
                Decrypted =  wallet.Decrypted,
                Encrypted =  wallet.Encrypted,
                Hashcat   =  wallet.Hashcat,
                Secret    =  wallet.Secret,
                Type      =  wallet.Type,
                HasBeenDecrypted = wallet.HasBeenDecrypted,
                Addresses = wallet.Addresses?.Select(adressesInfo => new AddressInfo()
                {
                    Id              = adressesInfo.Id,
                    Address         = adressesInfo.Address ?? string.Empty,
                    NumberOfAddress = adressesInfo.NumberOfAddress,
                    Nfts            = adressesInfo.Nfts?.Select(nft => new NFT()
                    {
                        Id              = nft.Id,
                        Blockchain      = nft.Blockchain,
                        Name            = nft.Name,
                        Symbol          = nft.Symbol,
                        CollectionName  = nft.CollectionName,
                        ContractAddress = nft.ContractAddress,
                        ImageUrl        = nft.ImageUrl,
                        TokenUrl        = nft.TokenUrl
                    }).ToArray(),
                    Tokens = adressesInfo.Tokens?.Select(token => new Token()
                    {
                        Id          = token.Id,
                        Blockchain  = token.Blockchain,
                        Type        = token.Type,
                        Balance     = token.Balance,
                        Thumbnail   = token.Thumbnail,
                        BalanceUsd  = token.BalanceUsd,
                        TokenName   = token.TokenName,
                        TokenPrice  = token.TokenPrice,
                        TokenSymbol = token.TokenSymbol
                    }).ToArray()
                }).ToArray()
            }).ToArray()
        };
    }
}