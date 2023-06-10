using CFinder.Domain.Log;

namespace CFinder.Application.Models.Result;

public class WalletDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Wallet directory
    /// </summary>
    public string? Directory { get; set; }
    
    /// <summary>
    /// Wallet type
    /// </summary>
    public Wallet.WalletType? Type { get; set; }
    
    /// <summary>
    /// Encrypted message
    /// </summary>
    public string? Encrypted { get; set; }
    
    /// <summary>
    /// HashCat
    /// </summary>
    public string? Hashcat { get; set; }
    
    /// <summary>
    /// Full decrypted message
    /// </summary>
    public string? Decrypted { get; set; }
    
    /// <summary>
    /// Wallet password
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Mnemonic/private key
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// Was the wallet decrypted
    /// </summary>
    public bool HasBeenDecrypted { get; set; }
    
    /// <summary>
    /// Addresses
    /// </summary>
    public ICollection<AddressInfoDto>? Addresses { get; set; }
}