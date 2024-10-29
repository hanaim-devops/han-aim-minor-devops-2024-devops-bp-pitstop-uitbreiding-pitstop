using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging.Configuration;
using Pitstop.RentalManagementAPI;
using Pitstop.RentalManagementAPI.DataAccess;
using Pitstop.WorkshopManagementEventHandler.DataAccess;

IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);

        services.AddTransient<RentalManagementDBContext>((svc) =>
        {
            var sqlConnectionString = hostContext.Configuration.GetConnectionString("WorkshopManagementCN");
            var dbContextOptions = new DbContextOptionsBuilder<RentalManagementDBContext>()
                .UseSqlServer(sqlConnectionString)
                .Options;
            var dbContext = new RentalManagementDBContext(dbContextOptions);

            DBInitializer.Initialize(dbContext);

            return dbContext;
        });

        services.AddHostedService<EventHandlerWorker>();
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();