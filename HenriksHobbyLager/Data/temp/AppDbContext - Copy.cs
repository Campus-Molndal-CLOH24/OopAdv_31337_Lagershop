using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Sparar som referens när vi slår ihop MongoDBContext + SQLiteDbContext till AppDbContext

//namespace HenriksHobbylager.Data
//{
//    public class AppDbContext : DbContext
//    {
//        public DbSet<Product> Products { get; set; } = null!;
//        public string DbPath { get; }
//        // TODO; Code below: test for the database path. Might be possible to remove?
//        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//        public AppDbContext()
//        {
//            // Load appsettings.json from the output directory
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // appsettings.json is copied to bin/Debug to resolve pathing issues
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                .Build();

//            // Get the relative path from appsettings.json
//            var relativePath = configuration["ConnectionStrings:DatabasePath"]
//                ?? throw new ArgumentNullException("DatabasePath is not configured in appsettings.json");

//            // Combine with the base directory
//            DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder options)
//        {
//                options.UseSqlite($"Data Source={DbPath}");
//        }
//    }
//}
