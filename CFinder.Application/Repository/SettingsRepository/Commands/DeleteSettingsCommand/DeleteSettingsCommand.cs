using MediatR;

namespace CFinder.Application.Repository.SettingsRepository.Commands.DeleteSettingsCommand;

/// <summary>
/// Команда удаления профиля
/// </summary>
public class DeleteSettingsCommand : IRequest
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
}