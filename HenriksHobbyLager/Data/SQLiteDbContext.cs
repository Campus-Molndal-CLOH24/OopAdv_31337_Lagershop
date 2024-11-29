using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbylager.Data;
public class SQLiteDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    private readonly string _dbPath;

    public SQLiteDbContext(string dbPath)
    {
        _dbPath = dbPath ?? throw new ArgumentNullException(nameof(dbPath));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Product>().ToTable("Products");
        
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name)
            .HasDatabaseName("IX_Products_Name");
        
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)  // Navigational property in OrderItem
            .WithMany()               // A Product can have many OrderItems
            .HasForeignKey(oi => oi.ProductId);  // Foreign key in OrderItem

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)   // Navigational property in OrderItem
            .WithMany(o => o.OrderItems) // An Order has many OrderItems
            .HasForeignKey(oi => oi.OrderId);  // Foreign key in OrderItem
    }
}