using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using HenriksHobbylager.Data;
namespace HenriksHobbylager.Data.Factory
{
	public class SQLiteDbContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
	{
		public SQLiteDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SQLiteDbContext>();

			// Ange din SQLite-anslutningssträng här
			optionsBuilder.UseSqlite("Data Source=hobbylager.db");

			return new SQLiteDbContext(optionsBuilder.Options);
		}
	}
}
