using HenriksHobbylager.Data;
using HenriksHobbylager.Data.MongoDb;
using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using HenriksHobbylager.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbylager;

internal class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var sqliteFacade = ConfigureSQLiteFacade(configuration);
        var mongoFacade = ConfigureMongoDbFacade(configuration);

        var mainMenu = new MenuDb(sqliteFacade, mongoFacade);
        await mainMenu.ShowMainMenuAsync();
    }

    private static IProductFacade ConfigureSQLiteFacade(IConfiguration configuration)
    {
        var dbPath = configuration["ConnectionStrings:DatabasePath"];
        if (string.IsNullOrEmpty(dbPath))
        {
            throw new ArgumentNullException("DatabasePath saknas i appsettings.json");
        }

        var options = new DbContextOptionsBuilder<SQLiteDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        var sqliteDbContext = new SQLiteDbContext(options);
        var sqliteRepository = new SQLiteRepository(sqliteDbContext);
        return new ProductFacade(sqliteRepository);
    }

    private static IProductFacade? ConfigureMongoDbFacade(IConfiguration configuration)
    {
        try
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoDbConnection");
            var mongoDatabaseName = configuration["ConnectionStrings:MongoDbName"];

            if (string.IsNullOrWhiteSpace(mongoConnectionString) || string.IsNullOrWhiteSpace(mongoDatabaseName))
            {
                Console.WriteLine("❌ MongoDB-konfigurationen saknas eller är ofullständig.");
                return null;
            }

            var mongoDbContext = MongoDbContext.Instance(mongoConnectionString, mongoDatabaseName);
            var mongoRepository = new MongoRepository(mongoDbContext);
            return new ProductFacade(mongoRepository);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ett fel inträffade när MongoDB-fasaden skulle skapas: {ex.Message}");
            return null;
        }
    }
}
