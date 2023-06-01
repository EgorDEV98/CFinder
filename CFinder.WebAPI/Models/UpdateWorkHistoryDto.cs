using CFinder.Domain.WorkHistory;

namespace CFinder.WebAPI.Models;

public class UpdateWorkHistoryDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

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