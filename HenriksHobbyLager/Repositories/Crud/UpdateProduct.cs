namespace HenriksHobbylager.Models;

public class UpdateProduct
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
        
        // TODO: Add LINQ?

}