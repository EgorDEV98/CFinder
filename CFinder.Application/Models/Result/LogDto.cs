namespace CFinder.Application.Models.Result;

public class LogDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Папка с логом/файлом
    /// </summary>
    public string Directory { get; set; } = null!;
    
    /// <summary>
    /// Все файлы лога
    /// </summary>
    public ICollection<string>? Files { get; set; }
    
    /// <summary>
    /// Коллекция авторизационных данных
    /// </summary>
    public virtual ICollection<AuthenticationDto>? Authentications { get; set; }
    
    /// <summary>
    /// Кошельки
    /// </summary>
    public virtual ICollection<WalletDto>? Wallets { get; set; } 
}