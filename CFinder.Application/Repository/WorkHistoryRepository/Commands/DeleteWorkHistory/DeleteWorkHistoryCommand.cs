using CFinder.Domain.WorkHistory;
using MediatR;

namespace CFinder.Application.Repository.WorkHistoryRepository.Commands.DeleteWorkHistory;

/// <summary>
/// Команда удаление истории задачи
/// </summary>
public class DeleteWorkHistoryCommand : IRequest<WorkHistory>, IRequest
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }
}