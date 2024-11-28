using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbylager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;

        // Constructor for dependency injection
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}