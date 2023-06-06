using CFinder.Application.Models.Settings;
using MediatR;

namespace CFinder.Application.Repository.SettingsRepository.Commands.CreateSettings;

/// <summary>
/// Команда создания профиля
/// </summary>
public class CreateSettingsCommand : IRequest<SettingsDto>
{
    /// <summary>
    /// Имя профиля
    /// </summary>
    public string Name { get; set; } 
}