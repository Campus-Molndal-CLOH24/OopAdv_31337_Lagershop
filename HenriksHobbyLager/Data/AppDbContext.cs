using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbylager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public string DbPath { get; }

        public AppDbContext()
        {
            // Load appsettings.json from the output directory
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // This now works since appsettings.json is copied to bin/Debug
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the relative path from appsettings.json
            var relativePath = configuration["ConnectionStrings:DatabasePath"];
            // var projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;


            // Combine with the base directory
            DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite($"Data Source={DbPath}");
            }
        }
    }
}
