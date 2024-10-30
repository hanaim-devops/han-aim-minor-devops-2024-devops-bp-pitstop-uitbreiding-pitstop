using Microsoft.EntityFrameworkCore;
using Pitstop.RentalManagementAPI.Models;
using RentalManagementAPI.Models;

namespace Pitstop.RentalManagementAPI;

public class RentalManagementDbContext : DbContext
{
    public DbSet<RentalReservation> RentalReservations { get; set; }
    public DbSet<RentalCar> RentalCars { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    
    public RentalManagementDbContext(DbContextOptions<RentalManagementDbContext> options) : base(options)
    {
    }
}