using Pitstop.MaintanenceHistoryEventHandler.Model;

namespace Pitstop.MaintanenceHistoryEventHandler.DataAccess;

public class MaintenanceHistoryContext(DbContextOptions<MaintenanceHistoryContext> options) : DbContext(options)
{
    public DbSet<MaintenanceHistory> MaintenanceHistories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<MaintenanceHistory>().HasKey(m => m.LicenseNumber);
        builder.Entity<MaintenanceHistory>().ToTable("MaintenanceHistory");
        base.OnModelCreating(builder);
    }
    
    public void MigrateDB()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());
    }
};