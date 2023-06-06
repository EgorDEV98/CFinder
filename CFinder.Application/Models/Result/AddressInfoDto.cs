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
    public string Address { get; set; } = null!;

    /// <summary>
    /// Коллекция токенов принадлежащих адресу
    /// </summary>
    public virtual List<TokenDto> Tokens { get; set; } = new();
    
    /// <summary>
    /// Коллекция НФТ принадлежащих адресу
    /// </summary>
    public virtual List<NFTDto> Nfts { get; set; } = new();
}