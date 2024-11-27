

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
            // Read the connection string from appsettings and get the path to the database.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var projectBasePath = configuration.GetSection("ConnectionStrings:DatabasePath").Value;

            if (File.Exists(DbPath)) // Check if the database file exists in the project directory.
            {
                var dbDirectory = Path.GetDirectoryName(projectBasePath);
                
                if (!Directory.Exists(dbDirectory)) // Create the directory if it doesn't exist.
                {
                    Directory.CreateDirectory(dbDirectory);
                }

                DbPath = projectBasePath;
                Console.WriteLine($"Databasen skapas här: {DbPath}");
            } else
            {
                DbPath = projectBasePath;
                Console.WriteLine($"Databasen hittades här: {DbPath}");
            }
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
        
        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");
        }
    }
}
