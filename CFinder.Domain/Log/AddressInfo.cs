namespace CFinder.Domain.Log;

public class AddressInfo
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Адрес криптовалюты
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Коллекция токенов принадлежащих адресу
    /// </summary>
    public virtual ICollection<Token> Tokens { get; set; }
    
    /// <summary>
    /// Коллекция НФТ принадлежащих адресу
    /// </summary>
    public virtual ICollection<NFT> Nfts { get; set; }
}