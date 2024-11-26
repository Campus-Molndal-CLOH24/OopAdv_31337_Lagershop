

using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HenriksHobbylager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public string DbPath { get; }

 public AppDbContext()
        {
            // Read the connection string from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the path to the database
            var relativeDbPath = configuration.GetSection("ConnectionStrings:DatabasePath").Value;
            var projectBasePath = Path.Combine(Directory.GetCurrentDirectory(), relativeDbPath);

            // Create the directory if it doesn't exist
            var dbDirectory = Path.GetDirectoryName(projectBasePath);
            if (!Directory.Exists(dbDirectory))
            {
                Directory.CreateDirectory(dbDirectory);
            }

            DbPath = projectBasePath;

            Console.WriteLine($"Databasen skapas här: {DbPath}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
