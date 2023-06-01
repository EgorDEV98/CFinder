namespace CFinder.Domain.Log;

public class Log
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Папка с логом/файлом
    /// </summary>
    public string Directory { get; set; }
    
    /// <summary>
    /// Все файлы лога
    /// </summary>
    public ICollection<string> Files { get; set; }
    
    /// <summary>
    /// Коллекция авторизационных данных
    /// </summary>
    public virtual ICollection<Authentication>? Authentications { get; set; }
    
    /// <summary>
    /// Кошельки
    /// </summary>
    public virtual ICollection<Wallet>? Wallets { get; set; } 
}