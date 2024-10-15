using MaintenanceHistoryAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// sql connection string
var sqlConnectionString = builder.Configuration.GetConnectionString("MaintenanceHistoryCN");
builder.Services.AddDbContext<MaintenanceHistoryContext>(options => options.UseSqlServer(sqlConnectionString));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// auto migrate the database
using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetService<MaintenanceHistoryContext>()?.MigrateDB();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();