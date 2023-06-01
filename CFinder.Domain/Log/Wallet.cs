using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Log;

public class Wallet
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string? Id { get; set; }
    
    /// <summary>
    /// Папка с кошельком
    /// </summary>
    public string? Directory { get; set; }
    
    /// <summary>
    /// Тип кошелька
    /// </summary>
    public WalletType? Type { get; set; }
    
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
    public virtual ICollection<AddressInfo>? Addresses { get; set; }
    
    public enum WalletType : byte
    {
        [Display(Name = "Metamask")]  Metamask,
        [Display(Name = "Brave")]     Brave,
        [Display(Name = "Metamask")]  Ronin,
    }
}