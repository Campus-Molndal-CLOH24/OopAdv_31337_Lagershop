/* namespace HenriksHobbylager.Models;

private void CreateProduct(Product product)
{
    Console.WriteLine("Skriv in produktens namn: ");
    var productName = Console.ReadLine();
    System.Console.WriteLine("skriv in produkters kategori");
    var category = Console.ReadLine();
    Console.WriteLine("Skriv in antal: ");
    var productQuantity = int.Parse(Console.ReadLine());
    Console.Writ/*  eLine("Skriv in priset på produkten: ");
    var productPrice = decimal.Parse(Console.ReadLine()); 
     _productFacade.AddProduct(productName, productQuantity, productPrice);

    Console.WriteLine("Skriv in priset på produkten: ");
    if (!decimal.TryParse(Console.ReadLine(), out var productPrice))
    {
        Console.WriteLine("Felaktigt pris, vänligen försök igen.");
        return;
    }

    var product = new Product
    {
        Name = productName,
        Price = productPrice,
        Stock = productQuantity,
        Category = category,
        Created = DateTime.Now,
        LastUpdated = DateTime.Now
    };

    // Skicka produkten till repositoryn för att lägga till den i databasen
    _repository.CreateProduct(product);
    Console.WriteLine($"Produkten {product.Name} har lagts till i databasen.");
}
*/