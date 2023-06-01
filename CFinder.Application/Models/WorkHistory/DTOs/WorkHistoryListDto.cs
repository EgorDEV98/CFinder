namespace CFinder.Application.Models.WorkHistory.DTOs;

public class WorkHistoryListDto
{
    /// <summary>
    /// Профилей
    /// </summary>
    public IList<WorkHistoryDto>? WorkHistory { get; set; }
}