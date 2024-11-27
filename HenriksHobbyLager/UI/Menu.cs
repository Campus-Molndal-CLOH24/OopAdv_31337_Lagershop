namespace HenriksHobbylager.UI;

using HenriksHobbylager.Facades;

public class Menu
{
	private IProductFacade _productFacade;

	public void ShowMenu(IProductFacade productFacade)
	{
		_productFacade = productFacade;

		while (true)
		{
			Console.Clear();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("========== Välkommen till Min App ==========");
			Console.ResetColor();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Huvudmeny:");
			Console.ResetColor();
			Console.WriteLine("1. Lägg till en produkt");
			Console.WriteLine("2. Ta bort en produkt");
			Console.WriteLine("3. Uppdatera en produkt");
			Console.WriteLine("4. Sök igenom produkterna");
			Console.WriteLine("5. Visa alla produkterna");
			Console.WriteLine("6. Avsluta");
			Console.WriteLine();
			Console.Write("Välj ett alternativ: ");

			var option = Console.ReadLine();
			switch (option)
			{
				case "1":
					AddProduct();
					break;
				case "2":
					DeleteProduct();
					break;
				case "3":
					UpdateProduct();
					break;
				case "4":
					SearchProduct();
					break;
				case "5":
					ShowAllProducts();
					break;
				case "6":
					Console.WriteLine("Avslutar programmet...");
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Felaktigt val. Välj mellan alternativ 1-6.");
					Console.ReadKey();
					break;
			}
		}
	}

	private void AddProduct()
	{
		Console.WriteLine("Skriv in produktens namn: ");
		var productName = Console.ReadLine();
		Console.WriteLine("Skriv in antal: ");
		if (!int.TryParse(Console.ReadLine(), out var productQuantity))
		{
			Console.WriteLine("Ogiltigt antal.");
			return;
		}
		Console.WriteLine("Skriv in priset på produkten: ");
		if (!decimal.TryParse(Console.ReadLine(), out var productPrice))
		{
			Console.WriteLine("Ogiltigt pris.");
			return;
		}
		Console.WriteLine("Skriv in produktens kategori: ");
		var category = Console.ReadLine();

		_productFacade.AddProduct(productName, productQuantity, productPrice, category);
		Console.WriteLine("Produkten har lagts till.");
		Console.ReadKey();
	}

	private void DeleteProduct()
	{
		Console.WriteLine("Skriv in produkt-id att ta bort: ");
		if (!int.TryParse(Console.ReadLine(), out var productId))
		{
			Console.WriteLine("Ogiltigt ID.");
			return;
		}

		_productFacade.DeleteProduct(productId);
		Console.WriteLine("Produkten har tagits bort.");
		Console.ReadKey();
	}

	private void UpdateProduct()
	{
		Console.WriteLine("Skriv in produkt-id: ");
		if (!int.TryParse(Console.ReadLine(), out var productId))
		{
			Console.WriteLine("Ogiltigt ID.");
			return;
		}
		Console.WriteLine("Skriv in produktens nya namn: ");
		var productName = Console.ReadLine();
		Console.WriteLine("Skriv in antal: ");
		if (!int.TryParse(Console.ReadLine(), out var productQuantity))
		{
			Console.WriteLine("Ogiltigt antal.");
			return;
		}
		Console.WriteLine("Skriv in priset på produkten: ");
		if (!decimal.TryParse(Console.ReadLine(), out var productPrice))
		{
			Console.WriteLine("Ogiltigt pris.");
			return;
		}

		_productFacade.UpdateProduct(productId, productName, productQuantity, productPrice);
		Console.WriteLine("Produkten har uppdaterats.");
		Console.ReadKey();
	}

	private void SearchProduct()
	{
		Console.WriteLine("Skriv in produkt-id att söka efter: ");
		if (!int.TryParse(Console.ReadLine(), out var productId))
		{
			Console.WriteLine("Ogiltigt ID.");
			return;
		}

		var product = _productFacade.SearchProduct(productId);
		if (product != null)
		{
			Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Antal: {product.Stock}, Pris: {product.Price}");
		}
		else
		{
			Console.WriteLine("Produkten hittades inte.");
		}
		Console.ReadKey();
	}

	private void ShowAllProducts()
	{
		var products = _productFacade.GetAllProducts();
		Console.WriteLine("Alla produkter i databasen:");
		foreach (var product in products)
		{
			Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Antal: {product.Stock}, Pris: {product.Price}");
		}
		Console.ReadKey();
	}
}
