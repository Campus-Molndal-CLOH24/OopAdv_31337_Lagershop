namespace HenriksHobbylager.UI;

using HenriksHobbyLager.Facades;
using HenriksHobbylager.Repositories.Crud;
using HenriksHobbyLager.Repositories;
using HenriksHobbylager.Data;
using HenriksHobbylager.Interface;



internal class Menu
{
	private readonly IProductFacade _productFacade = null!;
	public Menu(IProductFacade productFacade)
	{
		_productFacade = productFacade ?? throw new ArgumentNullException(nameof(productFacade));
	}

	internal async Task ShowMenu()
	{
		while (true)
		{
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
					ShowAllProducts();
					break;
				case "3":
					// await MenuChoiceThree();
					break;
				case "4":
					/* MenuChoiceFour(); */
					break;
				case "5":
					/* MenuChoiceFive(); */
					break;
				case "6":
					/* 	MenuChoiceSix(); */
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

	private void AddProduct()
	{
		Console.WriteLine("ange productnamn");
		var name = Console.ReadLine();

		Console.Write("Ange pris");
		var price = double.Parse(Console.ReadLine());

		Console.Write("Ange lagerantal");
		var stock = int.Parse(Console.ReadLine());
	}

	private async void ShowAllProducts()
	{
		var products = await _productFacade.GetAllProductsAsync();
		foreach (var product in products)
		{
			Console.WriteLine($"{product.Id}, Name: {product.Name}");
		}
	}
}