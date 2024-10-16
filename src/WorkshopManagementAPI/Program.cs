var builder = WebApplication.CreateBuilder(args);

// setup logging
builder.Host.UseSerilog((context, logContext) => 
    logContext
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithMachineName()
);

builder.WebHost.UseSentry(o =>
{
    o.Dsn = builder.Configuration["Sentry:DSN"];
    // When configuring for the first time, to see what the SDK is doing:
    o.Debug = true;
    // Set TracesSampleRate to 1.0 to capture 100%
    // of transactions for tracing.
    // We recommend adjusting this value in production
    o.TracesSampleRate = 1.0;
    // Sample rate for profiling, applied on top of othe TracesSampleRate,
    // e.g. 0.2 means we want to profile 20 % of the captured transactions.
    // We recommend adjusting this value in production.
    o.ProfilesSampleRate = 1.0;
    // Requires NuGet package: Sentry.Profiling
    // Note: By default, the profiler is initialized asynchronously. This can
    // be tuned by passing a desired initialization timeout to the constructor.
    o.AddIntegration(new ProfilingIntegration(
        // During startup, wait up to 500ms to profile the app startup code.
        // This could make launching the app a bit slower so comment it out if you
        // prefer profiling to start asynchronously.
        TimeSpan.FromMilliseconds(500)
    ));
});

// add repo
var eventStoreConnectionString = builder.Configuration.GetConnectionString("EventStoreCN");
builder.Services.AddTransient<IEventSourceRepository<WorkshopPlanning>>((sp) => 
    new SqlServerWorkshopPlanningEventSourceRepository(eventStoreConnectionString));

var workshopManagementConnectionString = builder.Configuration.GetConnectionString("WorkshopManagementCN");
builder.Services.AddTransient<IVehicleRepository>((sp) => new SqlServerRefDataRepository(workshopManagementConnectionString));
builder.Services.AddTransient<ICustomerRepository>((sp) => new SqlServerRefDataRepository(workshopManagementConnectionString));

// add messagepublisher
builder.Services.UseRabbitMQMessagePublisher(builder.Configuration);

// add commandhandlers
builder.Services.AddCommandHandlers();

builder.Services.AddTransient<IAddHistoryMaintenanceCommandHandler, AddHistoryMaintenanceCommandHandler>();


// Add framework services.
builder.Services
    .AddMvc((options) => options.EnableEndpointRouting = false)
    .AddNewtonsoftJson();

// Register the Swagger generator, defining one or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo  { Title = "WorkshopManagement API", Version = "v1" });
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddSqlServer(eventStoreConnectionString, name: "EventStoreHC")
    .AddSqlServer(workshopManagementConnectionString, name: "WorkshopManagementStoreHC");

// Setup MVC
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMvc();
app.UseDefaultFiles();
app.UseStaticFiles();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkshopManagement API - v1");
});

app.UseHealthChecks("/hc");

app.MapControllers();

app.Run();