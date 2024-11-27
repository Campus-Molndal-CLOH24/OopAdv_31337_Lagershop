/* using System;
using HenriksHobbylager.Facades;

namespace HenriksHobbylager.Repositories.Crud
{
        public class UpdateProduct
        {
                private readonly IProductFacade _productFacade;

                public UpdateProduct(IProductFacade productFacade)
                {
                        _productFacade = productFacade ?? throw new ArgumentNullException(nameof(productFacade));
                }

                // Metod för att uppdatera produkt
                public void Execute()
                {
                        Console.WriteLine("Skriv in produkt-id: ");
                        if (!int.TryParse(Console.ReadLine(), out var productId))
                        {
                                Console.WriteLine("Ogiltigt produkt-id.");
                                return;
                        }

                        Console.WriteLine("Skriv in produktens namn: ");
                        var productName = Console.ReadLine();

                        Console.WriteLine("Skriv in antal: ");
                        if (!int.TryParse(Console.ReadLine(), out var productQuantity))
                        {
                                Console.WriteLine("Ogiltigt antal.");
                                return;
                        }

                        Console.WriteLine("Skriv in priset på produkten: ");
                        if (!decimal.TryParse(Console.ReadLine(), out var productPrice))
                        {
                                Console.WriteLine("Ogiltigt pris.");
                                return;
                        }

                        try
                        {
                                _productFacade.UpdateProduct(productId, productName, productQuantity, productPrice);
                                Console.WriteLine("Produkten uppdaterades framgångsrikt.");
                        }
                        catch (ArgumentException ex)
                        {
                                Console.WriteLine($"Fel: {ex.Message}");
                        }
                }
        }
}
 */