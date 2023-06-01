using CFinder.Application.Models.WorkHistory.DTOs;
using MediatR;

namespace CFinder.Application.Repository.WorkHistoryRepository.Commands.CreateWorkHistory;

/// <summary>
/// Команда создания задачи
/// </summary>
public class CreateWorkHistoryCommand : IRequest<WorkHistoryDto>
{
    /// <summary>
    /// Название операции
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Путь до папки/файла
    /// </summary>
    public string? Path { get; set; }
}