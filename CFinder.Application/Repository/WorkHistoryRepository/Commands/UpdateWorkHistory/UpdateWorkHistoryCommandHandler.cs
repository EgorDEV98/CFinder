using CFinder.Application.Common.Exceptions;
using CFinder.Application.Interfaces;
using CFinder.Domain.WorkHistory;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.WorkHistoryRepository.Commands.UpdateWorkHistory;

/// <summary>
/// Хендлер обновления задачи
/// </summary>
internal class UpdateWorkHistoryCommandHandler : IRequestHandler<UpdateWorkHistoryCommand, WorkHistory>
{
    private readonly IDataStore _dataStore;

    public UpdateWorkHistoryCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<WorkHistory> Handle(UpdateWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.History
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(WorkHistory), request.Id);
        }

        entity.Current = request.Current;
        entity.Total = request.Total;
        entity.EndDate = request.EndDate;
        entity.Status = request.Status;

        await _dataStore.SaveChangesAsync(cancellationToken);

        return entity;
    }
}