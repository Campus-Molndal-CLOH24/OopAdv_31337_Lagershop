using HenriksHobbylager.Models;

namespace HenriksHobbylager.Repositories.Crud;

public class DeleteProduct
{
    private readonly IRepository<Product> _repository;
    private Repository repository;

    public DeleteProduct(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public DeleteProduct(Repository repository)
    {
        this.repository = repository;
    }

    public async Task DeleteProductAsync()
    {
        try
        {
            Console.WriteLine("Ange produkt som du vill ta bort: ");
            // Sök först produkten - kan vi återanvända koden från SearchProduct.cs?
            string deleteProductInput = Console.ReadLine();
            var searchProductToDelete = await _repository.SearchAsync(deleteProductInput);

            // Returnera/Skriv ut om produkten finns och hur många som finns
            if (searchProductToDelete != null)
            {
                // returnera en lista på vad och hur många som finns
                var productName = searchProductToDelete.Name;
                var productAmount = searchProductToDelete.Stock;
                Console.WriteLine($"Det finns {productAmount} stycken av {productName}.");
                Console.WriteLine("Hur många vill du ta bort?");
                string? deleteAmountInput = Console.ReadLine();
                return;
            }
            else
            {
                // Om produkten inte finns, skriv ut att produkten inte finns: DUH! :o
            }
        }
        catch (Exception exProductDelete)
        {
            Console.WriteLine($"Ett fel inträffade: {exProductDelete.Message}");
        }
    }
}

// namespace HenriksHobbylager.Repositories.Crud;
//
// public class DeleteProduct
// {
//     Console.WriteLine("Skriv in produkt-id: ");
//     // TODO: Look this over.
//
//     if (!int.TryParse(Console.ReadLine(), out int id))
//     {
//         Console.WriteLine("Ogiltigt ID! Bara siffror är tillåtna här.");
//         return;
//     }
//     var productId = int.Parse(Console.ReadLine());
//     if (product == null)
//     {
//         Console.WriteLine("Produkt hittades inte! Puh, inget blev raderat av misstag!");
//         return;
//     }
//     _productFacade.DeleteProduct(productId);
// }