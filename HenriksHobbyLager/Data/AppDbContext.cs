

using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HenriksHobbylager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public string DbPath { get; }

 public AppDbContext()
        {
            // Get the base path of the project
            var projectBasePath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

            // Create the directory if it does not exist
            if (!Directory.Exists(projectBasePath))
            {
                Directory.CreateDirectory(projectBasePath);
            }

            // Set the path to the database
            DbPath = Path.Combine(projectBasePath, "hobbylager.db");

            // Check if the database already exists, do not overwrite it if it does.
            if (File.Exists(DbPath))
            {
                Console.WriteLine($"Databasen finns redan här: {DbPath}");
            }
            else
            {
                Console.WriteLine($"Skapar ny databas här: {DbPath}");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
