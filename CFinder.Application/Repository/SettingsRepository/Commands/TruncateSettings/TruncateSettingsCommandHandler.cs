using CFinder.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.SettingsRepository.Commands.TruncateSettings;

/// <summary>
/// Хендлер очищения настроек
/// </summary>
internal class TruncateSettingsCommandHandler : IRequestHandler<TruncateSettingsCommand>
{
    private readonly IDataStore _dataStore;

    public TruncateSettingsCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(TruncateSettingsCommand request, CancellationToken cancellationToken)
    {
        string truncatingTableName = nameof(_dataStore.Settings);
        
        await _dataStore.DatabaseFacade.ExecuteSqlRawAsync($"DELETE FROM {truncatingTableName}; DELETE FROM sqlite_sequence WHERE name = \'{truncatingTableName}\';", cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}