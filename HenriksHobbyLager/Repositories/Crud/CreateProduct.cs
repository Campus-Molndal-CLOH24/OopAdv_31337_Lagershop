namespace HenriksHobbylager.Models;

private void CreateProduct()
{
    Console.WriteLine("Skriv in produktens namn: ");
    var productName = Console.ReadLine();
    System.Console.WriteLine("skriv in produkters kategori");
    var category = Console.ReadLine();
    Console.WriteLine("Skriv in antal: ");
    var productQuantity = int.Parse(Console.ReadLine());
    Console.WriteLine("Skriv in priset på produkten: ");
    var productPrice = decimal.Parse(Console.ReadLine());
    // _productFacade.AddProduct(productName, productQuantity, productPrice);

    // TODO: A
    var product = new Product
    {

        Name = productName,
        Price = productPrice,
        Stock = productQuantity,
        Category = category,
        Created = DateTime.Now,
        LastUpdated = DateTime.Now
    };
    // var category = Console.ReadLine();
}