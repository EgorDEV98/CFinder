using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Settings;

/// <summary>
/// Настройки чекера
/// </summary>
public class BalanceCheckerSettings
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Режим проверки баланса
    /// </summary>
    public CheckCryptoMode CheckCrypto { get; set; }
    
    /// <summary>
    /// Только авторизованные CoinGeco токены
    /// </summary>
    public bool OnlyWhiteList { get; set; }
    
    /// <summary>
    /// Задержка перед запросом
    /// </summary>
    public short DelayBeforeRequest { get; set; }

    /// <summary>
    /// Получить стандартные настройки
    /// </summary>
    public static BalanceCheckerSettings GetDefault()
        => new BalanceCheckerSettings()
        {
            Id = 0,
            CheckCrypto = CheckCryptoMode.CheckOnlyNfts,
            DelayBeforeRequest = 1500,
            OnlyWhiteList = false
        };
}


/// <summary>
/// Режим проверки баланса
/// </summary>
public enum CheckCryptoMode : byte
{
    [Display(Name = "No Check")]          NoCheck,
    [Display(Name = "Only Tokens")]       CheckOnlyTokens,
    [Display(Name = "Only NFTs")]         CheckOnlyNfts,
    [Display(Name = "NFT and Tokens")]    AllCheck,
}