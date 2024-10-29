using Microsoft.EntityFrameworkCore;
using Pitstop.RentalManagementAPI.Models;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalManagementAPI.DataAccess;

public class RentalManagementDBContext : DbContext
{
    public DbSet<RentalReservation> RentalReservations { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<RentalCar> RentalCars { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    public RentalManagementDBContext(DbContextOptions<RentalManagementDBContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>().HasIndex(b => b.Name).IsUnique();
        modelBuilder.Entity<RentalCar>().HasIndex(c => c.LicenseNumber).IsUnique();
    }
}