namespace RefactoringExercise.Facades;

internal interface IProductFacade
{
    void AddProduct(string productName, int productQuantity, decimal productPrice);
    void DeleteProduct(int productId);
    void UpdateProduct(int productId, string productName, int productQuantity, decimal productPrice);
    Product SearchProduct(int productId);
    IEnumerable<Product> GetAllProducts();
}