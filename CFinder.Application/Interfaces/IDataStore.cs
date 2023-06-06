using CFinder.Domain.Log;
using CFinder.Domain.LogsCleaner;
using CFinder.Domain.Settings;
using CFinder.Domain.WorkHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CFinder.Application.Interfaces;

public interface IDataStore
{
    DbSet<Settings>? Settings { get; set; }
    DbSet<WorkHistory>? History { get; set; }
    DbSet<Log>? Logs { get; set; }
    DbSet<CleanerPattern>? CleanerPatterns { get; set; }
    DatabaseFacade DatabaseFacade { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}