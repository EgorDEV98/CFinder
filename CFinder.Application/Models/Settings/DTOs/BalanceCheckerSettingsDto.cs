using CFinder.Domain.Settings;

namespace CFinder.Application.Models.Settings;

/// <summary>
/// Balance crypto checker settings
/// </summary>
public class BalanceCheckerSettingsDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; set; }
    
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

    /// <summary>
    /// GetDefault settings
    /// </summary>
    public static BalanceCheckerSettingsDto GetDefault()
        => new()
        {
            Id = 0,
            CheckCrypto = CheckCryptoMode.AllCheck,
            DelayBeforeRequest = 1500,
            OnlyWhiteList = true
        };
}
