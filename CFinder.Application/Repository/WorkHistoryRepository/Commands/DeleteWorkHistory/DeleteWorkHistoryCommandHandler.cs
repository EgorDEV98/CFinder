using CFinder.Application.Common.Exceptions;
using CFinder.Application.Interfaces;
using CFinder.Domain.WorkHistory;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.WorkHistoryRepository.Commands.DeleteWorkHistory;

/// <summary>
/// Хендлер удаления команды истории задачи
/// </summary>
internal class DeleteWorkHistoryCommandHandler : IRequestHandler<DeleteWorkHistoryCommand>
{
    private readonly IDataStore _dataStore;

    public DeleteWorkHistoryCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(DeleteWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.History
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(WorkHistory), request.Id);
        }

        _dataStore.History.Remove(entity);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}