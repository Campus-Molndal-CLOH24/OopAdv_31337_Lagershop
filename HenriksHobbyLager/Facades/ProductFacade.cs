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

	public async Task<IEnumerable<Product>> GetAllProductsAsync()
	{
		return await _repository.GetAllAsync();
	}

	public async Task<Product> SearchProductAsync(int productId)
	{
		var product = await _repository.GetByIdAsync(productId);
		if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");

		return product;
	}

	public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
	{
		return await _repository.GetAllAsync(p =>
			p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
			p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
	}

	public async Task UpdateProductAsync(Product product)
	{
		var products = await _repository.GetByIdAsync(product.Id);
		if (products == null) throw new ArgumentException($"Product with ID {product.Id} not found.");
		var productss = new Product
		{
			Name = products.Name,
			Stock = products.Stock,
			Price = products.Price,
			LastUpdated = DateTime.Now

		};
		await _repository.UpdateAsync(productss);
	}

	public IEnumerable<Product> SearchProductAsync(string? term)
	{
		throw new NotImplementedException();
	}


}