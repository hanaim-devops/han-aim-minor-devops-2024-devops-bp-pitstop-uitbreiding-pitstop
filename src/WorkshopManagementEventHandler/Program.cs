IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);

        services.AddTransient<WorkshopManagementDBContext>((svc) =>
        {
            var sqlConnectionString = hostContext.Configuration.GetConnectionString("WorkshopManagementCN");
            var dbContextOptions = new DbContextOptionsBuilder<WorkshopManagementDBContext>()
                .UseSqlServer(sqlConnectionString)
                .Options;
            var dbContext = new WorkshopManagementDBContext(dbContextOptions);

            DBInitializer.Initialize(dbContext);

            return dbContext;
        });

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

        services.AddHostedService<EventHandlerWorker>();
    })
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();