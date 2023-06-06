namespace CFinder.Application.Models.Settings;

public class SettingsDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя профиля
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Активность профиля
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Настройки парсера
    /// </summary>
    public ParserSettingsDto ParserSettings { get; set; }

    /// <summary>
    /// Настройки декриптовщика
    /// </summary>
    public DecryptorSettingsDto DecryptorSettings { get; set; }

    /// <summary>
    /// Настройки проверки баланса
    /// </summary>
    public BalanceCheckerSettingsDto BalanceCheckerSettings { get; set; }
}