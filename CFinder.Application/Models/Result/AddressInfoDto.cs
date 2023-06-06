namespace CFinder.Application.Models.Result;

public class AddressInfoDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Адрес криптовалюты
    /// </summary>
    public string Address { get; set; } 

    /// <summary>
    /// Коллекция токенов принадлежащих адресу
    /// </summary>
    public virtual ICollection<TokenDto>? Tokens { get; set; }
    
    /// <summary>
    /// Коллекция НФТ принадлежащих адресу
    /// </summary>
    public virtual ICollection<NFTDto>? Nfts { get; set; }
}