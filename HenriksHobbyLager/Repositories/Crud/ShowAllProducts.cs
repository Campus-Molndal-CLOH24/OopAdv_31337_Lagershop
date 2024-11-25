namespace HenriksHobbylager.Models;

public class ShowAllProducts
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