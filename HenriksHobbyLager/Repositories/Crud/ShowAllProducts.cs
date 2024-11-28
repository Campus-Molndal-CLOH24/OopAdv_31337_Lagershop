using HenriksHobbylager.Facades;
using HenriksHobbyLager.Facades;

namespace HenriksHobbylager.Repositories.Crud;

public class ShowAllProducts
{
    private readonly IProductFacade _productFacade;

    internal ShowAllProducts(IProductFacade productFacade)
    {
        _productFacade = productFacade ?? throw new ArgumentNullException(nameof(productFacade));
    }

    public async Task DisplayAllProductsAsync()
    {
        var products = await _productFacade.GetAllProductsAsync();

        if (products.Any())
        {
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Stock: {product.Stock}, Price: {product.Price}");
            }
        }
        else
        {
            Console.WriteLine("No products found.");
        }
    }
 }