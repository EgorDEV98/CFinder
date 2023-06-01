namespace CFinder.WebAPI.Models;

public class CreateWorkHistoryDto
{
    /// <summary>
    /// Имя задачи
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Путь до папки/файла
    /// </summary>
    public string? Path { get; set; }
}