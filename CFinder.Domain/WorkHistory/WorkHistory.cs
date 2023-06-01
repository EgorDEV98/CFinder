using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.WorkHistory;

/// <summary>
/// Таблица истории запусков
/// </summary>
public class WorkHistory
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
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
    public DateTime StartDate { get; set; } = DateTime.Now;
    
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
    public Status Status { get; set; } = Status.AtWork;
}

/// <summary>
/// Статус работы
/// </summary>
public enum Status : byte
{
    [Display(Name = "Not started")]  NotStarted,
    [Display(Name = "At work")]      AtWork,
    [Display(Name = "Finished")]     Finished,
    [Display(Name = "Canceled")]     Canceled,
    [Display(Name = "Error")]        Error
}