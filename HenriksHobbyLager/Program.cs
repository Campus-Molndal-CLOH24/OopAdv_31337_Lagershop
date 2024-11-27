using HenriksHobbylager.Data;
using HenriksHobbylager.Facades;
using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using HenriksHobbylager.Models;
using HenriksHobbyLager.Repositories;
/* using HenriksHobbylager.UI; */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace HenriksHobbylager
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var dbPath = configuration["ConnectionStrings:DatabasePath"];

            var dbDirectory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(dbDirectory))
            {
                Directory.CreateDirectory(dbDirectory);
            }

            Console.WriteLine($"SQLite-databasen kommer att användas på: {dbPath}");


            // Configure services for dependency injection
            // Här skapas en ny instans av ServiceCollection. Det är en behållare där vi registrerar alla beroenden som applikationen behöver (t.ex. databaskontext, repositories och fasaden).
            var services = new ServiceCollection();

            // Register SQLite repository and SQLiteDbContext
            services.AddSingleton(_ => new SQLiteDbContext(dbPath));
            services.AddScoped<IRepository<Product>, SQLiteRepository>();

            // Register MongoDB configuration
            var mongoConnectionString = configuration["ConnectionStrings:MongoDbConnection"];
            var mongoDatabaseName = configuration["ConnectionStrings:MongoDbName"];
            services.AddSingleton(_ => new MongoDbContext(mongoConnectionString, mongoDatabaseName));
            services.AddScoped<IRepository<Product>, MongoRepository>();

            // Register ProductFacade and choose database 
            bool useMongo = bool.Parse(configuration["UseMongo"]);
            services.AddScoped<IProductFacade, ProductFacade>(provider =>
            {
                var sqliteRepo = provider.GetService<IRepository<Product>>();
                var mongoRepo = provider.GetService<IRepository<Product>>();
                return new ProductFacade(sqliteRepo, mongoRepo, useMongo); // If no database is chosen, SQLite will be used.
            });

            // Register Menu
            var serviceProvider = services.BuildServiceProvider();
            /*  var menu = new Menu(); */
            /* menu.ShowMenu(serviceProvider.GetService<IProductFacade>()); */


        }
    }
}
