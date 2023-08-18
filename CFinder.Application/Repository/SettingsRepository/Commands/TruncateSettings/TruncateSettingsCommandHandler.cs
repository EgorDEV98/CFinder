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
        await _dataStore.DatabaseFacade.ExecuteSqlRawAsync($"DELETE FROM Settings; DELETE FROM sqlite_sequence WHERE name = \'Settings\';", cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}