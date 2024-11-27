using HenriksHobbylager.Data;
using HenriksHobbylager.UI;

class Program
{
    static async Task Main(string[] args)
    {
        // Create the database and tables if they don't exist.
        using var context = new AppDbContext();
        context.Database.EnsureCreated();

        // Create a repository and a facade to handle the CRUD operations.
        var productRepository = new Repository(context);
        var productFacade = new ProductFacade(productRepository);

        // Show the main menu.
        var menu = new Menu();
        await menu.ShowMenu(productFacade);
    }
}