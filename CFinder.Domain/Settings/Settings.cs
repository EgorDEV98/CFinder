namespace CFinder.Domain.Settings;

public class Settings
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; set; }

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
    public ParserSettings ParserSettings { get; set; } 
        = ParserSettings.GetDefault();

    /// <summary>
    /// Decryptor settings
    /// </summary>
    public DecryptorSettings DecryptorSettings { get; set; } 
        = DecryptorSettings.GetDefault();

    /// <summary>
    /// Balance checker settings
    /// </summary>
    public BalanceCheckerSettings BalanceCheckerSettings { get; set; }
        = BalanceCheckerSettings.GetDefault();
    
    /// <summary>
    /// Get default settings
    /// </summary>
    public static Settings GetDefault()
        => new()
        {
            Id = 0,
            IsActive = true,
            Name = "DEFAULT",
            DecryptorSettings = DecryptorSettings.GetDefault(),
            ParserSettings = ParserSettings.GetDefault(),
            BalanceCheckerSettings = BalanceCheckerSettings.GetDefault()
        };
}