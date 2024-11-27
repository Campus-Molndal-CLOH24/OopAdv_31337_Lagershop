using HenriksHobbylager.Models;

namespace HenriksHobbylager.Repositories.Crud;

public class CreateProduct
{
    private readonly IRepository<Product> _repository = null!;
    // private readonly Repository repository;

    public CreateProduct(IRepository<Product> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    // Constructor chaining: allow for the use of the repository while trying to reduce repeated use of the this keyword
    //public CreateProduct(Repository repository) : this((IRepository<Product>)repository)
    //{
    //    this.repository = repository;
    //}

    public async Task CreateProductAsync()
    {
        try
        {
            Console.WriteLine("Ange produktnamn: ");
            var productName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Produktnamn får inte vara tomt. :)");

            Console.WriteLine("Skriv in produktens kategori: ");
            var category = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Kategorin får inte vara tom. :)");

            Console.Write("Ange antal: ");
            if (!int.TryParse(Console.ReadLine(), out var productStock) || productStock < 0)
                throw new ArgumentException("Lagersaldo måste vara ett giltigt icke-negativt tal.");

            Console.Write("Ange pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out var productPrice) || productPrice <= 0)
                throw new ArgumentException("Priset måste vara ett giltigt positivt tal.");

            var product = new Product
            {
                Name = productName,
                Price = productPrice,
                Stock = productStock,
                Category = category,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };

            // Skicka produkten till repositoryn för att lägga till den i databasen
            await _repository.AddAsync(product);
            Console.WriteLine($"Produkten {product.Name} har lagts till i databasen.");
        }
        catch (Exception exProductAdd)
        {
            Console.WriteLine($"Ett fel inträffade: {exProductAdd.Message}");
        }
    }
}