using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Settings;

/// <summary>
/// Balance crypto checker settings
/// </summary>
public class BalanceCheckerSettings
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Check crypto mode
    /// </summary>
    public CheckCryptoMode CheckCrypto { get; set; }
    
    /// <summary>
    /// Only CoinGeco Tokens
    /// </summary>
    public bool OnlyWhiteList { get; set; }
    
    /// <summary>
    /// Delay before request
    /// </summary>
    public short DelayBeforeRequest { get; set; }
    
    public Settings Settings { get; set; }
    public int SettingsId { get; set; }

    /// <summary>
    /// GetDefault settings
    /// </summary>
    public static BalanceCheckerSettings GetDefault()
        => new()
        {
            Id = 0,
            CheckCrypto = CheckCryptoMode.AllCheck,
            DelayBeforeRequest = 1500,
            OnlyWhiteList = false
        };
}


/// <summary>
/// Crypto checker mode
/// </summary>
public enum CheckCryptoMode : byte
{
    [Display(Name = "No Check")]          NoCheck,
    [Display(Name = "Only Tokens")]       CheckOnlyTokens,
    [Display(Name = "Only NFTs")]         CheckOnlyNfts,
    [Display(Name = "NFT and Tokens")]    AllCheck,
}