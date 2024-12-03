using HenriksHobbylager.Models;
using HenriksHobbyLager.UI;
using HenriksHobbyLager.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace HenriksHobbylager.UI;

internal class MenuCrud
{
    private readonly SQLiteFacade? _sqliteFacade;
    private readonly MongoDbFacade? _mongoFacade;

    public MenuCrud(SQLiteFacade? sqliteFacade = null, MongoDbFacade? mongoFacade = null)
    {
        if (sqliteFacade == null && mongoFacade == null)
        {
            throw new ArgumentException("Minst en fasad måste vara tillgänglig.");
        }

        _sqliteFacade = sqliteFacade;
        _mongoFacade = mongoFacade;
    }

    internal async Task ShowMenu()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.Clear();
            DisplayMenuHeader();

            Console.WriteLine($"Aktiv databas: {(_sqliteFacade != null ? "SQLite" : "MongoDB")}");

            Console.WriteLine("1. Lägg till en produkt");
            Console.WriteLine("2. Ta bort en produkt");
            Console.WriteLine("3. Uppdatera en produkt");
            Console.WriteLine("4. Sök igenom produkterna");
            Console.WriteLine("5. Visa alla produkterna");
            Console.WriteLine("6. Gå tillbaka till databasmenyn");
            Console.WriteLine("0. Avsluta");
            Console.Write("\nVälj ett alternativ: ");

            var menuOption = Console.ReadLine();
            Console.Clear();

            switch (menuOption)
            {
                case "1":
                    DebugCurrentFacade(); // Temp: Debug method
                    await AddProduct();
                    break;
                case "2":
                    DebugCurrentFacade(); // Temp: Debug method
                    await DeleteProduct();
                    break;
                case "3":
                    DebugCurrentFacade(); // Temp: Debug method
                    await UpdateProduct();
                    break;
                case "4":
                    DebugCurrentFacade(); // Temp: Debug method
                    await SearchProducts();
                    break;
                case "5":
                    DebugCurrentFacade(); // Temp: Debug method
                    await ShowAllProducts();
                    break;
                case "6":
                    keepRunning = false;
                    var serviceProvider = Program.ConfigureServices(); // Skapar DI-container
                    var menuDb = serviceProvider.GetRequiredService<MenuDb>();
                    await menuDb.ShowMainMenuAsync();
                    break;
                case "0":
                    ConsoleHelper.DisplayColourMessage(
                        "Programmet avslutas. Tack för att du använde Henriks Hobbylager!", ConsoleColor.Green);
                    Environment.Exit(0);
                    break;
                default:
                    ConsoleHelper.DisplayColourMessage("Felaktigt val. Försök igen.", ConsoleColor.Red);
                    Console.ResetColor();
                    break;
            }

            if (keepRunning)
            {
                Console.WriteLine("\nVill du visa menyn igen eller avsluta?");
                Console.WriteLine("1. Visa menyn igen");
                Console.WriteLine("0. Avsluta");
                Console.Write("\nVälj ett alternativ: ");
                var choice = Console.ReadLine();

                if (choice == "0")
                {
                    ConsoleHelper.DisplayColourMessage(
                        "Programmet avslutas. Tack för att du använde Henriks Hobbylager!", ConsoleColor.Green);
                    Environment.Exit(0);
                }
                else if (choice != "1")
                {
                    ConsoleHelper.DisplayColourMessage("Felaktigt val. Programmet avslutas.", ConsoleColor.Red);
                    Environment.Exit(0);
                }
            }
        }
    }

    private static void DisplayMenuHeader()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        ConsoleHelper.PrintCentered("========================================");
        ConsoleHelper.PrintCentered("         Henriks Hobbylager MenuCrud        ");
        ConsoleHelper.PrintCentered("========================================");
        Console.ResetColor();
    }

    private async Task AddProduct()
    {
        if (_sqliteFacade != null)
        {
            await AddProductSQLite();
        }
        else if (_mongoFacade != null)
        {
            await AddProductMongo();
        }
        else
        {
            Console.WriteLine("❌ Ingen fasad är konfigurerad.");
        }
    }

    private async Task DeleteProduct()
    {
        Console.Write("Ange ID för produkten som ska tas bort: ");
        var id = ConsoleHelper.GetNonNullInput("Ange produktens ID: ");

        try
        {
            if (_sqliteFacade != null)
            {
                await _sqliteFacade.DeleteProductAsync(id);
            }
            else if (_mongoFacade != null)
            {
                await _mongoFacade.DeleteProductAsync(id);
            }
            else
            {
                ConsoleHelper.DisplayColourMessage("Ingen fasad är konfigurerad för att radera produkter.", ConsoleColor.Red);
                return;
            }

            ConsoleHelper.DisplayColourMessage("Produkten har tagits bort.", ConsoleColor.Green);
        }
        catch (ArgumentException ex)
        {
            ConsoleHelper.DisplayColourMessage(ex.Message, ConsoleColor.Red);
        }
        catch (Exception ex)
        {
            ConsoleHelper.DisplayColourMessage($"Ett oväntat fel inträffade: {ex.Message}", ConsoleColor.Red);
        }
    }

    private async Task UpdateProduct()
    {
        Console.Write("Ange ID för produkten som ska uppdateras: ");
        var id = ConsoleHelper.GetNonNullInput("Ange produktens ID: ");
        Product? product = null;

        if (_sqliteFacade != null)
        {
            product = await _sqliteFacade.GetProductByIdAsync(id);
        }
        else if (_mongoFacade != null)
        {
            product = await _mongoFacade.GetProductByIdAsync(id);
        }

        if (product == null)
        {
            ConsoleHelper.DisplayColourMessage("Produkten kunde inte hittas.", ConsoleColor.Red);
            return;
        }

        Console.WriteLine($"Uppdaterar produkt: ID: {product.Id}, Namn: {product.Name}, Kategori: {product.Category}, Pris: {product.Price}, Lager: {product.Stock}");
        Console.Write("Ange nytt namn (eller tryck Enter för att behålla): ");
        var newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName)) product.Name = newName;

        Console.Write("Ange ny kategori (eller tryck Enter för att behålla): ");
        var newCategory = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newCategory)) product.Category = newCategory;

        Console.Write("Ange nytt pris (eller tryck Enter för att behålla): ");
        if (decimal.TryParse(Console.ReadLine(), out var newPrice)) product.Price = newPrice;

        Console.Write("Ange nytt lagerantal (eller tryck Enter för att behålla): ");
        if (int.TryParse(Console.ReadLine(), out var newStock)) product.Stock = newStock;

        if (_sqliteFacade != null)
        {
            await _sqliteFacade.UpdateProductAsync(product);
        }
        else if (_mongoFacade != null)
        {
            await _mongoFacade.UpdateProductAsync(product);
        }

        ConsoleHelper.DisplayColourMessage("Produkten har uppdaterats.", ConsoleColor.Green);
    }
    private async Task SearchProducts()
    {
        var searchTerm = ConsoleHelper.GetNonNullInput("Ange sökterm: ");
        IEnumerable<Product> products;

        if (_sqliteFacade != null)
        {
            products = await _sqliteFacade.SearchProductsAsync(searchTerm);
        }
        else if (_mongoFacade != null)
        {
            products = await _mongoFacade.SearchProductsAsync(searchTerm);
        }
        else
        {
            ConsoleHelper.DisplayColourMessage("Ingen fasad är konfigurerad för att söka produkter.", ConsoleColor.Red);
            return;
        }

        ShowSearchResults(products, "SÖKRESULTAT");
    }


    private void ShowSearchResults(IEnumerable<Product> products, string header)
    {
        Console.WriteLine($"=== {header} ===");
        foreach (var product in products)
        {
            Console.WriteLine($"- {product.Name} | {product.Category} | {product.Price:C} | {product.Stock}");
        }
    }

    private async Task ShowAllProducts()
    {
        if (_sqliteFacade != null)
        {
            var products = await _sqliteFacade.GetAllProductsAsync();
            ShowProducts(products);
        }
        else if (_mongoFacade != null)
        {
            var products = await _mongoFacade.GetAllProductsAsync();
            ShowProducts(products);
        }
        else
        {
            ConsoleHelper.DisplayColourMessage("Ingen fasad är konfigurerad för att visa produkter.", ConsoleColor.Red);
        }
    }

    private void ShowProducts(IEnumerable<Product> products)
    {
        if (products.Any())
        {
            Console.WriteLine("========================================");
            Console.WriteLine("               PRODUKTKATALOG            ");
            Console.WriteLine("========================================");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} | {product.Category} | {product.Price} | {product.Stock}");
            }
        }
        else
        {
            Console.WriteLine("Inga produkter hittades.");
        }
    }
    private async Task AddProductSQLite()
    {
        var name = ConsoleHelper.GetNonNullInput("Ange produktnamn: ");
        var category = ConsoleHelper.GetNonNullInput("Ange kategori: ");
        Console.WriteLine("Ange pris:");
        if (!decimal.TryParse(Console.ReadLine(), out var price)) return;
        Console.WriteLine("Ange lagerantal:");
        if (!int.TryParse(Console.ReadLine(), out var stock)) return;

        await _sqliteFacade!.CreateProductAsync(name, stock, price, category);
    }

    private async Task AddProductMongo()
    {
        var name = ConsoleHelper.GetNonNullInput("Ange produktnamn: ");
        var category = ConsoleHelper.GetNonNullInput("Ange kategori: ");
        Console.WriteLine("Ange pris:");
        if (!decimal.TryParse(Console.ReadLine(), out var price)) return;
        Console.WriteLine("Ange lagerantal:");
        if (!int.TryParse(Console.ReadLine(), out var stock)) return;

        await _mongoFacade!.CreateProductAsync(name, stock, price, category);
    }

    private void DebugCurrentFacade()
    {
        if (_sqliteFacade != null)
        {
            Console.WriteLine($"Använder SQLite-fasaden: {_sqliteFacade.DatabaseType}");
        }
        if (_mongoFacade != null)
        {
            Console.WriteLine($"Använder MongoDB-fasaden: {_mongoFacade.DatabaseType}");
        }
    }

}