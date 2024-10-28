using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pitstop.RentalCarManagementAPI.Services;
using Pitstop.RentalCarManagementAPI.Services.Interfaces;
using RentalCarManagementAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IRentalCarService, RentalCarService>();

var sqlConnectionString = builder.Configuration.GetConnectionString("RentalCarManagementCN");
builder.Services.AddDbContext<RentalCarManagementDBContext>(options => options.UseSqlServer(sqlConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();