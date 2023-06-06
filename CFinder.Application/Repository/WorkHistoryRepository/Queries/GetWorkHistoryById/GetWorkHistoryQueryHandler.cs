using CFinder.Application.Common.Exceptions;
using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using CFinder.Application.Models.WorkHistory.DTOs;
using CFinder.Domain.WorkHistory;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.WorkHistoryRepository.Queries.GetWorkHistoryById;

internal class GetWorkHistoryQueryHandler : IRequestHandler<GetWorkHistoryByIdQuery, WorkHistoryDto>
{
    private readonly IDataStore _dataStore;

    public GetWorkHistoryQueryHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<WorkHistoryDto> Handle(GetWorkHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.History
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(WorkHistory), request.Id);
        }

        return entity.ToDto();
    }
}