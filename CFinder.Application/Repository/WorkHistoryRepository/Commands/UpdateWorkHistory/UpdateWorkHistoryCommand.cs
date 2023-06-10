using CFinder.Domain.WorkHistory;
using MediatR;

namespace CFinder.Application.Repository.WorkHistoryRepository.Commands.UpdateWorkHistory;

/// <summary>
/// Команда обновления задачи
/// </summary>
public class UpdateWorkHistoryCommand : IRequest<WorkHistory>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Окончание работы операции
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Статус работы операции
    /// </summary>
    public Status Status { get; set; }
    
    /// <summary>
    /// Всего иттераций
    /// </summary>
    public long Total { get; set; }
    
    /// <summary>
    /// Текущая иттерация
    /// </summary>
    public long Current { get; set; }
}