using HenriksHobbylager.Models;
using HenriksHobbylager.Interface;
using HenriksHobbyLager.UI;

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


		// var product = new Product { Name = name, Price = price, Stock = stock };
		await _currentFacade.CreateProductAsync(name, stock, price, category);
		ConsoleHelper.DisplayColourMessage("Produkten lades till.", ConsoleColor.Green);
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
				ConsoleHelper.DisplayColourMessage("Ogiltigt ID. Försök igen.", ConsoleColor.Red);
				return;
			}

			product = await _currentFacade.GetProductByIdAsync(id);
		}
		else if (searchOption == "2")
		{
			var name = ConsoleHelper.GetNonNullInput("Ange produktens namn: ");

			var products = await _currentFacade.SearchProductsAsync(name);
			if (!products.Any())
			{
				ConsoleHelper.DisplayColourMessage("Ingen produkt hittades med det angivna namnet.", ConsoleColor.Red);
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
					ConsoleHelper.DisplayColourMessage("Ogiltigt ID. Försök igen.", ConsoleColor.Red);
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
			ConsoleHelper.DisplayColourMessage("Ogiltigt alternativ. Försök igen.", ConsoleColor.Red);
			return;
		}

		if (product == null)
		{
			ConsoleHelper.DisplayColourMessage("Produkten kunde inte hittas.", ConsoleColor.Red);
			return;
		}

		Console.WriteLine(
			$"Uppdaterar produkt: ID: {product.Id}, Namn: {product.Name}, Kategori: {product.Category}, Pris: {product.Price}, Lager: {product.Stock} ");
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

	private async Task DeleteProduct()
	{
		Console.Write("Ange ID för produkten som ska tas bort: ");
		if (!int.TryParse(Console.ReadLine(), out var id))
		{
			ConsoleHelper.DisplayColourMessage("Ogiltigt ID. Försök igen.", ConsoleColor.Red);
			return;
		}

		await _currentFacade.DeleteProductAsync(id);
		ConsoleHelper.DisplayColourMessage("Produkten har tagits bort.", ConsoleColor.Green);
	}

	private async Task SearchProducts()
	{
		var searchTerm = ConsoleHelper.GetNonNullInput("Ange sökterm: ");
		var products = await _currentFacade.SearchProductsAsync(searchTerm);

		ShowSearchResults(products,"SÖKRESULTAT");
	}

	private async Task ShowAllProducts()
	{
		var products = await _currentFacade.GetAllProductsAsync();
		ShowSearchResults(products,"PRODUKTKATALOG");

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
					product.Id,
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