using CFinder.Application.Interfaces;
using CFinder.Domain.Log;
using CFinder.Domain.LogsCleaner;
using CFinder.Domain.Settings;
using CFinder.Domain.WorkHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CFinder.Persistence;

public sealed class DataStore : DbContext, IDataStore
{
    public DbSet<Settings> Settings { get; set; }
    public DbSet<WorkHistory> History { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<CleanerPattern> CleanerPatterns { get; set; }
    public DatabaseFacade DatabaseFacade { get; set; }
    

    public DataStore(
        string pragmas, 
        DbContextOptions<DataStore> options) 
        : base(options)
    {
        this.Database.ExecuteSqlRaw(pragmas);
    }

    public DataStore(DbContextOptions<DataStore> options)
        : base(options)
    {
        DatabaseFacade = this.Database;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}