using CFinder.Domain.Log;

namespace CFinder.Application.Models.Result;

public class WalletDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Папка с кошельком
    /// </summary>
    public string? Directory { get; set; }
    
    /// <summary>
    /// Тип кошелька
    /// </summary>
    public Wallet.WalletType? Type { get; set; }
    
    /// <summary>
    /// Зашифрованное сообщение
    /// </summary>
    public string? Encrypted { get; set; }
    
    /// <summary>
    /// HashCat строка
    /// </summary>
    public string? Hashcat { get; set; }
    
    /// <summary>
    /// Расшифрованное сообщение
    /// </summary>
    public string? Decrypted { get; set; }
    
    /// <summary>
    /// Пароль от кошелька
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Мнемоника/приватный ключ
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// Был ли расшифрован кошелек
    /// </summary>
    public bool HasBeenDecrypted { get; set; }

    /// <summary>
    /// Адреса
    /// </summary>
    public virtual ICollection<AddressInfoDto>? Addresses { get; set; }

    
}