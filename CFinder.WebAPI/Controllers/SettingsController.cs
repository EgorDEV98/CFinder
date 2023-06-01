using CFinder.Application.Common.AppResponse;
using CFinder.Application.Mappings;
using CFinder.Application.Models.Settings;
using CFinder.Application.Repository.SettingsRepository.Commands.CreateSettings;
using CFinder.Application.Repository.SettingsRepository.Commands.DeleteSettingsCommand;
using CFinder.Application.Repository.SettingsRepository.Commands.TruncateSettings;
using CFinder.Application.Repository.SettingsRepository.Commands.UpdateSettings;
using CFinder.Application.Repository.SettingsRepository.Queries.GetActiveSettings;
using CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsById;
using CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsList;
using CFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFinder.WebAPI.Controllers;


[Produces("application/json")]
[Route("api/[controller]")]
public class SettingsController : BaseController
{
    
    /// <summary>
    /// Get the list of settings
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /settings
    /// </remarks>>
    /// <returns>Return SettingsListVm</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SettingsListVm>> GetAll()
    {
        var query = new GetSettingsListQuery();
        var settingsListDto = await Mediator.Send(query);
        var vm = settingsListDto.Settings;
        
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = null,
            Data = vm
        });
    }
    
    /// <summary>
    /// Get the settings by Id
    /// </summary>
    /// <remarks>
    /// Semple request:
    /// GET /settings/FD6C10C8-FAF7-434C-88C5-ED52F1E838CD
    /// </remarks>
    /// <param name="Id">Settings Id</param>
    /// <returns>Return SettingsDetailsVm</returns>
    /// <response code="200">Success</response>   
    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SettingsVm>> GetById(int Id)
    {
        var query = new GetSettingsByIdQuery()
        {
            Id = Id
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
    /// Get the active settings
    /// </summary>
    /// <remarks>
    /// Semple request:
    /// GET /settings/FD6C10C8-FAF7-434C-88C5-ED52F1E838CD
    /// </remarks>
    /// <returns>Return SettingsDetailsVm</returns>
    /// <response code="200">Success</response>   
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/[controller]/Active")]
    public async Task<ActionResult<SettingsVm>> GetActive()
    {
        var query = new GetActiveSettingsQuery();
        var vm = await Mediator.Send(query);
    
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = null,
            Data = vm.ToVm()
        });
    }
    
    /// <summary>
    /// Create a new  settings profile
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /settings
    /// {
    ///     name: "profile name"
    /// }
    /// </remarks>
    /// <param name="createSettingsDto">CreateSettingsDto object</param>
    /// <returns>
    /// Return creating settings profile object
    /// </returns>
    /// <response code="200">Success</response>   
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Create([FromBody] CreateSettingsDto createSettingsDto)
    {
        var command = new CreateSettingsCommand()
        {
            Name = createSettingsDto.Name
        };
        var settings = await Mediator.Send(command);
    
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Profile successfully created!",
            Data = settings.ToVm()
        });
    }
    
    /// <summary>
    /// Update the settings profile
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /settings
    /// {
    ///     Id: 1,
    ///     Name: "profile name,
    ///     DecryptorSettings: {         
    ///         Depth: 3,             
    ///         TryDecrypt: true
    ///     },
    ///     BalanceCheckerSettings: {
    ///         IsCheckToken: true,    
    ///         IsCheckNFT: true       
    ///     }
    /// }
    /// </remarks>
    /// <param name="updateSettingsDto">UpdateSettingsDto object</param>
    /// <returns>Updated Content</returns>
    /// <response code="200">Success</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] SettingsDto updateSettingsDto)
    {
        var command = new UpdateSettingsCommand()
        {
            Id = updateSettingsDto.Id,
            Name = updateSettingsDto.Name,
            DecryptorSettings = updateSettingsDto.DecryptorSettings,
            ParserSettings = updateSettingsDto.ParserSettings,
            BalanceCheckerSettings = updateSettingsDto.BalanceCheckerSettings
        };
        var updated = await Mediator.Send(command);
        
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Profile successfully updated!",
            Data = updated.ToVm()
        });
    }
    
    /// <summary>
    /// Delete settings by Id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /settings/9435E0CD-5AF7-402E-8E38-8D2C6EB94D5A
    /// </remarks>
    /// <param name="id">Profile Id</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="200">Success</response> 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteSettingsCommand()
        {
            Id = id
        };
        await Mediator.Send(command);
    
        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Profile successfully deleted!",
            Data = null
        });
    }

    /// <summary>
    /// Truncate Settings table
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Truncate()
    {
        var command = new TruncateSettingsCommand();
        await Mediator.Send(command);

        return Ok(new AppResponse()
        {
            IsSuccess = true,
            Message = "Table \'Settings\' success truncated",
            Data = null
        });
    }
}