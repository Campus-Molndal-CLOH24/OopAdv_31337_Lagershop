namespace HenriksHobbylager.UI;

using Facades;
using HenriksHobbylager.Repositories.Crud;
using HenriksHobbyLager.Repositories;
using HenriksHobbylager.Data;

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
                    await MenuChoiceOne();
                    break;
                case "2":
                    await MenuChoiceTwo();
                    break;
                case "3":
                    // await MenuChoiceThree();
                    break;
                case "4":
                    await MenuChoiceFour();
                    break;
                case "5":
                    await MenuChoiceFive();
                    break;
                case "6":
                    await MenuChoiceSix();
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

    private async Task MenuChoiceOne()
    {
        try
        {
            await _productFacade.CreateProductAsync("Produktnamn", 10, 199.99m);
        }
        catch (Exception exInput)
        {
            Console.WriteLine($"Ett fel inträffade: {exInput.Message}");
        }
    }

    private async Task MenuChoiceTwo()
    {
        try
        {
            await _productFacade.DeleteProductAsync(1);
        }
        catch (Exception exInput)
        {
            Console.WriteLine($"Ett fel inträffade: {exInput.Message}");
        }
    }

    //private async Task MenuChoiceThree()
    //{
    //    try
    //    {
    //        var repository = new SQLiteRepository(new Data.AppDbContext());
    //        var updateUnit = new UpdateProduct(repository);
    //        await updateUnit.UpdateProductAsync();
    //    }
    //    catch (Exception exInput)
    //    {
    //        Console.WriteLine($"Ett fel inträffade: {exInput.Message}");
    //    }
    //}

    private async Task MenuChoiceFour()
    {
        try
        {
            var showAllProducts = new ShowAllProducts(_productFacade);
            await showAllProducts.DisplayAllProductsAsync();
        }
        catch (Exception exFetchAll)
        {
            Console.WriteLine($"Ett fel inträffade: {exFetchAll.Message}");
        }
    }

    private async Task MenuChoiceFive()
    {
        try
        {
            var products = await _productFacade.GetAllProductsAsync();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price:C}, Lager: {product.Stock}");
            }
        }
        catch (Exception exInput)
        {
            Console.WriteLine($"Ett fel inträffade: {exInput.Message}");
        }
    }

    private async Task MenuChoiceSix()
    {
        // TODO; Vi satte ShowMenu i meny så länge, den skall senare anropa databasmenyn när vi har brutit ut den
        await ShowMenu();
    }
}