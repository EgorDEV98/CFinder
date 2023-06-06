namespace CFinder.Domain.Log;

// ReSharper disable once InconsistentNaming
public class NFT
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Blockchain node
    /// </summary>
    public string? Blockchain { get; init; }
    
    /// <summary>
    /// NFT name
    /// </summary>
    public string? Name { get; init; }
    
    /// <summary>
    /// Link to NFT
    /// </summary>
    public string? TokenUrl { get; init; }
    
    /// <summary>
    /// Image
    /// </summary>
    public string? ImageUrl { get; init; }
    
    /// <summary>
    /// Collaction name
    /// </summary>
    public string? CollectionName { get; init; }
    
    /// <summary>
    /// Symbol
    /// </summary>
    public string? Symbol { get; init; }
    
    /// <summary>
    /// Contract address
    /// </summary>
    public string? ContractAddress { get; init; }
}