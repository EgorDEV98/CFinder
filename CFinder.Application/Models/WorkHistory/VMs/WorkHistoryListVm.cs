using CFinder.Application.Models.WorkHistory.DTOs;

namespace CFinder.Application.Models.WorkHistory.VMs;

public class WorkHistoryListVm
{
    /// <summary>
    /// Профилей
    /// </summary>
    public IList<WorkHistoryVm>? WorkHistory { get; set; }
}