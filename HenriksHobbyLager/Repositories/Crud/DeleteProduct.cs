using HenriksHobbylager.Models;

namespace HenriksHobbylager.Repositories.Crud;

public class DeleteProduct
{
    private readonly IRepository<Product> _repository;

    public DeleteProduct(IRepository<Product> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task DeleteProductAsync()
    {
        try
        {
            Console.WriteLine("Ange produktnamn som du vill ta bort: ");
            var productName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(productName))
            {
                Console.WriteLine("Produktnamn kan inte vara tomt.");
                return;
            }

            var products = await _repository.SearchAsync(p =>
                p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase));

            if (!products.Any())
            {
                Console.WriteLine("Ingen produkt matchade söktermen.");
                return;
            }

            foreach (var product in products)
            {
                await _repository.DeleteAsync(product);
                Console.WriteLine($"Produkten {product.Name} togs bort.");
            }
        }
        catch (Exception exProductDelete)
        {
            Console.WriteLine($"Ett fel inträffade: {exProductDelete.Message}");
        }
    }
}