using CFinder.Application.Interfaces;
using CFinder.Application.Mappings;
using CFinder.Application.Models.Settings;
using CFinder.Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.SettingsRepository.Commands.CreateSettings;

/// <summary>
/// Хендлер создания профиля
/// </summary>
internal class CreateSettingsCommandHandler : IRequestHandler<CreateSettingsCommand, SettingsDto>
{
    private readonly IDataStore _dataStore;

    public CreateSettingsCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<SettingsDto> Handle(CreateSettingsCommand request, CancellationToken cancellationToken)
    {
        var entity = new Settings()
        {
            Name = request.Name,
            IsActive = true,
            DecryptorSettings = DecryptorSettings.GetDefault(),
            ParserSettings = ParserSettings.GetDefault(),
            BalanceCheckerSettings = BalanceCheckerSettings.GetDefault()
        };
        
        await _dataStore.Settings.ForEachAsync(x => x.IsActive = false, cancellationToken);
        await _dataStore.Settings.AddAsync(entity, cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);

        return entity.ToDto();
    }
}