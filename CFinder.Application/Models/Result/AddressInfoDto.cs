namespace CFinder.Application.Models.Result;

public class AddressInfoDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; init; }
    
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
    public ICollection<TokenDto>? Tokens { get; set; }
    
    /// <summary>
    /// Collection of NMTs belonging to the address
    /// </summary>
    public ICollection<NFTDto>? Nfts { get; set; }
}