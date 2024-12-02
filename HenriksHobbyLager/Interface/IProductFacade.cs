using HenriksHobbylager.Models;

namespace HenriksHobbylager.Interface;

internal interface IProductFacade
{
    string DatabaseType { get; } // SQLite or MongoDB

    Task CreateProductAsync(string productName, int productQuantity, decimal productPrice, string category);
    Task DeleteProductAsync(string productId);
    Task UpdateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(string productId); 
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm); 
    Task<IEnumerable<Product>> GetAllProductsAsync(); 
}