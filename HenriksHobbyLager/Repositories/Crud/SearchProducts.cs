/* using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbylager.Repositories.Crud;

internal class SearchProducts
{
    private readonly IProductFacade _productFacade;
    {
        _productFacade = productFacade;
    }

    internal void ExecuteSearch()
    {
        Console.WriteLine("Skriv in söktermen (namn eller kategori): ");
        var searchTerm = Console.ReadLine().ToLower();

        IEnumerable<Product> products;

        if searchTerm != null ? products = _productFacade.SearchProduct(searchTerm) : products = _productFacade.GetAllProducts();
        else
        {
            Console.WriteLine("Produkten hittades inte.");
        }
    }
{ */

// TODO: Check this to add nullchecks and category.
//    Console.WriteLine("Skriv in produkt-id: ");
//    var productId = int.Parse(Console.ReadLine());
//    var product = _productFacade.SearchProduct(productId);

//    Console.WriteLine("Skriv in söktermen (namn eller id): ");
//var searchTerm = int.Parse(Console.ReadLine());

//    var product = _productFacade.SearchProduct(searchTerm);
//if (product.Any())


//    foreach (var product in product)
//    {
//        Console.WriteLine($"Produktens id: {product.Id}");
//        Console.WriteLine($"Produktens namn: {product.Name}");
//        Console.WriteLine($"Produktens antal: {product.Quantity}");
//        Console.WriteLine($"Produktens pris: {product.Price}");
//        Console.WriteLine($"Skapad: {product.Created}");
//        Console.WriteLine();
//    }
//}
//else
//{
//    Console.WriteLine("Inga produkter matchade din sökning.");
//}

//if (product != null)
//    {
//        Console.WriteLine($"Produktens namn: {product.Name}");
//        Console.WriteLine($"Produktens antal: {product.Quantity}");
//        Console.WriteLine($"Produktens pris: {product Price}");

//    }
//    else
//    {
//        Console.WriteLine("Produkten hittades inte.");
//    }

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
//}