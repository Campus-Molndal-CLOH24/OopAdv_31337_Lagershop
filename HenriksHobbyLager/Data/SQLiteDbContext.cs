using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbylager.Data;
public class SQLiteDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    private readonly string _dbPath;

    public SQLiteDbContext()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Starta från körningskatalogen
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var relativePath = configuration["ConnectionStrings:DatabasePath"];
        if (string.IsNullOrWhiteSpace(relativePath))
            throw new ArgumentNullException("DatabasePath saknas i appsettings.json");

        // Gå upp tre nivåer från `bin/Debug/netX.0/` till projektets rotkatalog
        var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        if (string.IsNullOrWhiteSpace(projectRoot))
            throw new InvalidOperationException("Projektets root kunde inte identifieras.");

        // Kombinera projektroten med den relativa pathen
        _dbPath = Path.Combine(projectRoot, relativePath);

        // Debug-utskrift för att verifiera path
        Console.WriteLine($"Resolved SQLite Database Path: {_dbPath}");
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