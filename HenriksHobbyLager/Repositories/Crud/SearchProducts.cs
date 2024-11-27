
using HenriksHobbylager.Models;

namespace HenriksHobbylager.Repositories.Crud
{
    internal class SearchProducts
    {
        private readonly IRepository<Product> _repository = null!;

        internal SearchProducts(IRepository<Product> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        internal async Task ExecuteSearchAsync()
        {
            try
            {
                Console.WriteLine("Vill du söka efter (1) Produktnamn eller (2) Kategori?");
                Console.Write("Ange ditt val (1 eller 2): ");
                var searchChoice = Console.ReadLine()?.Trim();

                IEnumerable<Product> products = Enumerable.Empty<Product>();

                switch (searchChoice)
                {
                    case "1":
                        Console.Write("Skriv in produktens namn: ");
                        var productName = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(productName))
                        {
                            // Search for product by name
                            products = await _repository.SearchAsync(p =>
                        p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase));
                        }
                        else
                        {
                            Console.WriteLine("Produktens namn kan inte vara tomt.");
                        }
                        break;

                    case "2":
                        Console.Write("Skriv in produktkategori: ");
                        var category = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(category))
                        {
                            products = await _repository.SearchAsync(p =>
                        p.Category.Contains(category, StringComparison.OrdinalIgnoreCase));
                        }
                        else
                        {
                            Console.WriteLine("Produktens kategori kan inte vara tom.");
                        }
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val. Välj 1 eller 2.");
                        return;
                }

                // Display search results
                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, " +
                            $"Kategori: {product.Category}, Pris: {product.Price:C}");
                    }
                }
                else
                {
                    Console.WriteLine("Inga produkter matchade din sökning.");
                }
            }
            catch (Exception searchFail)
            {
                Console.WriteLine($"Ett fel uppstod: {searchFail.Message}. Vänligen försök igen senare.");
            }
        }
    }
}