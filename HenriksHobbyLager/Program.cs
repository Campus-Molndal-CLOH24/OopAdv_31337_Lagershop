using HenriksHobbylager.Data;
using HenriksHobbylager.Facades;
using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using HenriksHobbylager.Models;
using HenriksHobbyLager.Repositories;
using HenriksHobbylager.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using HenriksHobbyLager.Data;


namespace HenriksHobbylager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await MainProgramMenuAsync();
        }

        static async Task MainProgramMenuAsync()
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
                        await SQLMenuAsync();
                        break;
                    case "2":
                        await MongoMenuAsync();
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

        static async Task SQLMenuAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            // Registrera SQLite
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration["ConnectionStrings:DatabasePath"]));
            services.AddScoped<IRepository<Product>, SQLiteRepository>();
            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<Menu>();

            var serviceProvider = services.BuildServiceProvider();

            // Kör menyn
            var menu = serviceProvider.GetService<Menu>();
            if (menu != null)
            {
                await menu.ShowMenu();
            }
            else
            {
                Console.WriteLine("Fel: Kunde inte ladda menyn.");
            }
        }

        static async Task MongoMenuAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            // Registrera MongoDB
            services.AddScoped<IRepository<Product>, MongoRepository>();
            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<Menu>();

            var serviceProvider = services.BuildServiceProvider();

            // Kör menyn
            var menu = serviceProvider.GetService<Menu>();
            if (menu != null)
            {
                await menu.ShowMenu();
            }
            else
            {
                Console.WriteLine("Fel: Kunde inte ladda menyn.");
            }
        }
    }
}