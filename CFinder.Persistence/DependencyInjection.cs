using CFinder.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CFinder.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("DataBase:DbConnection").Value;

        var dbPragma = configuration
            .GetSection("DataBase:DbPragma")
            .GetChildren()
            .Select(x => x.Value);
        var stringPragma = string.Join(' ', dbPragma);
        
        services.AddDbContext<IDataStore, DataStore>(options =>
        {
            options.UseSqlite(connectionString);
        });
        
        services.AddScoped<IDataStore>(provider =>
        {
            var options = provider.GetRequiredService<DbContextOptions<DataStore>>();
            return new DataStore(stringPragma, options);
        });

        return services;
    }
}