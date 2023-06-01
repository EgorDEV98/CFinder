using CFinder.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.WorkHistoryRepository.Commands.TruncateWorkHistory;

/// <summary>
/// Хендлер очищения таблицы
/// </summary>
internal class TruncateWorkHistoryCommandHandler : IRequestHandler<TruncateWorkHistoryCommand>
{
    private readonly IDataStore _dataStore;
    
    public TruncateWorkHistoryCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task Handle(TruncateWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        string truncatingTableName = nameof(_dataStore.History);
        
        await _dataStore.DatabaseFacade.ExecuteSqlRawAsync($"DELETE FROM {truncatingTableName}; DELETE FROM sqlite_sequence WHERE name = \'{truncatingTableName}\';", cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}