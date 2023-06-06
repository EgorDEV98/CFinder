using CFinder.Domain.Log;

namespace CFinder.Application.Models.Result;

public class TokenDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Имя блокчейн сети
    /// </summary>
    public string? Blockchain { get; set; }
    
    /// <summary>
    /// Имя токена
    /// </summary>
    public string? TokenName { get; set; }
    
    /// <summary>
    /// Символ токена
    /// </summary>
    public string? TokenSymbol { get; set; }
    
    /// <summary>
    /// Тип токена
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Баланс токена
    /// </summary>
    public string? Balance { get; set; }
    
    /// <summary>
    /// Баланс токена в долларах
    /// </summary>
    public string? BalanceUsd { get; set; }
    
    /// <summary>
    /// Стоимость токена
    /// </summary>
    public string? TokenPrice { get; set; }
    
    /// <summary>
    /// Изображение связанное с токеном
    /// </summary>
    public string? Thumbnail { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public AddressInfoDto AddressInfo { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public int AddressInfoId { get; set; }
}