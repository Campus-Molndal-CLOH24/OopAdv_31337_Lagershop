namespace HenriksHobbylager.UI;

using Facades;

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
        Console.WriteLine("0. Avsluta");
        Console.WriteLine("Välj ett alternativ: ");

        var option = Console.ReadLine();
        switch (option)
        {
            case "1":
                // TODO: Needs parameters to work, make connections to CRUD.
                // _productFacade.CreateProductAsync();
                break;
            case "2":
                // TODO: Needs parameters to work, make connections to CRUD.
                // _productFacade.DeleteProductAsync();
                break;
            case "3":
                // TODO: Needs parameters to work, make connections to CRUD.
                // _productFacade.UpdateProductAsync();
                break;
            case "4":
                // TODO: Needs parameters to work, make connections to CRUD.
                // _productFacade.SearchProductAsync();
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
}