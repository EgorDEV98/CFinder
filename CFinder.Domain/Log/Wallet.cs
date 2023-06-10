using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Log;

public sealed class Wallet
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Wallet directory
    /// </summary>
    public string? Directory { get; init; }
    
    /// <summary>
    /// Wallet type
    /// </summary>
    public WalletType? Type { get; init; }
    
    /// <summary>
    /// Encrypted message
    /// </summary>
    public string? Encrypted { get; init; }
    
    /// <summary>
    /// HashCat
    /// </summary>
    public string? Hashcat { get; init; }
    
    /// <summary>
    /// Full decrypted message
    /// </summary>
    public string? Decrypted { get; init; }
    
    /// <summary>
    /// Wallet password
    /// </summary>
    public string? Password { get; init; }
    
    /// <summary>
    /// Mnemonic/private key
    /// </summary>
    public string? Secret { get; init; }

    /// <summary>
    /// Was the wallet decrypted
    /// </summary>
    public bool HasBeenDecrypted { get; init; }
    
    /// <summary>
    /// Addresses
    /// </summary>
    public ICollection<AddressInfo>? Addresses { get; init; }
    
    
    public Log Log { get; set; }
    public int LogId { get; set; }
    
    public enum WalletType : byte
    {
        [Display(Name = "Metamask")]  Metamask,
        [Display(Name = "Brave")]     Brave,
        [Display(Name = "Metamask")]  Ronin,
    }
}