namespace HenriksHobbylager.UI;

public class Menu
{
    internal void ShowMenu(IProductFacade productFacade)
    {
        _productFacade = productFacade;
        
        Console.WriteLine("1. Lägg till en produkt");
        Console.WriteLine("2. Ta bort en produkt");
        Console.WriteLine("3. Uppdatera en produkt");
        Console.WriteLine("4. Sök igenom produkterna");
        Console.WriteLine("5. Visa alla produkterna");
        Console.WriteLine("6. Avsluta");
        Console.WriteLine("Välj ett alternativ: ");
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