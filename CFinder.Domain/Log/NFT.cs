namespace CFinder.Domain.Log;

public class NFT
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
    /// Имя НФТ
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Ссылка на NFT
    /// </summary>
    public string? TokenUrl { get; set; }
    
    /// <summary>
    /// Изображение
    /// </summary>
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// Имя коллекции
    /// </summary>
    public string? CollectionName { get; set; }
    
    /// <summary>
    /// Символ в сети
    /// </summary>
    public string? Symbol { get; set; }
    
    /// <summary>
    /// Адрес контракта
    /// </summary>
    public string? ContractAddress { get; set; }

}