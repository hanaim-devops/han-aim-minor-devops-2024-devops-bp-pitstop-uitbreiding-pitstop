using DIYManagementAPI.Controllers;
using DIYManagementAPI.Data;
using DIYManagementAPI.Services;
using DIYManagementAPI.Models;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// setup logging
builder.Host.UseSerilog((context, logContext) =>
    logContext
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithMachineName()
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DIYService>();
builder.Services.AddScoped<DIYDAO>();

var connectionString = builder.Configuration.GetConnectionString("DIYManagementCN");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Use Prometheus metrics middleware to expose metrics at /metrics
app.UseMetricServer();

app.UseHttpMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetService<DatabaseContext>()?.MigrateDB();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
