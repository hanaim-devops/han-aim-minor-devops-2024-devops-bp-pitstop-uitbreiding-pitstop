using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// using Pitstop.ReviewManagementAPI.Services;
// using Pitstop.ReviewManagementAPI.Services.Interfaces;
using ReviewManagementAPI.DataAccess;
using ReviewManagementAPI.Services;
using ReviewManagementAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IReviewService, ReviewService>();

var sqlConnectionString = builder.Configuration.GetConnectionString("ReviewManagementCN");
builder.Services.AddDbContext<ReviewManagementDBContext>(options => options.UseSqlServer(sqlConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();