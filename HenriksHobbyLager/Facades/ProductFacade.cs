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
        DatabaseType = repository switch
        {
            SQLiteRepository => "SQLite",
            MongoRepository => "MongoDB",
            _ => "Okänd"
        };
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

        if (DatabaseType == "MongoDB" && string.IsNullOrEmpty(product._id))
        {
            product._id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(); // Sätt `_id` för MongoDB
        }

        await _repository.AddAsync(product);
    }

    public async Task DeleteProductAsync(string productId)
    {
        var product = await GetProductByIdInternalAsync(productId);
        if (product == null)
        {
            throw new ArgumentException($"Produkten med ID {productId} hittades ej.");
        }

        await _repository.DeleteAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        var productId = GetDatabaseSpecificId(product);
        if (string.IsNullOrEmpty(productId))
        {
            throw new InvalidOperationException("Produkten saknar ett giltigt ID.");
        }

        var existingProduct = await GetProductByIdInternalAsync(productId);
        if (existingProduct == null)
        {
            throw new InvalidOperationException("Produkten kunde inte hittas.");
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.Category = product.Category;

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
            (p.Category != null && p.Category.ToLower().Contains(lowerSearchTerm)));
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllAsync(p => true);
    }

    public async Task<Product?> GetProductByIdAsync(string productId)
    {
        return await GetProductByIdInternalAsync(productId);
    }

    private async Task<Product?> GetProductByIdInternalAsync(string productId)
    {
        return DatabaseType switch
        {
            "SQLite" when int.TryParse(productId, out var intId) => await _repository.GetByIdAsync(intId.ToString()),
            "MongoDB" => await _repository.GetByIdAsync(productId),
            _ => throw new InvalidOperationException("Databastypen är okänd eller ogiltig.")
        };
    }

    private string? GetDatabaseSpecificId(Product product)
    {
        return DatabaseType switch
        {
            "SQLite" => product.Id.ToString(),
            "MongoDB" => product._id,
            _ => null
        };
    }
}
