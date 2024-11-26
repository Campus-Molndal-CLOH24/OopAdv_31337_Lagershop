

using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HenriksHobbylager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } // Lägg till denna rad

        public string DbPath { get; }

        public AppDbContext()
        {
            var folder = Path.Combine(AppContext.BaseDirectory, "Data");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            DbPath = Path.Combine(folder, "hobbylager.db");

            Console.WriteLine($"Databasen skapas här: {DbPath}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
