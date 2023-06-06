namespace CFinder.Domain.Log;

public class Authentication
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The application from which the data was taken
    /// </summary>
    public string? Application { get; set; }
    
    /// <summary>
    /// Link
    /// </summary>
    public string? Link { get; set; }
    
    /// <summary>
    /// Account login 
    /// </summary>
    public string? Login { get; set; }
    
    /// <summary>
    /// Account password
    /// </summary>
    public string? Password { get; set; }
}