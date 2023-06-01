using CFinder.Application.Common.AppResponse;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.CreateWorkHistory;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.DeleteWorkHistory;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.TruncateWorkHistory;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.UpdateWorkHistory;
using CFinder.Application.Repository.WorkHistoryRepository.Queries.GetWorkHistoryById;
using CFinder.Application.Repository.WorkHistoryRepository.Queries.GetWorkHistoryList;
using CFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFinder.WebAPI.Controllers;


[Produces("application/json")]
[Route("api/[controller]")]
public class WorkHistoryController : BaseController
{
    /// <summary>
    /// Get more info about History Item by Id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /WorkHistory/E2D3DBC3-4837-4A16-8853-DFA3EECF092D
    /// </remarks>
    /// <param name="id">Id work history item</param>
    /// <returns>
    /// GetWorkHistoryByIdQuery obj
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMoreInfo(int id)
    {
        var query = new GetWorkHistoryByIdQuery()
        {
            Id = id
        };
        var vm = await Mediator.Send(query);

        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = null,
            Data = vm
        });
    }

    /// <summary>
    /// Get list actual workHistory
    /// </summary>
    /// <returns>
    /// WorkHistoryListVm obj
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetListWorkHistory(string orderBy = "desc", int? rangeStart = 1, int? rangeEnd = 50)
    {
        var query = new GetWorkHistoryListQuery()
        {
            OrderBy = orderBy,
            RangeStart = rangeStart,
            RangeEnd = rangeEnd
        };
        var vm = await Mediator.Send(query);

        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = null,
            Data = vm
        });
    }
    
    /// <summary>
    /// Create work history
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /WorkHistory
    /// {
    ///     "name": "Any task",
    ///     "isServiceHistory": false
    /// }
    /// </remarks>>
    /// <param name="createWorkHistoryDto">CreateWorkHistoryDto obj</param>
    /// <returns>Created WorkHistory obj</returns>
    [HttpPost]
    public async Task<IActionResult> CreateWorkHistory([FromBody] CreateWorkHistoryDto createWorkHistoryDto)
    {
        var command = new CreateWorkHistoryCommand()
        {
            Name = createWorkHistoryDto.Name,
            Path = createWorkHistoryDto.Path
        };

        var workHistory = await Mediator.Send(command);
        
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Task Successfuly added!",
            Data = workHistory
        });
    }
    
    /// <summary>
    /// Update work history
    /// </summary>
    /// <param name="workHistoryDto">UpdateWorkHistoryDto obj</param>
    /// <returns>WorkHistory obj</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateWorkHistory([FromBody] UpdateWorkHistoryDto workHistoryDto)
    {
        var command = new UpdateWorkHistoryCommand()
        {
            Id = workHistoryDto.Id,
            EndDate = workHistoryDto.EndDate,
            Current = workHistoryDto.Current,
            Total = workHistoryDto.Total,
            Status = workHistoryDto.Status
        };
        var workHistory = await Mediator.Send(command);
    
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Work history success updated",
            Data = workHistory
        });
    }

    /// <summary>
    /// Remove work History by Id
    /// </summary>
    /// <param name="id">Id work history item</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkHistory(int id)
    {
        var command = new DeleteWorkHistoryCommand()
        {
            Id = id
        };
        await Mediator.Send(command);

        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Work history success deleted",
            Data = null
        });
    }
    
    /// <summary>
    /// Truncate table History
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> Truncate()
    {
        await Mediator.Send(new TruncateWorkHistoryCommand());
        
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Success truncated WorkHistory Table",
            Data = null
        });
    }
}