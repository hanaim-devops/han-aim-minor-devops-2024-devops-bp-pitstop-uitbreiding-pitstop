using MaintenanceHistoryAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// setup logging
builder.Host.UseSerilog((context, logContext) => 
    logContext
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithMachineName()
);

builder.Services.AddControllers();

// Add services to the container.

// sql connection string
var sqlConnectionString = builder.Configuration.GetConnectionString("MaintenanceHistoryCN");
builder.Services.AddDbContext<MaintenanceHistoryContext>(options => options.UseSqlServer(sqlConnectionString));

// add messagepublisher
builder.Services.UseRabbitMQMessagePublisher(builder.Configuration);

// Register the Swagger generator, defining one or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Maintenance History API", Version = "v1" });
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<MaintenanceHistoryContext>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaintenanceHistory API - v1");
});

// auto migrate the database
using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetService<MaintenanceHistoryContext>()?.MigrateDB();
}

app.UseHttpsRedirection();

app.UseHealthChecks("/hc");

app.UseAuthorization();

app.MapControllers();

app.Run();