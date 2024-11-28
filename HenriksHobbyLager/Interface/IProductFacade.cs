using HenriksHobbylager.Models;

namespace HenriksHobbylager.Interface;

internal interface IProductFacade
{
    string DatabaseType { get; }
    Task CreateProductAsync(string productName, int productQuantity, decimal productPrice);
    Task DeleteProductAsync(int productId);

    
    Task UpdateProductAsync(int productId, string productName, int productQuantity, decimal productPrice);
    Task<Product> SearchProductAsync(int productId);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
