namespace HenriksHobbylager.UI;

using Facades;
using HenriksHobbylager.Repositories.Crud;

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
                    // TODO: Needs parameters to work, make connections to CRUD.
                    break;
                case "4":
                    await MenuChoiceFour();
                    break;
                case "5":
                    // TODO: Needs parameters to work, make connections to CRUD.
                    break;
                case "6":
                    // TODO: Needs parameters to work, make connections to CRUD.
                    break;
                case "0":
                    // TODO: Needs parameters to work, make connections to CRUD.
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
            var repository = new Repository(new Data.AppDbContext());
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
            var repository = new Repository(new Data.AppDbContext());
            var deleteUnit = new DeleteProduct(repository);
            await deleteUnit.DeleteProductAsync();
        }
        catch (Exception exInput)
        {
            Console.WriteLine($"Ett fel inträffade: {exInput.Message}");
        }
    }

    private async Task MenuChoiceFour()
    {
        try
        {
            var repository = new Repository(new Data.AppDbContext());
            var searchUnit = new SearchProducts(repository);
            await searchUnit.ExecuteSearchAsync();
        }
        catch (Exception exInput)
        {
            Console.WriteLine($"Ett fel inträffade: {exInput.Message}");
        }
    }
}