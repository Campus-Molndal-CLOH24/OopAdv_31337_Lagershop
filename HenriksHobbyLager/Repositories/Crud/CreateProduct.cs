using HenriksHobbylager.Models;

namespace HenriksHobbylager.Repositories.Crud;

public class CreateProduct
{
    private readonly IRepository<Product> _repository;
    private Repository repository;

    public CreateProduct(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public CreateProduct(Repository repository)
    {
        this.repository = repository;
    }

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

            Console.WriteLine("Ange antal: ");
            var productStock = int.Parse(Console.ReadLine());
            if (productStock < 0)
                throw new ArgumentException("Lagersaldo får inte vara negativt. :(");

            Console.WriteLine("Ange pris: ");
            var productPrice = decimal.Parse(Console.ReadLine());
            if (productPrice <= 0)
                throw new ArgumentException("Priset måste vara mer än noll! Ger du bort alla helikoptrar?!");

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