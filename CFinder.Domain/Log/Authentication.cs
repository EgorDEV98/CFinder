namespace CFinder.Domain.Log;

public class Authentication
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Приложение с которого сняты данные
    /// </summary>
    public string? Applcation { get; set; }
    
    /// <summary>
    /// Ссылка
    /// </summary>
    public string? Link { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string? Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
}