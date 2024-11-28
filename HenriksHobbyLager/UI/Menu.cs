namespace HenriksHobbylager.UI;

using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories.Crud;
using HenriksHobbyLager.Repositories;
using HenriksHobbylager.Data;
using HenriksHobbylager.Interface;



internal class Menu
{
	private readonly IProductFacade _currentFacade;

	public Menu(IProductFacade currentFacade)
	{
		_currentFacade = currentFacade ?? throw new ArgumentNullException(nameof(currentFacade));	}
	
	internal async Task ShowMenu()
	{
		while (true)
		{
			Console.WriteLine($"Använder: {_currentFacade.DatabaseType}.");
			Console.WriteLine("1. Lägg till en produkt");
			Console.WriteLine("2. Ta bort en produkt");
			Console.WriteLine("3. Uppdatera en produkt");
			Console.WriteLine("4. Sök igenom produkterna");
			Console.WriteLine("5. Visa alla produkterna");
			// TODO: "6-7. Ta bort en produktkategori" ?
			Console.WriteLine("6. Gå tillbaka till databasmenyn");
			Console.WriteLine("0. Avsluta");
			Console.WriteLine("Välj ett alternativ: ");

			var menuOption = Console.ReadLine();
			switch (menuOption)
			{
				case "1":
					AddProduct();
					break;
				case "2":
					break;
				case "3":
					break;
				case "4":
					break;
				case "5":
					ShowAllProducts();
					break;
				case "6":
					break;
				case "0":
					Console.WriteLine("Tryck valfri knapp för att avsluta.");
					Console.ReadKey();
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Felaktigt val. Välj mellan alternativ 1-5, eller 0 för att avsluta.");
					break;
			}
		}
	}

	private async void AddProduct()
	{
		Console.WriteLine("Ange productnamn:");
		var name = Console.ReadLine();

		Console.WriteLine("Ange pris:");
		var price = decimal.Parse(Console.ReadLine());

		Console.WriteLine("Ange lagerantal:");
		var stock = int.Parse(Console.ReadLine());
		
		await _currentFacade.CreateProductAsync(name, stock, price);
		Console.WriteLine("Produkten lades till.");
	}

	private async void ShowAllProducts()
	{
		var products = await _currentFacade.GetAllProductsAsync();
		foreach (var product in products)
		{
			Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price:C}, Lager: {product.Stock}");
		}
	}
}