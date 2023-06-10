using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Settings;

/// <summary>
/// Parser authentical settings
/// </summary>
public class ParserSettings
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Save password type
    /// </summary>
    public AuthSaveAs SaveAs { get; init; }
    
    /// <summary>
    /// Parsing authentical data type
    /// </summary>
    public AuthParsingType ParsingType { get; init; }
    
    public Settings Settings { get; set; }
    public long SettingsId { get; set; }

    /// <summary>
    /// Get default settings
    /// </summary>
    public static ParserSettings GetDefault()
        => new()
        {
            Id = 0,
            ParsingType = AuthParsingType.OnlyPassword,
            SaveAs = AuthSaveAs.SaveInFile,
        };
}

/// <summary>
/// Save password mode
/// </summary>
public enum AuthSaveAs : byte
{
    [Display(Name = "No Save")]            NoSave,
    [Display(Name = "Save in database")]   SaveInDatabase,
    [Display(Name = "Save in file")]       SaveInFile,
}

/// <summary>
/// Parsing of authentication data mode
/// </summary>
public enum AuthParsingType : byte
{
    [Display(Name = "Only Passwords")]     OnlyPassword,
    [Display(Name = "Login and Password")] LoginAndPassword,
    [Display(Name = "Full parsing")]       FullParsing,
}



