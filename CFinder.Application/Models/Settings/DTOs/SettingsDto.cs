namespace CFinder.Application.Models.Settings;

public class SettingsDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Profile name
    /// </summary>
    public string Name { get; set; } 
        = "NONAME";
    
    /// <summary>
    /// Profile activity
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Parser settings
    /// </summary>
    public ParserSettingsDto ParserSettings { get; set; } 
        = ParserSettingsDto.GetDefault();

    /// <summary>
    /// Decryptor settings
    /// </summary>
    public DecryptorSettingsDto DecryptorSettings { get; set; } 
        = DecryptorSettingsDto.GetDefault();

    /// <summary>
    /// Balance checker settings
    /// </summary>
    public BalanceCheckerSettingsDto BalanceCheckerSettings { get; set; }
        = BalanceCheckerSettingsDto.GetDefault();
    
    /// <summary>
    /// Get default settings
    /// </summary>
    public static SettingsDto GetDefault()
        => new()
        {
            Id = 0,
            IsActive = true,
            Name = "DEFAULT",
            DecryptorSettings = DecryptorSettingsDto.GetDefault(),
            ParserSettings = ParserSettingsDto.GetDefault(),
            BalanceCheckerSettings = BalanceCheckerSettingsDto.GetDefault()
        };
}