using HenriksHobbylager.Interface;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;
using HenriksHobbyLager.Repositories;

namespace HenriksHobbyLager.Facades;

internal class ProductFacade : IProductFacade
{
	private readonly IRepository<Product> _repository;
	public string DatabaseType { get; }

	public ProductFacade(IRepository<Product> repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		DatabaseType = repository.GetType().Name.Contains("SQLite") ? "SQLite" : "MongoDB";
	}

	public async Task CreateProductAsync(string productName, int productStock, decimal productPrice)
	{
		var product = new Product
		{
			Name = productName,
			Stock = productStock,
			Price = productPrice,
		};

		await _repository.AddAsync(product);
	}

	public async Task DeleteProductAsync(int productId)
	{
		var product = await _repository.GetByIdAsync(productId);
		if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");

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
			throw new ArgumentException("Search term cannot be null or empty.", nameof(searchTerm));
		}

		var lowerSearchTerm = searchTerm.ToLower();

		return await _repository.SearchAsync(p =>
			p.Name.ToLower().Contains(lowerSearchTerm) ||
			p.Category.ToLower().Contains(lowerSearchTerm));
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