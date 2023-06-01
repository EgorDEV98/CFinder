namespace CFinder.Application.Common.AppResponse;

/// <summary>
/// Класс ответа
/// </summary>
public class AppResponse
{
    /// <summary>
    /// Статус выполнения
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Сообщение
    /// </summary>
    public string? Message { get; set; }
    
    /// <summary>
    /// Данные
    /// </summary>
    public object? Data { get; set; }
}