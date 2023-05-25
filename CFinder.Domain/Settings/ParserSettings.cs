using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Settings;

/// <summary>
/// Настройки парсера директории с логом
/// </summary>
public class ParserSettings
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Required]
    public int Id { get; set; }
    
    /// <summary>
    /// Тип сохрания паролей
    /// </summary>
    public AuthSaveAs SaveAs { get; set; }
    
    /// <summary>
    /// Тип парсинга
    /// </summary>
    public AuthParsingType ParsingType { get; set; }

    /// <summary>
    /// Получить стандартные настройки
    /// </summary>
    public static ParserSettings GetDefault()
        => new ParserSettings()
        {
            Id = 0,
            ParsingType = AuthParsingType.FullParsing,
            SaveAs = AuthSaveAs.NoSave,
        };
}

/// <summary>
/// Режим сохранения паролей
/// </summary>
public enum AuthSaveAs : byte
{
    [Display(Name = "No Save")]            NoSave,
    [Display(Name = "Save in database")]   SaveInDatabase,
    [Display(Name = "Save in file")]       SaveInFile,
}

/// <summary>
/// Тип парсинга
/// </summary>
public enum AuthParsingType : byte
{
    [Display(Name = "Only Passwords")]     OnlyPassword,
    [Display(Name = "Login and Password")] LoginAndPassword,
    [Display(Name = "Full parsing")]       FullParsing,
}



