namespace HenriksHobbyLager.Data;
using Microsoft.EntityFrameworkCore;
using HenriksHobbylager.Models;

public class AppDbContext : DbContext
{
    public DbSet<Product>? Product { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=Data/henrikshobbylager.db");

}

