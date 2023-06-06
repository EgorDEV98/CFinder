namespace CFinder.Domain.Log;

public class Token
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
    /// Token name
    /// </summary>
    public string? TokenName { get; init; }
    
    /// <summary>
    /// Symbol token
    /// </summary>
    public string? TokenSymbol { get; init; }
    
    /// <summary>
    /// Token type
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// Balance
    /// </summary>
    public string? Balance { get; init; }
    
    /// <summary>
    /// Token balance in USD
    /// </summary>
    public string? BalanceUsd { get; init; }
    
    /// <summary>
    /// The cost of the token
    /// </summary>
    public string? TokenPrice { get; init; }
    
    /// <summary>
    /// Token image
    /// </summary>
    public string? Thumbnail { get; init; }
}