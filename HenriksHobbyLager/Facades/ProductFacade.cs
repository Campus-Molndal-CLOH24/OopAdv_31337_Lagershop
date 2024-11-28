using HenriksHobbylager.Interface;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

namespace HenriksHobbyLager.Facades;

internal class ProductFacade : IProductFacade
{
	private readonly IRepository<Product> _repository;

	public ProductFacade(IRepository<Product> repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
	}

	public async Task CreateProductAsync(string productName, int productStock, decimal productPrice)
	{
		var product = new Product
		{
			Name = productName,
			Stock = productStock,
			Price = productPrice,
			Created = DateTime.Now,
			// LastUpdated = DateTime.Now
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

	public async Task UpdateProductAsync(int productId, string productName, int productQuantity, decimal productPrice)
	{
		var product = await _repository.GetByIdAsync(productId);
		if (product == null) throw new ArgumentException($"Product with ID {productId} not found.");

		product.Name = productName;
		product.Stock = productQuantity;
		product.Price = productPrice;
		product.LastUpdated = DateTime.Now;

		await _repository.UpdateAsync(product);
	}
}