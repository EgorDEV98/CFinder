using CFinder.Domain.Log;

namespace CFinder.Application.Models.Result;

public class WalletDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Wallet directory
    /// </summary>
    public string? Directory { get; init; }
    
    /// <summary>
    /// Wallet type
    /// </summary>
    public Wallet.WalletType? Type { get; init; }
    
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
    public ICollection<AddressInfoDto>? Addresses { get; init; }
}