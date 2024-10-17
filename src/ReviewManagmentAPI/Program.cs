using Microsoft.EntityFrameworkCore;
using ReviewManagmentAPI.DataAccess;
using ReviewManagmentAPI.Services;
using ReviewManagmentAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IReviewService, ReviewService>();

var sqlConnectionString = builder.Configuration.GetConnectionString("ReviewManagementCN");
builder.Services.AddDbContext<ReviewManagementDBContext>(options => options.UseSqlServer(sqlConnectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();