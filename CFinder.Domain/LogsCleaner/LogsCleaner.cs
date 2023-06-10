namespace CFinder.Domain.LogsCleaner;

#nullable disable
public class CleanerPattern
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// File format ( .exe | .bin | etc...)
    /// </summary>
    public string Format { get; set; } 
}