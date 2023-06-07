using CFinder.Application.Models.AddressCheckerModels;
using CFinder.Application.Models.Result;
using CFinder.Application.Models.Settings;

namespace CFinder.Application.ServerMethods.Base;

public abstract class AddressCheckBalanceServerMethod
{
    /// <summary>
    /// Check Token and NFTs
    /// </summary>
    /// <param name="address">address</param>
    /// <param name="checkerSettingsDto">BalanceCheckerSettingsDto obj</param>
    /// <returns>TupleBalances obj</returns>
    public abstract Task<TupleBalances> CheckAll(string address, BalanceCheckerSettingsDto checkerSettingsDto);

    /// <summary>
    /// Check address token
    /// </summary>
    /// <param name="address">address ethereum (0x...)</param>
    /// <param name="onlyWhiteListed">only those authorized CoinGeco tokens</param>
    /// <returns>ListTokenDto or null</returns>
    public abstract Task<ICollection<TokenDto>?> CheckToken(string address, bool onlyWhiteListed = true);

    /// <summary>
    /// Check 
    /// </summary>
    /// <param name="address">address ethereum (0x...)</param>
    /// <returns>ListNFTDto or null</returns>
    public abstract Task<ICollection<NFTDto>?> CheckNFT(string address);
}