namespace Pitstop.WorkshopManagementEventHandler.DataAccess;

public static class DBInitializer
{
    public static void Initialize<TContext>(TContext context) where TContext : DbContext
    {
        Log.Information($"Ensure {typeof(TContext).Name} Database");

        Policy
            .Handle<Exception>()
            .WaitAndRetry(5, r => TimeSpan.FromSeconds(5), (ex, ts) => { Log.Error($"Error connecting to {typeof(TContext).Name} DB. Retrying in 5 sec."); })
            .Execute(() => context.Database.Migrate());

        Log.Information($"{typeof(TContext).Name} Database available");
    }
}