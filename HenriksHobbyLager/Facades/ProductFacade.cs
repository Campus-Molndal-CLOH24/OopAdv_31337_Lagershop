using HenriksHobbylager.Facades;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

namespace HenriksHobbyLager.Facades;

internal class ProductFacade : IProductFacade
{
    private readonly IRepository<Product> _sqliteRepository;
    private readonly IRepository<Product> _mongoRepository;

    private readonly bool _useMongo;

    public ProductFacade(IRepository<Product> sqliteRepository, IRepository<Product> mongoRepository, bool useMongo)
    {
        _sqliteRepository = sqliteRepository;
        _mongoRepository = mongoRepository;
        _useMongo = useMongo;
    }
    private IRepository<Product> Repository => _useMongo ? _mongoRepository : _sqliteRepository;

    // Sorting methods alphabetically for easier reading

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
        // await Repository.AddAsync(product);
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await Repository.GetByIdAsync(productId);
        if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");
        await Repository.DeleteAsync(productId);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync(p => true);

        // The code below was added when MongoDB was introduced. Do we want to keep it? Above returns all?
        //var product = await Repository.GetByIdAsync(productId);
        //if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");

        //product.Name = productName;
        //product.Stock = productQuantity;
        //product.Price = productPrice;
        //product.LastUpdated = DateTime.Now;

        //await Repository.UpdateAsync(product);
    }

    public async Task<Product> SearchProductAsync(int productId)
    {
        var product = await Repository.GetByIdAsync(productId);
        if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");
        return product;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        return await Repository.GetAllAsync(p =>
            p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }

    public async Task UpdateProductAsync(int productId, string productName, int productQuantity, decimal productPrice)
    {
        // The code below was added when MongoDB was introduced. Do we want to keep it? Above returns all?
        //var product = await _productRepository.GetByIdAsync(productId);
        //if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");

        //product.Name = productName;
        //product.Stock = productQuantity;
        //product.Price = productPrice;
        //product.LastUpdated = DateTime.Now;

        //await _productRepository.UpdateAsync(product);

        return await Repository.GetAllAsync(p => true);
    }
}