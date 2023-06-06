using System.ComponentModel.DataAnnotations;

namespace CFinder.WebAPI.Models;


/// <summary>
/// DTO для криптофинтера
/// </summary>
public class CryptoFinderServiceDto
{
    [Required] 
    public string Path { get; set; } = null!;
}

/// <summary>
/// DTO для пассворд граббера
/// </summary>
public class PasswordScraperServiceDto
{
    [Required]
    public string Path { get; set; } = null!;
    
}

/// <summary>
/// DTO для мнемоник речекера
/// </summary>
public class MnemonicRecheckerServiceDto
{
    
}

public class LogsCleanerServiceDto
{
    [Required]
    public string Path { get; set; }
}

/// <summary>
/// Тип сервиса
/// </summary>
public enum Type : byte
{
    [Display(Name = "Crypto Finder Service")]       CryptoFinder,
    [Display(Name = "Password Scraper Service")]    PasswordScraper,
    [Display(Name = "Mnemonic Re-checker Service")] MnemonicRecheker,
}