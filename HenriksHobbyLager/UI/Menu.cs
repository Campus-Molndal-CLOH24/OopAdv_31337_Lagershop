namespace HenriksHobbylager.UI;

using Facades;
using HenriksHobbylager.Repositories.Crud;
using HenriksHobbyLager.Repositories;
using HenriksHobbylager.Data;

internal class Menu
{
    private IProductFacade _productFacade = null!;

    internal async Task ShowMenu(IProductFacade productFacade)
    {
        _productFacade = productFacade;

        while (true)
        {
            Console.WriteLine("1. Lägg till en produkt");
            Console.WriteLine("2. Ta bort en produkt");
            Console.WriteLine("3. Uppdatera en produkt");
            Console.WriteLine("4. Sök igenom produkterna");
            Console.WriteLine("5. Visa alla produkterna");
            // TODO: "6. Ta bort en produktkategori" ?
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
                //case "6":
                //    await MenuChoiceSix();
                //    break;
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
            using var context = new AppDbContext();
            var repository = new SQLiteRepository(context);
            var createUnit = new CreateProduct(repository);
            await createUnit.CreateProductAsync();
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
            using var context = new AppDbContext();
            var repository = new SQLiteRepository(context);
            var deleteUnit = new DeleteProduct(repository);
            await deleteUnit.DeleteProductAsync();
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
            using var context = new AppDbContext();
            var repository = new SQLiteRepository(context);
            var searchUnit = new SearchProducts(repository);
            await searchUnit.ExecuteSearchAsync();
        }
        catch (Exception exInput)
        {
            Console.WriteLine($"Ett fel inträffade: {exInput.Message}");
        }
    }

    private async Task MenuChoiceFive()
    {
        try
        {
            using var context = new AppDbContext();
            var repository = new SQLiteRepository(context);
            var products = await repository.GetAllAsync();

            Console.WriteLine("Alla produkter:");
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
}