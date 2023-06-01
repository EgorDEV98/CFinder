using CFinder.Domain.WorkHistory;

namespace CFinder.Application.Models.WorkHistory.VMs;

public class WorkHistoryVm
{
    /// <summary>
    /// Название операции
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Директория
    /// </summary>
    public string? Path { get; set; }
    
    /// <summary>
    /// Начало работы операции
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Окончание работы операции
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Всего иттераций
    /// </summary>
    public long Total { get; set; }
    
    /// <summary>
    /// Текущая иттерация
    /// </summary>
    public long Current { get; set; }
    
    /// <summary>
    /// Статус работы операции
    /// </summary>
    public Status Status { get; set; }
}