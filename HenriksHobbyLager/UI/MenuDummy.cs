using System;

namespace RefactoringExercise.UI;

using RefactoringExercise.Facades;

internal class MenuDummy
{
    private readonly IProductFacade _productFacade;

    public MenuDummy(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }
    internal void ShowMenu()
    {
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
    private void AddProduct()
    {
        Console.WriteLine("Skriv in produktens namn: ");
        var productName = Console.ReadLine();
        Console.WriteLine("Skriv in antal: ");
        var productQuantity = int.Parse(Console.ReadLine());
        Console.WriteLine("Skriv in priset på produkten: ");
        var productPrice = decimal.Parse(Console.ReadLine());
        _productFacade.AddProduct(productName, productQuantity, productPrice);
    }
    private void DeleteProduct()
    {
        Console.WriteLine("Skriv in produkt-id: ");
        var productId = int.Parse(Console.ReadLine());
        _productFacade.DeleteProduct(productId);
    }
    private void UpdateProduct()
    {
        Console.WriteLine("Skriv in produkt-id: ");
        var productId = int.Parse(Console.ReadLine());
        Console.WriteLine("Skriv in produktens namn: ");
        var productName = Console.ReadLine();
        Console.WriteLine("Skriv in antal: ");
        var productQuantity = int.Parse(Console.ReadLine());
        Console.WriteLine("Skriv in priset på produkten: ");
        var productPrice = decimal.Parse(Console.ReadLine());
        _productFacade.UpdateProduct(productId, productName, productQuantity, productPrice);
    }
    private void SearchProduct()
    {
        Console.WriteLine("Skriv in produkt-id: ");
        var productId = int.Parse(Console.ReadLine());
        var product = _productFacade.SearchProduct(productId);
        if (product != null)
        {
            Console.WriteLine($"Produktens namn: {product.Name}");
            Console.WriteLine($"Produktens antal: {product.Quantity}");
            Console.WriteLine($"Produktens pris: {product Price}");

        }
        else
        {
            Console.WriteLine("Produkten hittades inte.");
        }
    }

    private void ShowAllProducts()
    {
        var products = _productFacade.GetAllProducts();
        foreach (var product in products)
        {
            Console.WriteLine($"Produktens id: {product.Id}");
            Console.WriteLine($"Produktens namn: {product.Name}");
            Console.WriteLine($"Produktens antal: {product.Quantity}");
            Console.WriteLine($"Produktens pris: {product.Price}");
            Console.WriteLine();
        }
    }
}
