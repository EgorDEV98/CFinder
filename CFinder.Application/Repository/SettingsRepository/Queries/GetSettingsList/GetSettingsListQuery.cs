using CFinder.Application.Models.Settings;
using MediatR;

namespace CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsList;

/// <summary>
/// Запрос для получения всего списка профилей
/// </summary>
public class GetSettingsListQuery : IRequest<SettingsListDto>
{
    
}