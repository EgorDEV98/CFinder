using CFinder.Application.Models.Settings;
using MediatR;

namespace CFinder.Application.Repository.SettingsRepository.Queries.GetActiveSettings;

/// <summary>
/// Запрос на получение АКТИВНОГО профиля
/// </summary>
public class GetActiveSettingsQuery : IRequest<SettingsDto>
{
    
}