using CFinder.Application.Models.Settings;
using CFinder.Domain.Settings;
using MediatR;

namespace CFinder.Application.Repository.SettingsRepository.Commands.UpdateSettings;

/// <summary>
/// Команда обновления профиля
/// </summary>
public class UpdateSettingsCommand : IRequest<SettingsDto>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Имя профиля
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Настройки парсера
    /// </summary>
    public ParserSettingsDto ParserSettings { get; set; } = null!;
    
    /// <summary>
    /// Настройки декриптовщика
    /// </summary>
    public DecryptorSettingsDto DecryptorSettings { get; set; } = null!;
    
    /// <summary>
    /// Настройки проверки баланса
    /// </summary>
    public BalanceCheckerSettingsDto BalanceCheckerSettings { get; set; } = null!;
}