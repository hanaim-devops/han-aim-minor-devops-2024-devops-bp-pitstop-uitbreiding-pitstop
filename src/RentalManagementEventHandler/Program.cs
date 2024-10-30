using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging.Configuration;
using Pitstop.RentalCarManagementAPI.MappingProfiles;
using Pitstop.RentalManagementAPI;
using Pitstop.RentalManagementAPI.DataAccess;
using Pitstop.RentalManagementAPI.EventHandlers;
using Pitstop.WorkshopManagementEventHandler.DataAccess;
using Serilog;

IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);
        
        services.AddAutoMapper(typeof(RentalCarProfile), typeof(BrandProfile), typeof(ModelProfile));

        services.AddScoped<PitstopEventHandler, CustomerRegisteredHandler>();
        services.AddScoped<PitstopEventHandler, ModelRegisteredHandler>();
        services.AddScoped<PitstopEventHandler, BrandRegisteredHandler>();
        services.AddScoped<PitstopEventHandler, RentalCarRegisteredHandler>();

        services.AddTransient<RentalManagementDBContext>((svc) =>
        {
            var sqlConnectionString = hostContext.Configuration.GetConnectionString("RentalManagementCN");
            var dbContextOptions = new DbContextOptionsBuilder<RentalManagementDBContext>()
                .UseSqlServer(sqlConnectionString)
                .Options;
            var dbContext = new RentalManagementDBContext(dbContextOptions);

            DBInitializer.Initialize(dbContext);

            return dbContext;
        });

        services.AddHostedService<EventHandlerWorker>();
    })
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();