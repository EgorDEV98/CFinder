namespace CFinder.Application.Models.CleanerPatterns.DTOs;

public class CleanerPatternListDto
{
    /// <summary>
    /// Лист паттернов
    /// </summary>
    public IList<CleanerPatternDto>? CleanerPatterns { get; set; }
}