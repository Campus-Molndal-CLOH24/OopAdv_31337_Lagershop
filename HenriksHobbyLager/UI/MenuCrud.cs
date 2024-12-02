using HenriksHobbylager.Models;
using HenriksHobbylager.Interface;
using HenriksHobbyLager.UI;

namespace HenriksHobbylager.UI;

internal class MenuCrud
{
    private readonly IProductFacade _currentFacade;
    private readonly IProductFacade _sqliteFacade;
    private readonly IProductFacade? _mongoFacade;

    public MenuCrud(IProductFacade currentFacade, IProductFacade sqliteFacade, IProductFacade? mongoFacade)
    {
        _currentFacade = currentFacade ?? throw new ArgumentNullException(nameof(currentFacade), "CurrentFacade är null.");
        _sqliteFacade = sqliteFacade ?? throw new ArgumentNullException(nameof(sqliteFacade), "SQLiteFacade är null.");
        _mongoFacade = mongoFacade;
    }

    internal async Task ShowMenu()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.Clear();
            DisplayMenuHeader();
            Console.WriteLine($"Använder: {_currentFacade.DatabaseType}.");
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
                    await AddProduct();
                    break;
                case "2":
                    await DeleteProduct();
                    break;
                case "3":
                    await UpdateProduct();
                    break;
                case "4":
                    await SearchProducts();
                    break;
                case "5":
                    await ShowAllProducts();
                    break;
                case "6":
                    keepRunning = false;
                    var menuDb = new MenuDb(_sqliteFacade, _mongoFacade);
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
        var name = ConsoleHelper.GetNonNullInput("Ange produktnamn: ");
        var category = ConsoleHelper.GetNonNullInput("Ange kategori: ");

        Console.WriteLine("Ange pris:");
        if (!decimal.TryParse(Console.ReadLine(), out var price))
        {
            ConsoleHelper.DisplayColourMessage("Ogiltigt pris. Försök igen.", ConsoleColor.Red);
            return;
        }

        Console.WriteLine("Ange lagerantal:");
        if (!int.TryParse(Console.ReadLine(), out var stock))
        {
            ConsoleHelper.DisplayColourMessage("Ogiltigt lagerantal. Försök igen.", ConsoleColor.Red);
            return;
        }

        await _currentFacade.CreateProductAsync(name, stock, price, category);
        ConsoleHelper.DisplayColourMessage("Produkten lades till.", ConsoleColor.Green);
    }

    private async Task DeleteProduct()
    {
        Console.Write("Ange ID för produkten som ska tas bort: ");
        var id = ConsoleHelper.GetNonNullInput("Ange produktens ID: ");

        try
        {
            await _currentFacade.DeleteProductAsync(id);
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
        var id = ConsoleHelper.GetNonNullInput("Ange ID för produkten som ska uppdateras: ");
        var product = await _currentFacade.GetProductByIdAsync(id);

        if (product == null)
        {
            ConsoleHelper.DisplayColourMessage("Produkten kunde inte hittas.", ConsoleColor.Red);
            return;
        }

        Console.WriteLine(
            $"Uppdaterar produkt: ID: {product.Id}, Namn: {product.Name}, Kategori: {product.Category}, Pris: {product.Price}, Lager: {product.Stock}");
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

        await _currentFacade.UpdateProductAsync(product);
        ConsoleHelper.DisplayColourMessage("Produkten har uppdaterats.", ConsoleColor.Green);
    }

    private async Task SearchProducts()
    {
        var searchTerm = ConsoleHelper.GetNonNullInput("Ange sökterm: ");
        var products = await _currentFacade.SearchProductsAsync(searchTerm);

        ShowSearchResults(products, "SÖKRESULTAT");
    }

    private async Task ShowAllProducts()
    {
        var products = await _currentFacade.GetAllProductsAsync();
        ShowSearchResults(products, "PRODUKTKATALOG");
    }

    private void ShowSearchResults(IEnumerable<Product> products, string header)
    {
        if (products.Any())
        {
            ConsoleHelper.DisplayColourMessage("=========================================", ConsoleColor.Cyan);
            ConsoleHelper.DisplayColourMessage($"               {header}                 ", ConsoleColor.Cyan);
            ConsoleHelper.DisplayColourMessage("=========================================", ConsoleColor.Cyan);

            Console.WriteLine("{0, -5} | {1, -20} | {2, -10} | {3, -10} | {4, -10} ", "ID", "Namn", "Kategori", "Pris",
                "Lager");
            Console.WriteLine(new string('-', 50));

            foreach (var product in products)
            {
                Console.WriteLine("{0, -5} | {1, -20} | {2, -10} | {3, -10:C} | {4, -10}",
                    product.DisplayId,  // Använder DisplayId
                    product.Name,
                    product.Category,
                    product.Price,
                    product.Stock);
            }

            ConsoleHelper.DisplayColourMessage("=========================================", ConsoleColor.Cyan);
        }
        else
        {
            ConsoleHelper.DisplayColourMessage("Inga produkter hittades.", ConsoleColor.Red);
        }
    }
}
