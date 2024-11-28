﻿using HenriksHobbylager.Data;
using HenriksHobbylager.Facades;
using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories;
using HenriksHobbylager.Models;
using HenriksHobbyLager.Repositories;
using HenriksHobbylager.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;


namespace HenriksHobbylager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Setup DI container
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            var serviceProvider = services.BuildServiceProvider();

            // Resolve Menu and its dependencies
            var menu = new Menu();
            var productFacade = serviceProvider.GetService<IProductFacade>();
            if (productFacade != null)
            {
                await menu.ShowMenu(productFacade);
            }
            else
            {
                Console.WriteLine("Failed to initialize the application. ProductFacade is missing.");
            }

            //await MainProgramMenuAsync();
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
            // Configure services for dependency injection
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Ensure that the database directory is valid
            var dbPath = configuration["ConnectionStrings:DatabasePath"];
            if (string.IsNullOrWhiteSpace(dbPath))
            {
                Console.WriteLine("Fel: Databasens sökväg saknas i konfigurationen.");
                return;
            }

            // Ensure that the database directory exists
            var dbDirectory = Path.GetDirectoryName(dbPath);
            if (dbDirectory is not null && !Directory.Exists(dbDirectory))
            {
                Directory.CreateDirectory(dbDirectory);
            }

            Console.WriteLine($"SQLite-databasen kommer att användas på: {dbPath}");

            // Setup Dependency Injection for SQLite
            var services = new ServiceCollection();
            services.AddSingleton(_ => new AppDbContext());
            services.AddScoped<IRepository<Product>, SQLiteRepository>();
            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<Menu>();

            var serviceProvider = services.BuildServiceProvider();

            // Show the main menu
            var menu = serviceProvider.GetService<Menu>();
            if (menu != null)
            {
                var productFacade = serviceProvider.GetService<IProductFacade>();
                if (productFacade != null)
                {
                    await menu.ShowMenu(productFacade);
                }
                else
                {
                    Console.WriteLine("Fel: Kunde inte ladda produktfasaden.");
                }
            }
            else
            {
                Console.WriteLine("Fel: Kunde inte ladda menyn.");
            }
        }

        static async Task MongoMenuAsync()
        {
            // Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Ensure MongoDB connection string and database name are valid
            var mongoConnectionString = configuration["ConnectionStrings:MongoDbConnection"];
            var mongoDatabaseName = configuration["ConnectionStrings:MongoDbName"];

            if (string.IsNullOrWhiteSpace(mongoConnectionString) || string.IsNullOrWhiteSpace(mongoDatabaseName))
            {
                Console.WriteLine("Fel: MongoDB-anslutningssträng eller databasnamn saknas i konfigurationen.");
                return;
            }

            Console.WriteLine($"Använder MongoDB-databas: {mongoDatabaseName}");

            // Setup Dependency Injection for MongoDB
            var services = new ServiceCollection();
            services.AddSingleton(_ => new MongoDbContext(mongoConnectionString, mongoDatabaseName));
            services.AddScoped<IRepository<Product>, MongoRepository>();
            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<Menu>();

            var serviceProvider = services.BuildServiceProvider();

            // Show the menu
            var menu = serviceProvider.GetService<Menu>();
            if (menu != null)
            {
                var productFacade = serviceProvider.GetService<IProductFacade>();
                if (productFacade != null)
                {
                    await menu.ShowMenu(productFacade);
                }
                else
                {
                    Console.WriteLine("Fel: Kunde inte ladda produktfasaden.");
                }
            }
            else
            {
                Console.WriteLine("Fel: Kunde inte ladda menyn.");
            }
        }
    }
}

// Nedanför är referens-material för MongoDB-koden som jag tog bort vid merge-konflikten /NH

//// Configure services for dependency injection
//// Här skapas en ny instans av ServiceCollection. Det är en behållare där vi registrerar alla beroenden som applikationen behöver (t.ex. databaskontext, repositories och fasaden).
//var services = new ServiceCollection();

//// Register SQLite repository and SQLiteDbContext
//services.AddSingleton(_ => new SQLiteDbContext(dbPath));
//services.AddScoped<IRepository<Product>, SQLiteRepository>();

//// Register MongoDB configuration
//var mongoConnectionString = configuration["ConnectionStrings:MongoDbConnection"];
//var mongoDatabaseName = configuration["ConnectionStrings:MongoDbName"];
//services.AddSingleton(_ => new MongoDbContext(mongoConnectionString, mongoDatabaseName));
//services.AddScoped<IRepository<Product>, MongoRepository>();

//// Register ProductFacade and choose database 
//bool useMongo = bool.Parse(configuration["UseMongo"]);
//services.AddScoped<IProductFacade, ProductFacade>(provider =>
//{
//    var sqliteRepo = provider.GetService<IRepository<Product>>();
//    var mongoRepo = provider.GetService<IRepository<Product>>();
//    return new ProductFacade(sqliteRepo, mongoRepo, useMongo); // If no database is chosen, SQLite will be used.
//});

//// Register Menu
//var serviceProvider = services.BuildServiceProvider();
//var menu = new Menu();
//menu.ShowMenu(serviceProvider.GetService<IProductFacade>());