using HenriksHobbylager.Data;

using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using HenriksHobbylager.Models;
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




        // Val 1 för SQLite

        static async Task Main(string[] args)
        {



        }
        static async Task MainProgramMenuAsync(IProductFacade sqliteFacade, IProductFacade mongoFacade)
        {
            var _sqliteFacade = sqliteFacade ?? throw new ArgumentNullException(nameof(sqliteFacade));
            var _mongoFacade = mongoFacade ?? throw new ArgumentNullException(nameof(mongoFacade));
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
                        var menuSQLite = new Menu(_sqliteFacade);
                        await menuSQLite.ShowMenu();
                        break;
                    case "2":
                        var menuMongo = new Menu(_mongoFacade);
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

