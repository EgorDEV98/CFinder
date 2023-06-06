namespace CFinder.Domain.LogsCleaner;

public class CleanerPattern
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Формат файла
    /// </summary>
    public string Format { get; set; } = null!;
}