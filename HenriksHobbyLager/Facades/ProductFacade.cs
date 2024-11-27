using HenriksHobbylager.Facades;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

internal class ProductFacade : IProductFacade
{
    private readonly IRepository<Product> _productRepository;

    public ProductFacade(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task CreateProductAsync(string productName, int productStock, decimal productPrice)
    {
        var product = new Product
        {
            Name = productName,
            Stock = productStock,
            Price = productPrice,
            Created = DateTime.Now,
            LastUpdated = DateTime.Now
        };

        await _productRepository.AddAsync(product);
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");
        await _productRepository.DeleteAsync(productId);
    }

    public async Task UpdateProductAsync(int productId, string productName, int productQuantity, decimal productPrice)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");
        
        product.Name = productName;
        product.Stock = productQuantity;
        product.Price = productPrice;
        product.LastUpdated = DateTime.Now;

        await _productRepository.UpdateAsync(product);
    }

    public async Task<Product> SearchProductAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");
        return product;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        return await _productRepository.GetAllAsync(p =>
            p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync(p => true);
    }
}
