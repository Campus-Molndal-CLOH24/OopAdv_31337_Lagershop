using HenriksHobbylager.Interface;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

namespace HenriksHobbyLager.Facades;

internal class ProductFacade : IProductFacade
{
    private readonly IRepository<Product> _repository;
    public string DatabaseType { get; }

    public ProductFacade(IRepository<Product> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        DatabaseType = repository is SQLiteRepository ? "SQLite" :
               repository is MongoRepository ? "MongoDB" : "Okänd"; // We added "okänd" to handle unknown database types
    }

    public async Task CreateProductAsync(string productName, int productStock, decimal productPrice, string category)
    {
        var product = new Product
        {
            Name = productName,
            Stock = productStock,
            Price = productPrice,
            Category = category
        };

        await _repository.AddAsync(product);
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await _repository.GetByIdAsync(productId);
        if (product == null) throw new ArgumentException($"Produkten med ID {productId} hittades ej.");

        await _repository.DeleteAsync(productId);
    }

    public async Task UpdateProductAsync(Product product)
    {
        var existingProduct = await _repository.GetByIdAsync(product.Id);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Produkten kunde inte hittas.");
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;

        await _repository.UpdateAsync(existingProduct);
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentException("Söktermen kan inte vara null eller tom.", nameof(searchTerm));
        }

        var lowerSearchTerm = searchTerm.ToLower();

        return await _repository.SearchAsync(p =>
            p.Name.ToLower().Contains(lowerSearchTerm) ||
            (p.Category != null && p.Category.ToLower().Contains(lowerSearchTerm))); // Category can be null
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllAsync(p => true);
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _repository.GetByIdAsync(productId);
    }
}