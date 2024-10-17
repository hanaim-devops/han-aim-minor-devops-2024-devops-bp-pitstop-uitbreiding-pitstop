using Microsoft.EntityFrameworkCore;
using ReviewManagmentAPI.Models;

namespace ReviewManagmentAPI.DataAccess;

public class ReviewManagementDBContext : DbContext
{
    public DbSet<Review> Reviews { get; set; }

    public ReviewManagementDBContext(DbContextOptions<ReviewManagementDBContext> options) : base(options)
    {
    }
}