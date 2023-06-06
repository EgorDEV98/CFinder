using CFinder.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CFinder.Application.Services;

public abstract class BaseService
{
    protected readonly IDataStore? DataStore;
    protected readonly IServiceProvider? ServiceProvider;
    protected readonly IMediator? Mediator;

    protected BaseService(
        IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Mediator = serviceProvider?.GetService<IMediator>();
        DataStore = serviceProvider?.GetService<IDataStore>();
    }

}