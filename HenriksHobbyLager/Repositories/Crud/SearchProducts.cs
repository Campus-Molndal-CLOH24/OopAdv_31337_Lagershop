namespace HenriksHobbylager.Models;

public class SearchProducts
{
    // TODO: Check this to add nullchecks and category.
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
    
    //     // LINQ igen! Kollar både namn och kategori
    //     var results = _products.Where(p => 
    //         p.Name.ToLower().Contains(searchTerm) || 
    //         p.Category.ToLower().Contains(searchTerm)
    //     ).ToList();
    
    // TODO: Check to add more maby?
    // Snygga streck som separerar produkterna
    // Console.WriteLine($"\nID: {product.Id}");
    // Console.WriteLine($"Namn: {product.Name}");
    // Console.WriteLine($"Pris: {product.Price:C}");  // :C gör att det blir kronor automatiskt!
    // Console.WriteLine($"Lager: {product.Stock}");
    // Console.WriteLine($"Kategori: {product.Category}");
    // Console.WriteLine($"Skapad: {product.Created}");
    // if (product.LastUpdated.HasValue)  // Kollar om produkten har uppdaterats någon gång
    //     Console.WriteLine($"Senast uppdaterad: {product.LastUpdated}");
    // Console.WriteLine(new string('-', 40));  // Snyggt streck mellan produkterna
}