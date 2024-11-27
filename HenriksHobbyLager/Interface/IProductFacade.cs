using HenriksHobbylager.Models;

namespace HenriksHobbylager.Facades;

internal interface IProductFacade
{
    Task CreateProductAsync(string productName, int productQuantity, decimal productPrice); // Adds a new product
    Task DeleteProductAsync(int productId); // Deletes a product by ID
    Task UpdateProductAsync(int productId, string productName, int productQuantity, decimal productPrice); // Updates product details
    Task<Product> SearchProductAsync(int productId); // Fetches a product by ID
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm); // Searches for products by term
    Task<IEnumerable<Product>> GetAllProductsAsync(); // Retrieves all products
}
