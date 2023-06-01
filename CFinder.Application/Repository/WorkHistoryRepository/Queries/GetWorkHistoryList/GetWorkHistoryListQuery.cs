using CFinder.Application.Models.WorkHistory.DTOs;
using MediatR;

namespace CFinder.Application.Repository.WorkHistoryRepository.Queries.GetWorkHistoryList;

public class GetWorkHistoryListQuery : IRequest<WorkHistoryListDto>
{
    public string? OrderBy { get; set; }
    public int? RangeStart { get; set; }
    public int? RangeEnd { get; set; }
}