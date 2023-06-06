using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using CFinder.Application.Models.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsList;

/// <summary>
/// Хендлер для получения всего списка профилей
/// </summary>
internal class GetSettingsListHandler : IRequestHandler<GetSettingsListQuery, SettingsListDto>
{
    private readonly IDataStore _dataStore;

    public GetSettingsListHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<SettingsListDto> Handle(GetSettingsListQuery request, CancellationToken cancellationToken)
    {
        var settingsQuery = await _dataStore.Settings
            .Include(x => x.DecryptorSettings)
            .Include(x => x.ParserSettings)
            .Include(x => x.BalanceCheckerSettings)
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);

        return new SettingsListDto()
        {
            Settings = settingsQuery
        };
    }
}