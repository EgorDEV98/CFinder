using CFinder.Application.Models.Settings;
using MediatR;

namespace CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsById;

/// <summary>
/// Команда для получения профиля по ID
/// </summary>
public class GetSettingsByIdQuery : IRequest<SettingsDto>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }
}