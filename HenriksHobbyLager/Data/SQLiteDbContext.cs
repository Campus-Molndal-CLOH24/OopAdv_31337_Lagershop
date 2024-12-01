using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbylager.Data
{
    public class SQLiteDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        private readonly string _dbPath;

        // private static SQLiteDbContext? _instance;
        private static readonly object _lock = new();

        // Singleton-pattern instance of SQLiteDbContext with thread-safe lazy initialization
        private static readonly Lazy<SQLiteDbContext> _lazyInstance = new(() => new SQLiteDbContext());
        public static SQLiteDbContext Instance => _lazyInstance.Value;

        private SQLiteDbContext()
        {
            // Load database path from appsettings.json, relative to project root to avoid issues with different paths/environment
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var relativePath = configuration["ConnectionStrings:DatabasePath"];
            if (string.IsNullOrWhiteSpace(relativePath))
                throw new ArgumentNullException("DatabasePath saknas i appsettings.json");

            var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            if (string.IsNullOrWhiteSpace(projectRoot))
                throw new InvalidOperationException("Projektets root kunde inte identifieras.");

            _dbPath = Path.Combine(projectRoot, relativePath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_dbPath}");
        }

        // ModelBuilder configures table mappings, indexes and relationships between entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name").IsUnique();
            

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);
                

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
