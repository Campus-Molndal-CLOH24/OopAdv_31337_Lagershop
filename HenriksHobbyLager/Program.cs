using HenriksHobbylager.Data;
using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using HenriksHobbylager.Interface;
using Microsoft.Extensions.Configuration;

namespace HenriksHobbylager;
internal class Program
{
    static async Task Main(string[] args)
    {
        await ActivationOfFacades();
    }

    private static async Task ActivationOfFacades()
    {
        var sqliteFacade = CreateSqLiteFacade();
        if (sqliteFacade == null)
        {
            throw new ArgumentNullException(nameof(sqliteFacade), "Misslyckades att skapa SQLite-fasaden.");
        }

        var mongoFacade = CreateMongoFacade();
        if (mongoFacade == null)
        {
            throw new ArgumentNullException(nameof(mongoFacade), "Misslyckades att skapa MongoDB-fasaden.");
        }

        var mainMenu = new MenuDb(sqliteFacade, mongoFacade);
        await mainMenu.ShowMainMenuAsync();
    }

    private static IProductFacade CreateSqLiteFacade()
    {
        var sqliteRepository = new SQLiteRepository(SQLiteDbContext.Instance);
        return new ProductFacade(sqliteRepository);
    }
    
    private static IProductFacade CreateMongoFacade()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var mongoConnectionString = configuration.GetConnectionString("MongoDbConnection");
        var mongoDatabaseName = configuration["ConnectionStrings:MongoDbName"];
    
        var mongoDbContext = MongoDbContext.Instance(mongoConnectionString!, mongoDatabaseName!);
        var mongoRepository = new MongoRepository(mongoDbContext);
        return new ProductFacade(mongoRepository);
    }
}
