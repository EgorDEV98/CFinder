namespace CFinder.Domain.Log;

public sealed class AddressInfo
{
    /// <summary>
    /// Identifier
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Main address
    /// </summary>
    public string? Address { get; init; }
    
    /// <summary>
    /// Number Of Address
    /// </summary>
    public int? NumberOfAddress { get; init; }
    
    /// <summary>
    /// Collection of tokens belonging to the address
    /// </summary>
    public ICollection<Token>? Tokens { get; init; }
    
    /// <summary>
    /// Collection of NMTs belonging to the address
    /// </summary>
    public ICollection<NFT>? Nfts { get; init; }
    
    
    public Wallet Wallet { get; set; }
    public int WalletId { get; set; }
}