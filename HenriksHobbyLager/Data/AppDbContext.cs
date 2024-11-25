namespace HenriksHobbyLager.Data;
using Microsoft.EntityFrameworkCore;
using HenriksHobbylager.Models;

public class AppDbContext : DbContext
{
    public DbSet<Product>? Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    // TODO: Break out connection string to appsettings.json.
        => options.UseSqlite("Data Source=Data/henrikshobbylager.db");

}

