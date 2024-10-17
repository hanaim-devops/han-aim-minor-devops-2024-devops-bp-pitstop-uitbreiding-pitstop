using Microsoft.EntityFrameworkCore;
using RentalCarManagementAPI.Models;

namespace RentalCarManagementAPI;

public class RentalCarManagementDBContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<RentalCar> RentalCars { get; set; }
    public DbSet<Model> Models { get; set; }
}