namespace CFinder.Application.Models.Result;

public class LogDto
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
    public virtual ICollection<AuthenticationDto>? Authentications { get; set; }
    
    /// <summary>
    /// Crypto wallets
    /// </summary>
    public virtual ICollection<WalletDto>? Wallets { get; set; } 
}