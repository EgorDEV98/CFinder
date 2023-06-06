using CFinder.Application.Common.AppResponse;
using CFinder.Application.Repository.LogCleanerRepository.Commands.CreatePattern;
using CFinder.Application.Repository.LogCleanerRepository.Commands.DeletePattern;
using CFinder.Application.Repository.LogCleanerRepository.Queries.GetPatternList;
using CFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFinder.WebAPI.Controllers;


[Produces("application/json")]
[Route("api/[controller]")]
public class CleanerPatternController : BaseController
{
    /// <summary>
    /// Get the list of cleaner patterns
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /settings
    /// </remarks>>
    /// <returns>Return SettingsListVm</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetCleanerPatternsQuery();
        var dto = await Mediator.Send(query);
        
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = null,
            Data = dto
        });
    }

    /// <summary>
    /// Delete format
    /// </summary>
    /// <param name="id">Id format</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var command = new DeletePatternCommand()
        {
            Id = id
        };
        await Mediator.Send(command);

        return Ok(new AppResponse()
        {
            Data = null,
            Message = "Format success deleted!",
            IsSuccess = true
        });
    }

    /// <summary>
    /// Create format for delete
    /// </summary>
    /// <param name="createClearpattern">CreateClearPatternDto obj</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateClearPatternDto createClearpattern)
    {
        var command = new CreatePatternCommand()
        {
            Format = createClearpattern.Format
        };

        await Mediator.Send(command);

        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Format success added!",
            Data = null
        });
    }
    
}