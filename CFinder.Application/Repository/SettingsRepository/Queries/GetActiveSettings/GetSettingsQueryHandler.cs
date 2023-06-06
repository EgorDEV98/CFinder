using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using CFinder.Application.Models.Settings;
using CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsById;
using CFinder.Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.SettingsRepository.Queries.GetActiveSettings;

/// <summary>
/// Хендлер на получение активного профиля
/// </summary>
internal class GetSettingsQueryHandler : IRequestHandler<GetActiveSettingsQuery, SettingsDto>
{
    private readonly IDataStore _dataStore;

    public GetSettingsQueryHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task<SettingsDto> Handle(GetActiveSettingsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.Settings
            .Include(x => x.DecryptorSettings)
            .Include(x => x.ParserSettings)
            .Include(x => x.BalanceCheckerSettings)
            .FirstOrDefaultAsync(x => x.IsActive, cancellationToken);

        if (entity == null)
        {
            return Settings.GetDefault().ToDto();
        }
        
        return entity.ToDto();
    }
}