using CFinder.Domain.Settings;

namespace CFinder.Application.Models.Settings;

/// <summary>
/// Parser authentical settings
/// </summary>
public class ParserSettingsDto
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

    /// <summary>
    /// Get default settings
    /// </summary>
    public static ParserSettingsDto GetDefault()
        => new()
        {
            Id = 0,
            ParsingType = AuthParsingType.OnlyPassword,
            SaveAs = AuthSaveAs.SaveInFile
        };
}