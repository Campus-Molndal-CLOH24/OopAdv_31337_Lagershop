using HenriksHobbylager.Models;
using HenriksHobbylager.Interface;

namespace HenriksHobbylager.UI;
internal class MenuCrud
{
	private readonly IProductFacade _currentFacade;
	private readonly IProductFacade sqliteFacade;
	private readonly IProductFacade mongoFacade;

    // We use .this to make sure that the current facade is the one that is used in the CRUD operations.
    public MenuCrud(IProductFacade currentFacade, IProductFacade sqliteFacade, IProductFacade mongoFacade)
	{
		_currentFacade = currentFacade ?? throw new ArgumentNullException(nameof(currentFacade));
		this.sqliteFacade = sqliteFacade ?? throw new ArgumentNullException(nameof(sqliteFacade));
		this.mongoFacade = mongoFacade ?? throw new ArgumentNullException(nameof(mongoFacade));
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
					var menuDb = new MenuDb(sqliteFacade, mongoFacade);
					await menuDb.ShowMainMenuAsync();
					break;
				case "0":
                    DisplayColourMessage("Programmet avslutas. Tack för att du använde Henriks Hobbylager!", ConsoleColor.Green);
                    Environment.Exit(0);
					break;
				default:
                    DisplayColourMessage("Felaktigt val. Försök igen.", ConsoleColor.Red);
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
                    DisplayColourMessage("Programmet avslutas. Tack för att du använde Henriks Hobbylager!", ConsoleColor.Green);
                    Environment.Exit(0);
				}
				else if (choice != "1")
				{
                    DisplayColourMessage("Felaktigt val. Programmet avslutas.", ConsoleColor.Red);
                    Environment.Exit(0);
				}
			}
		}
	}

	private static void DisplayMenuHeader()
	{
        DisplayColourMessage("========================================", ConsoleColor.Green);
        DisplayColourMessage("         Henriks Hobbylager MenuCrud        ", ConsoleColor.Green);
        DisplayColourMessage("========================================", ConsoleColor.Green);
    }

    private async Task AddProduct()
	{
        var name = GetNonNullInput("Ange produktnamn: ");

        Console.WriteLine("Ange pris:");
		if (!decimal.TryParse(Console.ReadLine(), out var price))
		{
            DisplayColourMessage("Ogiltigt pris. Försök igen.", ConsoleColor.Red);
            return;
		}

		Console.WriteLine("Ange lagerantal:");
		if (!int.TryParse(Console.ReadLine(), out var stock))
		{
            DisplayColourMessage("Ogiltigt lagerantal. Försök igen.", ConsoleColor.Red);
            return;
		}

		// var product = new Product { Name = name, Price = price, Stock = stock };
		await _currentFacade.CreateProductAsync(name, stock, price);
        DisplayColourMessage("Produkten lades till.", ConsoleColor.Green);
    }

    private async Task UpdateProduct()
    {
        Console.WriteLine("Vill du söka efter produkt med:");
        Console.WriteLine("1. Produkt-ID");
        Console.WriteLine("2. Produktnamn");
        Console.Write("Välj alternativ: ");

        var searchOption = Console.ReadLine();

        Product? product = null;

        if (searchOption == "1")
        {
            Console.Write("Ange ID för produkten som ska uppdateras: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                DisplayColourMessage("Ogiltigt ID. Försök igen.", ConsoleColor.Red);
                return;
            }

            product = await _currentFacade.GetProductByIdAsync(id);
        }
        else if (searchOption == "2")
        {
            var name = GetNonNullInput("Ange produktens namn: ");

            var products = await _currentFacade.SearchProductsAsync(name);
            if (!products.Any())
            {
                DisplayColourMessage("Ingen produkt hittades med det angivna namnet.", ConsoleColor.Red);
                return;
            }

            if (products.Count() > 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Flera produkter hittades:");
                foreach (var p in products)
                {
                    Console.WriteLine($"ID: {p.Id}, Namn: {p.Name}, Pris: {p.Price}, Lager: {p.Stock}");
                }
                Console.ResetColor();

                Console.Write("Ange ID för produkten du vill uppdatera: ");
                if (!int.TryParse(Console.ReadLine(), out var id))
                {
                    DisplayColourMessage("Ogiltigt ID. Försök igen.", ConsoleColor.Red);
                    return;
                }

                product = await _currentFacade.GetProductByIdAsync(id);
            }
            else
            {
                product = products.First();
            }
        }
        else
        {
            DisplayColourMessage("Ogiltigt alternativ. Försök igen.", ConsoleColor.Red);
            return;
        }

        if (product == null)
        {
            DisplayColourMessage("Produkten kunde inte hittas.", ConsoleColor.Red);
            return;
        }

        Console.WriteLine($"Uppdaterar produkt: ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price}, Lager: {product.Stock}");
        Console.Write("Ange nytt namn (eller tryck Enter för att behålla): ");
        var newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName)) product.Name = newName;

        Console.Write("Ange nytt pris (eller tryck Enter för att behålla): ");
        if (decimal.TryParse(Console.ReadLine(), out var newPrice)) product.Price = newPrice;

        Console.Write("Ange nytt lagerantal (eller tryck Enter för att behålla): ");
        if (int.TryParse(Console.ReadLine(), out var newStock)) product.Stock = newStock;

        await _currentFacade.UpdateProductAsync(product);
        DisplayColourMessage("Produkten har uppdaterats.", ConsoleColor.Green);
    }

    private async Task DeleteProduct()
	{
		Console.Write("Ange ID för produkten som ska tas bort: ");
		if (!int.TryParse(Console.ReadLine(), out var id))
		{
            DisplayColourMessage("Ogiltigt ID. Försök igen.", ConsoleColor.Red);
			return;
		}

		await _currentFacade.DeleteProductAsync(id);
        DisplayColourMessage("Produkten har tagits bort.", ConsoleColor.Green);
    }

    private async Task SearchProducts()
    {
        var searchTerm = GetNonNullInput("Ange sökterm: ");
        var products = await _currentFacade.SearchProductsAsync(searchTerm);

        if (products.Any())
        {
            DisplayColourMessage("=========================================", ConsoleColor.Cyan);
            DisplayColourMessage("            SÖKRESULTAT                 ", ConsoleColor.Cyan);
            DisplayColourMessage("=========================================", ConsoleColor.Cyan);

            Console.WriteLine("{0, -5} | {1, -20} | {2, -10} | {3, -10}", "ID", "Namn", "Pris", "Lager");
            Console.WriteLine(new string('-', 50));

            foreach (var product in products)
            {
                Console.WriteLine("{0, -5} | {1, -20} | {2, -10:C} | {3, -10}",
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Stock);
            }

            DisplayColourMessage("=========================================", ConsoleColor.Cyan);
        }
        else
        {
            DisplayColourMessage("Inga produkter hittades.", ConsoleColor.Red);
        }
    }

    private async Task ShowAllProducts()
	{
		var products = await _currentFacade.GetAllProductsAsync();

		if (products.Any())
		{
            DisplayColourMessage("=========================================", ConsoleColor.Cyan);
            DisplayColourMessage("            PRODUKTKATALOG              ", ConsoleColor.Cyan);
            DisplayColourMessage("=========================================", ConsoleColor.Cyan);

            Console.WriteLine("{0, -5} | {1, -20} | {2, -10} | {3, -10}", "ID", "Namn", "Pris", "Lager");
			Console.WriteLine(new string('-', 50));

			foreach (var product in products)
			{
				Console.WriteLine("{0, -5} | {1, -20} | {2, -10:C} | {3, -10}",
								  product.Id,
								  product.Name,
								  product.Price,
								  product.Stock);
			}

            DisplayColourMessage("=========================================", ConsoleColor.Cyan);

        }
        else
		{
            DisplayColourMessage("Inga produkter hittades.", ConsoleColor.Red);
        }
    }

    private static string GetNonNullInput(string prompt)
    {
        Console.Write(prompt);
		var input = Console.ReadLine();
		while (string.IsNullOrWhiteSpace(input))
		{
            DisplayColourMessage("Fältet får inte vara tomt. Försök igen.", ConsoleColor.Red);
            Console.Write(prompt);
            input = Console.ReadLine();
        }
		return input;
    }

	private static void DisplayColourMessage(string message, ConsoleColor colour)
    {
        Console.ForegroundColor = colour;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}