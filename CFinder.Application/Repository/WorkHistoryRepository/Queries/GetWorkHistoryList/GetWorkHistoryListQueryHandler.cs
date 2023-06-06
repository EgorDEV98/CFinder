using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using CFinder.Application.Models.WorkHistory.DTOs;
using CFinder.Application.Utils.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.WorkHistoryRepository.Queries.GetWorkHistoryList;

internal class GetWorkHistoryListQueryHandler : IRequestHandler<GetWorkHistoryListQuery, WorkHistoryListDto>
{
    private readonly IDataStore _dataStore;

    public GetWorkHistoryListQueryHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<WorkHistoryListDto> Handle(GetWorkHistoryListQuery request, CancellationToken cancellationToken)
    {
        var workHistorys = await _dataStore.History
            .ApplyRange(request.RangeStart, request.RangeEnd)
            .ApplyOrderBy(x => x.StartDate, request.OrderBy)
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);

        return new WorkHistoryListDto()
        {
            WorkHistory = workHistorys
        };
    }
}
