using CFinder.Domain.Settings;

namespace CFinder.Application.Models.Settings;

public class BalanceCheckerSettingsDto
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
}