using CFinder.Application.Services;
using CFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFinder.WebAPI.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class ServiceController : BaseController
{
    private readonly IServiceProvider _provider;
    private readonly CryptoFinderService _cfService;
    private readonly LogsCleanerService _cleanerService;

    public ServiceController(
        IServiceProvider provider, 
        CryptoFinderService cfService,
        LogsCleanerService cleanerService)
    {
        _provider = provider;
        _cfService = cfService;
        _cleanerService = cleanerService;
    }
    
    /// <summary>
    /// Start Crypto Finder Service
    /// </summary>
    /// <param name="cryptoFinderDto">CryptoFinderServiceDto obj</param>
    /// <returns></returns>
    [HttpPost("crypto-finder")]
    public async Task Start([FromBody] CryptoFinderServiceDto cryptoFinderDto)
    {
        await _cfService.StartAsync(cryptoFinderDto.Path);
    }

    /// <summary>
    /// Start Password Scraper Service
    /// </summary>
    /// <param name="passwordScraperDto">PasswordScraperServiceDto obj</param>
    /// <returns></returns>
    [HttpPost("password-scraper")]
    public async Task Start([FromBody] PasswordScraperServiceDto passwordScraperDto)
    {
        await Task.CompletedTask;
    }
    
    /// <summary>
    /// Start mnemonic Re-Checker Service
    /// </summary>
    /// <param name="mnemonicRecheckerDto">MnemonicRecheckerServiceDto obj</param>
    /// <returns></returns>
    [HttpPost("mnemonic-rechecker")]
    public async Task Start([FromBody] MnemonicRecheckerServiceDto mnemonicRecheckerDto)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// Cleans logs from potentially malicious files. The file formats to be deleted are in the
    /// </summary>
    /// <param name="cleanerServiceDto">LogsCleanerServiceDto obj</param>
    [HttpPost("logs-cleaner")]
    public async Task Start([FromBody] LogsCleanerServiceDto cleanerServiceDto)
    {
        await _cleanerService.StartAsync(cleanerServiceDto.Path);
    }
}