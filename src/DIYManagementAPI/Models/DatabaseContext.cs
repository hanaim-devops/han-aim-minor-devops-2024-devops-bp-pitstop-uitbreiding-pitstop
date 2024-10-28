using Microsoft.EntityFrameworkCore;
using Polly;

namespace DIYManagementAPI.Models
{
        public class DatabaseContext : DbContext
        {
                public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
                {
                }

                public DbSet<DIYRegistration> DIYRegistrations { get; set; }

                public DbSet<DIYEveningModel> DIYEvening { get; set; }

                public void MigrateDB()
                {
                        Policy
                            .Handle<Exception>()
                            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                            .Execute(() => Database.Migrate());
                }
        }
}