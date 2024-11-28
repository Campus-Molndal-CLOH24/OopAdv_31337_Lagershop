using HenriksHobbylager.Data;
using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using HenriksHobbyLager.Repositories;
using HenriksHobbylager.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using HenriksHobbylager.Interface;

namespace HenriksHobbylager
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var sqliteFacade = CreateSqLiteFacade();
            var mongoFacade = CreateMongoFacade();

            await MainProgramMenuAsync(sqliteFacade, mongoFacade);
        }
        
        private static IProductFacade CreateSqLiteFacade()
        {
            var dbPath = "app.db"; // Hårdkodat för enkelhet
            var sqliteRepository = new SQLiteRepository(new SQLiteDbContext(dbPath));
            return new ProductFacade(sqliteRepository);
        }

        private static IProductFacade CreateMongoFacade()
        {
            var mongoConnectionString = "mongodb://localhost:27017";
            var mongoDatabaseName = "HenriksHobbylager";
            var mongoRepository = new MongoRepository(new MongoDbContext(mongoConnectionString, mongoDatabaseName));
            return new ProductFacade(mongoRepository);
        }
        
        static async Task MainProgramMenuAsync(IProductFacade sqliteFacade, IProductFacade mongoFacade)
        {
            while (true)
            {
                Console.WriteLine("Välj databasalternativ:");
                Console.WriteLine("1. SQLite");
                Console.WriteLine("2. MongoDB");
                Console.WriteLine("0. Avsluta");
                Console.WriteLine("Välj ett alternativ: ");

                var menuOption = Console.ReadLine();

                switch (menuOption)
                {
                    case "1":
                        var menuSQLite = new Menu(sqliteFacade);
                        await menuSQLite.ShowMenu();
                        break;
                    case "2":
                        var menuMongo = new Menu(mongoFacade);
                        await menuMongo.ShowMenu();
                        break;
                    case "0":
                        Console.WriteLine("Tryck valfri knapp för att avsluta.");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Felaktigt val. Välj mellan alternativ 1-2, eller 0 för att avsluta.");
                        break;
                }
            }
        }
    }
}

