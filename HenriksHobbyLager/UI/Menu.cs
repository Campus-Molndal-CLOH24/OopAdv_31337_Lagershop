namespace HenriksHobbylager.UI;

using Facades;
using HenriksHobbylager.Repositories.Crud;

internal class Menu
{
    private IProductFacade _productFacade;

    internal void ShowMenu(IProductFacade productFacade)
    {
        _productFacade = productFacade;

        while (true)
        {
            Console.WriteLine("1. Lägg till en produkt");
            Console.WriteLine("2. Ta bort en produkt");
            Console.WriteLine("3. Uppdatera en produkt");
            Console.WriteLine("4. Sök igenom produkterna");
            Console.WriteLine("5. Visa alla produkterna");
            // TODO: Ta bort en produktkategori?
            Console.WriteLine("0. Avsluta");
            Console.WriteLine("Välj ett alternativ: ");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    MenuChoiceOne();
                    break;
                case "2":
                    MenuChoiceTwo();
                    break;
                case "3":
                    // TODO: Needs parameters to work, make connections to CRUD.
                    // _productFacade.UpdateProductAsync();
                    break;
                case "4":
                    await MenuChoiceFour();
                    break;
                case "5":
                    // TODO: Needs parameters to work, make connections to CRUD.
                    // _productFacade.GetAllProductsAsync();
                    break;
                case "6":
                    // TODO: Needs parameters to work, make connections to CRUD.
                    // _productFacade.GetAllProductsAsync();
                    break;
                case "0":
                    // TODO: Needs parameters to work, make connections to CRUD.
                    Console.WriteLine("Tryck valfri knapp för att avsluta.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Felaktigt val. Välj mellan alternativ 1-6.");
                    break;
            }
        }
    }

    private async void MenuChoiceOne()
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

    private async void MenuChoiceTwo()
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