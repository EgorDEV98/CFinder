using System.Reflection;
using CFinder.Application.Common.Behaviors;
using CFinder.Application.ServerMethods.Base;
using CFinder.Application.ServerMethods.Override;
using CFinder.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CFinder.Application;

/// <summary>
/// Внедрение зависимостей Application Layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        services.AddScoped<CryptoFinderService>();
        services.AddScoped<LogsCleanerService>();
        services.AddScoped<LogParserServerMethod>();
        services.AddScoped<AddressGeneratorServerMethod>();
        services.AddScoped<AnkrAddressCheckBalanceServerMethodOverride>();
        
        
        return services;
    }
}