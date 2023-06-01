using System.ComponentModel.DataAnnotations;

namespace CFinder.WebAPI.Models;

public class CreateSettingsDto
{
    /// <summary>
    /// Имя профиля
    /// </summary>
    [Required] 
    public string Name { get; set; } = null!;
}