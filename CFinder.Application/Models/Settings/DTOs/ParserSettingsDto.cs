using CFinder.Domain.Settings;

namespace CFinder.Application.Models.Settings;

public class ParserSettingsDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Тип сохрания паролей
    /// </summary>
    public AuthSaveAs SaveAs { get; set; }
    
    /// <summary>
    /// Тип парсинга
    /// </summary>
    public AuthParsingType ParsingType { get; set; }
}