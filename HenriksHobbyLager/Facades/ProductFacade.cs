using System;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

namespace HenriksHobbylager.Facades;

using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;
using System.Collections;

internal class ProductFacade : IProductFacade
{
	private readonly IRepository<Product> _productRepository;
    private int _nextId = 1; // Start ID counter from 1

    public ProductFacade(IAsyncResult productRepository)
    {
        _productRepository = productRepository;
    }

    internal void AddProduct(string productName, int productQuantity, decimal productPrice)
    {
        var product = new Product
        {
            Id = _nextId++,
            Name = productName,
            Quantity = productQuantity,
            Price = productPrice
            Created = DateTime.Now
        };
        _productRepository.Add(product);
    }

    internal void DeleteProduct(int productId)
    {
        _productRepository.Delete(productId);
    }

    internal void UpdateProduct(int productId, string productName, int productQuantity, decimal productPrice)
    {
        var product = _productRepository.Get(productId);
        if (product != null)
        {
        product.Name = productName;
        product.Quantity = productQuantity;
        product.Price = productPrice;
        _productRepository.Update(product);
        }
        else
        {
            throw new ArgumentException("Product not found");
        }
    }

    internal Product SearchProduct(int productId)
    {
        return _productRepository.Get(productId);
    }

    internal IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAll();
    }
}
