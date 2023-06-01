using CFinder.Application.Models.WorkHistory.DTOs;
using CFinder.Application.Models.WorkHistory.VMs;
using CFinder.Domain.WorkHistory;

namespace CFinder.Application.Mappings;

public static class WorkHistoryMapping
{
    /// <summary>
    /// DTO to DB Model
    /// </summary>
    /// <param name="workHistory"></param>
    /// <returns></returns>
    public static WorkHistory ToDomain(this WorkHistoryDto workHistory)
    {
        return new WorkHistory()
        {
            Id = workHistory.Id,
            Name = workHistory.Name,
            Current = workHistory.Current,
            Path = workHistory.Path,
            Status = workHistory.Status,
            Total = workHistory.Total,
            EndDate = workHistory.EndDate,
            StartDate = workHistory.StartDate
        };
    }
    
    /// <summary>
    /// DB Model to DTO
    /// </summary>
    /// <param name="workHistory"></param>
    /// <returns></returns>
    public static WorkHistoryDto ToDto(this WorkHistory workHistory)
    {
        return new WorkHistoryDto()
        {
            Id = workHistory.Id,
            Name = workHistory.Name,
            Current = workHistory.Current,
            Path = workHistory.Path,
            Status = workHistory.Status,
            Total = workHistory.Total,
            EndDate = workHistory.EndDate,
            StartDate = workHistory.StartDate
        };
    }
    
    /// <summary>
    /// DTO to VM
    /// </summary>
    /// <param name="workHistory"></param>
    /// <returns></returns>
    public static WorkHistoryVm ToVm(this WorkHistoryDto workHistory)
    {
        return new WorkHistoryVm()
        {
            Name = workHistory.Name,
            Current = workHistory.Current,
            Path = workHistory.Path,
            Status = workHistory.Status,
            Total = workHistory.Total,
            EndDate = workHistory.EndDate,
            StartDate = workHistory.StartDate
        };
    }
}