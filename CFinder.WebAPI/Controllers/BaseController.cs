using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CFinder.WebAPI.Controllers;

/// <summary>
/// Базовый класс контроллеров
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;

    /// <summary>
    /// Mediator для обращение через базовый класс
    /// </summary>
    protected IMediator? Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}