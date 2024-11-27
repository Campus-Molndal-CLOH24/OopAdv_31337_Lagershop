﻿using HenriksHobbylager.Models;

namespace HenriksHobbylager.Facades;

internal interface IProductFacade
{
    Task CreateProductAsync(string productName, int productQuantity, decimal productPrice); 
    Task DeleteProductAsync(int productId);
    Task UpdateProductAsync(int productId, string productName, int productQuantity, decimal productPrice); 
    Task<Product> SearchProductAsync(int productId); 
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<Product>> GetAllProductsAsync(); 
}
