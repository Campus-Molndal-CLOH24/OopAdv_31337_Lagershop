using System;
using System.Collections.Generic;
using System.Linq;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

namespace HenriksHobbylager.Facades;

internal class ProductFacade : IProductFacade
{
	private readonly IRepository<Product> _productRepository;

	public ProductFacade(IRepository<Product> productRepository)
	{
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

	public void AddProduct(string productName, int productQuantity, decimal productPrice, string category)
	{
		var product = new Product
		{
			Name = productName,
			Price = productPrice,
			Stock = productQuantity,
			Category = category,
			Created = DateTime.Now
		};
		_productRepository.CreateProduct(product);
	}

	public void DeleteProduct(int productId)
	{
		_productRepository.Delete(productId);
	}

	public void UpdateProduct(int productId, string productName, int productQuantity, decimal productPrice)
	{
		var product = _productRepository.Search(p => p.Id == productId).FirstOrDefault();
		if (product == null)
		{
			throw new ArgumentException("Produkten hittades inte.");
		}

		product.Name = productName;
		product.Stock = productQuantity;
		product.Price = productPrice;
		_productRepository.Update(product);
	}

	public Product SearchProduct(int productId)
	{
		return _productRepository.Search(p => p.Id == productId).FirstOrDefault()
			?? throw new ArgumentException("Produkten hittades inte.");
	}

	public IEnumerable<Product> GetAllProducts()
	{
		return _productRepository.GetAllAsync(p => true).Result; // Asynkront anrop omvandlat till synkront
	}

	public IEnumerable<Product> SearchProducts(string searchTerm)
	{
		return _productRepository.Search(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
	}
}
