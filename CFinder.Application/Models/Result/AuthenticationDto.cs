namespace CFinder.Application.Models.Result;

public class AuthenticationDto
{
    /// <summary>
    /// Ссылка
    /// </summary>
    public string? Url { get; set; }
    
    /// <summary>
    /// Браузер
    /// </summary>
    public string? Application { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string? Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
}