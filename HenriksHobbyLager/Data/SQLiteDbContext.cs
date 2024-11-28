using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbylager.Data;
public class SQLiteDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    private readonly string _dbPath;

    public SQLiteDbContext(string dbPath)
    {
        _dbPath = dbPath ?? throw new ArgumentNullException(nameof(dbPath));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={_dbPath}");
    }
}

// Saving the below for reference. This is how you can configure the database to use a specific table name and index name.

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    base.OnModelCreating(modelBuilder);
//    modelBuilder.Entity<Product>().ToTable("Products");
//    modelBuilder.Entity<Product>()
//        .HasIndex(p => p.Name)
//        .HasDatabaseName("IX_Products_Name");
//}
