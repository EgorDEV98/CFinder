using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <inheritdoc />
[Produces("application/json")]
[Route("api/[controller]")]
public class LinksController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(new Uri("https://raw.githubusercontent.com/CryptoFinderWEB/CryptoFinderWEB/main/links.json"));
            var content = await response.Content.ReadAsStringAsync();
            
            return Ok(content);
        }
    }
}