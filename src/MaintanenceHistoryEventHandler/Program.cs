using Pitstop.Infrastructure.Messaging.Configuration;
using Pitstop.MaintanenceHistoryEventHandler;
using Pitstop.MaintanenceHistoryEventHandler.DataAccess;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);
        services.AddTransient<MaintenanceHistoryContext>((svc) =>
        {
            var sqlConnectionString = hostContext.Configuration.GetConnectionString("MaintenanceHistoryCN");
            var dbContextOptions = new DbContextOptionsBuilder<MaintenanceHistoryContext>()
                .UseSqlServer(sqlConnectionString)
                .Options;

            var dbContext = new MaintenanceHistoryContext(dbContextOptions);
            
            DBInitializer.Initialize(dbContext);
            
            return dbContext;
        });
        services.AddHostedService<EventHandleWorker>();
    })
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .UseConsoleLifetime()
    .Build();
    
await host.RunAsync();