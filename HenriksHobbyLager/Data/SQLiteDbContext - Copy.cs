using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;

// Sparar som referens när vi slår ihop MongoDBContext + SQLiteDbContext till AppDbContext

//namespace HenriksHobbylager.Data;
//public class SQLiteDbContext : DbContext
//{
//    public DbSet<Product> Products { get; set; } = null!;

//    private readonly string _dbPath;

//    public SQLiteDbContext(string dbPath)
//    {
//        _dbPath = dbPath ?? throw new ArgumentNullException(nameof(dbPath));
//    }

//    protected override void OnConfiguring(DbContextOptionsBuilder options)
//    {
//        options.UseSqlite($"Data Source={_dbPath}");
//    }
//}
