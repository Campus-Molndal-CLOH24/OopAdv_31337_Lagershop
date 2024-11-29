using HenriksHobbylager.Models;

namespace HenriksHobbylager.Interface;

internal interface IProductFacade
{
    string DatabaseType { get; }
    Task CreateProductAsync(string productName, int productQuantity, decimal productPrice);
    Task DeleteProductAsync(int productId);
    Task UpdateProductAsync(Product product);
    Task<Product> SearchProductAsync(int productId);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    IEnumerable<Product> SearchProductAsync(string? term);

}
