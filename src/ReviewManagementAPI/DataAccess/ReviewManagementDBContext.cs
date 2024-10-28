using Microsoft.EntityFrameworkCore;
using ReviewManagementAPI.Models;

namespace ReviewManagementAPI.DataAccess;

public class ReviewManagementDBContext : DbContext
{
    public DbSet<Review> Reviews { get; set; }

    public ReviewManagementDBContext(DbContextOptions<ReviewManagementDBContext> options) : base(options)
    {
    }
}