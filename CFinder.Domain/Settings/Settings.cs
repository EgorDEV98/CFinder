namespace CFinder.Domain.Settings;

public class Settings
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
    /// Активность профиля
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Настройки парсера
    /// </summary>
    public ParserSettings ParserSettings { get; set; } = null!;

    /// <summary>
    /// Настройки декриптовщика
    /// </summary>
    public DecryptorSettings DecryptorSettings { get; set; } = null!;

    /// <summary>
    /// Настройки проверки баланса
    /// </summary>
    public BalanceCheckerSettings BalanceCheckerSettings { get; set; } = null!;

    /// <summary>
    /// Получить стандартные настройки
    /// </summary>
    public static Settings GetDefault()
        => new Settings()
        {
            Id = 0,
            IsActive = true,
            Name = "DEFAULT",
            DecryptorSettings = DecryptorSettings.GetDefault(),
            ParserSettings = ParserSettings.GetDefault(),
            BalanceCheckerSettings = BalanceCheckerSettings.GetDefault()
        };
}