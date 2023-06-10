using CFinder.Application.Models.WorkHistory.DTOs;
using MediatR;

namespace CFinder.Application.Repository.WorkHistoryRepository.Queries.GetWorkHistoryById;

/// <summary>
/// Запрос получения подробной информации о работе по ID
/// </summary>
public class GetWorkHistoryByIdQuery : IRequest<WorkHistoryDto>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }
}