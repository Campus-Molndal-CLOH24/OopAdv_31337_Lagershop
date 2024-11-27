using HenriksHobbylager.Models;

namespace HenriksHobbylager.Facades;

public interface IProductFacade
{
	void AddProduct(string productName, int productQuantity, decimal productPrice, string category);
	void DeleteProduct(int productId);
	void UpdateProduct(int productId, string productName, int productQuantity, decimal productPrice);
	Product SearchProduct(int productId);
	IEnumerable<Product> GetAllProducts();
	IEnumerable<Product> SearchProducts(string searchTerm); // Tillagt för mer användarvänlighet
}
