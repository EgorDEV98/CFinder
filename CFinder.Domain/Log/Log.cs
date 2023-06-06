namespace CFinder.Domain.Log;

public class Log
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Logs directory
    /// </summary>
    public string? Directory { get; init; }
    
    /// <summary>
    /// All files 
    /// </summary>
    public ICollection<string>? Files { get; set; }
    
    /// <summary>
    /// Collection of authorization data
    /// </summary>
    public virtual ICollection<Authentication>? Authentications { get; set; }
    
    /// <summary>
    /// Crypto wallets
    /// </summary>
    public virtual ICollection<Wallet>? Wallets { get; set; } 
}