using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HenriksHobbylager.Data
{
    public class SQLiteDbContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
    {
        public SQLiteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SQLiteDbContext>();
            
            optionsBuilder.UseSqlite("Data Source=Data/hobbylager.db");
            return new SQLiteDbContext(optionsBuilder.Options);
        }
    }
}