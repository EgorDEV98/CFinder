using CFinder.Domain.Settings;

namespace CFinder.WebAPI.Models;

public class UpdateSettingsDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя профиля
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Настройки парсера логов
    /// </summary>
    public ParserSettingsDto ParserSettings { get; set; } = null!;
    
    /// <summary>
    /// Настройки декриптовщика
    /// </summary>
    public DecryptorSettingsDto DecryptorSettings { get; set; } = null!;
    
    /// <summary>
    /// Настройки поиска баланса
    /// </summary>
    public BalanceCheckerSettingsDto BalanceCheckerSettings { get; set; } = null!;
    

    /// <summary>
    /// Настройки парсера логов
    /// </summary>
    public class ParserSettingsDto
    {
        /// <summary>
        /// Тип сохрания паролей
        /// </summary>
        public AuthSaveAs SaveAs { get; set; }
    
        /// <summary>
        /// Тип парсинга
        /// </summary>
        public AuthParsingType ParsingType { get; set; }
    }
    
    /// <summary>
    /// Настройки декриптовщика
    /// </summary>
    public class DecryptorSettingsDto
    {
        /// <summary>
        /// Декриптовать
        /// </summary>
        public bool TryDecrypt { get; set; }
    
        /// <summary>
        /// Кол-во потоков
        /// </summary>
        public byte ThreadCount { get; set; }
    
        /// <summary>
        /// Глубина генерации
        /// </summary>
        public byte DepthGenerate { get; set; }
    
        /// <summary>
        /// Режим сохранения результатов
        /// </summary>
        public DecryptSaveAs DecryptSaveAs { get; set; }
    
        /// <summary>
        /// Режим парсинга кошельков
        /// </summary>
        public EncryptedParsingType WalletParsingType { get; set; }
    }
    /// <summary>
    /// Настройки поиска баланса
    /// </summary>
    public class BalanceCheckerSettingsDto
    {
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
    
}