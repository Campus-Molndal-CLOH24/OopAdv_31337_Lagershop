using HenriksHobbylager.Models;
using HenriksHobbylager.Interface;

namespace HenriksHobbylager.UI;
internal class MenuCrud
{
	private readonly IProductFacade _currentFacade;
	private IProductFacade sqliteFacade;
	private IProductFacade mongoFacade;

	public MenuCrud(IProductFacade currentFacade, IProductFacade sqliteFacade = null, IProductFacade mongoFacade = null)
	{
		_currentFacade = currentFacade ?? throw new ArgumentNullException(nameof(currentFacade));
		this.sqliteFacade = sqliteFacade ?? currentFacade;
		this.mongoFacade = mongoFacade ?? currentFacade;
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
					MenuDb menuDb = new MenuDb(sqliteFacade, mongoFacade);
					await menuDb.ShowMainMenuAsync();
					break;
				
				case "0":
					Console.WriteLine("Programmet avslutas. Tack för att du använde Henriks Hobbylager!");
					Environment.Exit(0);
					break;
				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Felaktigt val. Försök igen.");
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
					Console.WriteLine("Programmet avslutas. Tack för att du använde Henriks Hobbylager!");
					Environment.Exit(0);
				}
				else if (choice != "1")
				{
					Console.WriteLine("Felaktigt val. Programmet avslutas.");
					Environment.Exit(0);
				}
			}
		}
	}

	private static void DisplayMenuHeader()
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("========================================");
		Console.WriteLine("         Henriks Hobbylager MenuCrud        ");
		Console.WriteLine("========================================");
		Console.ResetColor();
	}

	private async Task AddProduct()
	{
		Console.WriteLine("Ange productnamn:");
		var name = Console.ReadLine();

		Console.WriteLine("Ange pris:");
		if (!decimal.TryParse(Console.ReadLine(), out var price))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Ogiltigt pris. Försök igen.");
			Console.ResetColor();
			return;
		}

		Console.WriteLine("Ange lagerantal:");
		if (!int.TryParse(Console.ReadLine(), out var stock))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Ogiltigt lagerantal. Försök igen.");
			Console.ResetColor();
			return;
		}

		var product = new Product { Name = name, Price = price, Stock = stock };
		await _currentFacade.CreateProductAsync(product.Name, product.Stock, product.Price);
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Produkten lades till.");
		Console.ResetColor();
	}

    private async Task UpdateProduct()
    {
        Console.WriteLine("Vill du söka efter produkt med:");
        Console.WriteLine("1. Produkt-ID");
        Console.WriteLine("2. Produktnamn");
        Console.Write("Välj alternativ: ");

        var searchOption = Console.ReadLine();

        Product product = null;

        if (searchOption == "1")
        {
            Console.Write("Ange ID för produkten som ska uppdateras: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt ID. Försök igen.");
                Console.ResetColor();
                return;
            }

            product = await _currentFacade.GetProductByIdAsync(id);
        }
        else if (searchOption == "2")
        {
            Console.Write("Ange produktens namn: ");
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Produktnamnet får inte vara tomt. Försök igen.");
                Console.ResetColor();
                return;
            }

            var products = await _currentFacade.SearchProductsAsync(name);
            if (!products.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingen produkt hittades med det angivna namnet.");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltigt ID. Försök igen.");
                    Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt alternativ. Försök igen.");
            Console.ResetColor();
            return;
        }

        if (product == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Produkten kunde inte hittas.");
            Console.ResetColor();
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
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Produkten har uppdaterats.");
        Console.ResetColor();
    }

    private async Task DeleteProduct()
	{
		Console.Write("Ange ID för produkten som ska tas bort: ");
		if (!int.TryParse(Console.ReadLine(), out var id))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Ogiltigt ID. Försök igen.");
			Console.ResetColor();
			return;
		}

		await _currentFacade.DeleteProductAsync(id);
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Produkten har tagits bort.");
		Console.ResetColor();
	}

    private async Task SearchProducts(bool forUpdate = false)
    {
        Console.Write("Ange sökterm: ");
        var searchTerm = Console.ReadLine();

        var products = await _currentFacade.SearchProductsAsync(searchTerm);

        if (!products.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Inga produkter hittades.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("{0, -5} | {1, -20} | {2, -10} | {3, -10}", "ID", "Namn", "Pris", "Lager");
        Console.WriteLine(new string('-', 50));

        foreach (var product in products)
        {
            Console.WriteLine("{0, -5} | {1, -20} | {2, -10:C} | {3, -10}",
                              product.Id, product.Name, product.Price, product.Stock);
        }

        if (forUpdate)
        {
            Console.Write("Ange ID för produkten du vill uppdatera: ");
            if (int.TryParse(Console.ReadLine(), out var id))
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    await UpdateProduct(); // Anropa uppdateringsflödet
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Produkten kunde inte hittas.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltigt ID. Försök igen.");
                Console.ResetColor();
            }
        }
    }


    private async Task ShowAllProducts()
	{
		var products = await _currentFacade.GetAllProductsAsync();

		if (products.Any())
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("=========================================");
			Console.WriteLine("            PRODUKTKATALOG              ");
			Console.WriteLine("=========================================");
			Console.ResetColor();

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

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("=========================================");
			Console.ResetColor();
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Inga produkter hittades.");
			Console.ResetColor();
		}
	}
}