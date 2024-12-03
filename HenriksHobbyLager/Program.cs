using HenriksHobbylager.Data.MongoDb;
using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HenriksHobbylager.Models;

namespace HenriksHobbylager;

internal class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();
        var mainMenu = serviceProvider.GetRequiredService<MenuDb>();
        await mainMenu.ShowMainMenuAsync();
    }

    // Building the service provider
    internal static ServiceProvider ConfigureServices()
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        var services = new ServiceCollection();

        // Adding the configuration to the DI-container
        services.AddSingleton<IConfiguration>(configuration);

        // SQLite-configuration
        services.AddScoped<SQLiteDbContext>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var dbPath = config["ConnectionStrings:DatabasePath"];
            if (string.IsNullOrEmpty(dbPath))
            {
                throw new ArgumentNullException("DatabasePath saknas i appsettings.json");
            }

            var options = new DbContextOptionsBuilder<SQLiteDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            return new SQLiteDbContext(options);
        });
        services.AddScoped<IRepository<Product>, SQLiteRepository>();
        services.AddScoped<SQLiteFacade>(provider =>
        {
            var dbContext = provider.GetRequiredService<SQLiteDbContext>();
            var repository = new SQLiteRepository(dbContext);
            return new SQLiteFacade(repository);
        });

        // MongoDB-configuration
        services.AddScoped<MongoDbContext>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var mongoConnectionString = config.GetConnectionString("MongoDbConnection");
            var dbName = config["ConnectionStrings:MongoDbName"];

            if (string.IsNullOrWhiteSpace(mongoConnectionString) || string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentException("MongoDB-konfigurationen saknas eller är ofullständig.");
            }

            return MongoDbContext.Instance(mongoConnectionString, dbName);
        });

        services.AddScoped<IRepository<Product>, MongoRepository>();
        services.AddScoped<MongoDbFacade>(provider =>
        {
            var mongoDbContext = provider.GetRequiredService<MongoDbContext>();
            var repository = new MongoRepository(mongoDbContext);
            return new MongoDbFacade(repository);
        });

        // Setting up the MenuDb
        services.AddScoped<MenuDb>(provider =>
        {
            var sqliteFacade = provider.GetRequiredService<SQLiteFacade>();
            var mongoFacade = provider.GetService<MongoDbFacade>(); // Se till att den inte är null
            if (mongoFacade == null)
            {
                throw new InvalidOperationException("MongoDB-fasaden är inte korrekt registrerad.");
            }
            return new MenuDb(sqliteFacade, mongoFacade);
        });

        return services.BuildServiceProvider();
    }
}
