
using HenriksHobbylager.Facades;
using HenriksHobbylager.Models;

namespace HenriksHobbylager.Repositories.Crud
{
    internal class SearchProducts(IProductFacade productFacade) 
    {
        //private readonly IProductFacade _productFacade = productFacade ?? throw new ArgumentNullException(nameof(productFacade));

        private readonly IRepository<Product> _repository;

        //public SearchProducts(Repository repository)
        //{
        //    Repository = repository;
        //}

        public SearchProducts(IRepository<Product> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //public SearchProducts(Repository repository)
        //{
        //    this.repository = repository;
        //}

        //public Repository repository { get; }

        public async Task ExecuteSearchAsync()
        {
            try
            {
                Console.WriteLine("Vill du söka efter (1) Produkt-ID eller (2) Namn?");
                Console.Write("Ange ditt val (1 eller 2): ");
                var choice = Console.ReadLine()?.Trim();

                IEnumerable<Product> products = Enumerable.Empty<Product>();
                // IEnumerable<Product> products = new List<Product>();

                switch (choice)
                {
                    case "1":
                        Console.Write("Skriv in produktens namn: ");
                        // var idInput = Console.ReadLine()?.Trim();
                        // if (int.TryParse(idInput, out int productId))
                        if (int.TryParse(Console.ReadLine(), out var productName))
                        {
                            var product = await _repository.GetByNameAsync(productName);
                            if (product != null)
                            {
                                products = new List<Product> { product };
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt produkt-id.");
                                return;
                            }
                        }
                        break;

                    case "2":
                        Console.Write("Skriv in söktermen (namn eller kategori): ");
                        var searchTerm = Console.ReadLine()?.Trim().ToLower();
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                           Console.WriteLine("Söktermen kan inte vara tom.");
                            return;
                        } 
                        break;   

                    default:
                        Console.WriteLine("Ogiltigt val.");
                        return;
                }

                if (products != null && products.Any())
                {
                    Console.WriteLine("Hittade följande produkter:");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"Produktens ID: {product.Id}");
                        Console.WriteLine($"Produktens namn: {product.Name}");
                        Console.WriteLine($"Produktens antal: {product.Stock}");
                        Console.WriteLine($"Produktens pris: {product.Price:C}");
                        Console.WriteLine($"Skapad: {product.Created}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                else
                {
                    Console.WriteLine("Inga produkter matchade din sökning.");
                }
            }
            catch (Exception )
            {
                
                Console.WriteLine("Ett fel uppstod. Vänligen försök igen senare.");
            }
        }
    }
}

// using System.Linq;
//
// namespace HenriksHobbylager.Repositories.Crud;
//
// internal class SearchProducts
// {
//     private readonly IProductFacade _productFacade;
//     {
//         _productFacade = productFacade;
//     }
//
//     internal void ExecuteSearch()
//     {
//         Console.WriteLine("Skriv in söktermen (namn eller kategori): ");
//         var searchTerm = Console.ReadLine().ToLower();
//
//         IEnumerable<Product> products;
//
//         if searchTerm != null ? products = _productFacade.SearchProduct(searchTerm) : products = _productFacade.GetAllProducts();
//         else
//         {
//             Console.WriteLine("Produkten hittades inte.");
//         }
//     }
// {

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

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
//
// namespace HenriksHobbylager.Repositories.Crud;
//
// internal class SearchProducts
// {
//     private readonly IProductFacade _productFacade;
//     {
//         _productFacade = productFacade;
//     }
//
//     internal void ExecuteSearch()
//     {
//         Console.WriteLine("Skriv in söktermen (namn eller kategori): ");
//         var searchTerm = Console.ReadLine().ToLower();
//
//         IEnumerable<Product> products;
//
//         if searchTerm != null ? products = _productFacade.SearchProduct(searchTerm) : products = _productFacade.GetAllProducts();
//         else
//         {
//             Console.WriteLine("Produkten hittades inte.");
//         }
//     }
// {

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