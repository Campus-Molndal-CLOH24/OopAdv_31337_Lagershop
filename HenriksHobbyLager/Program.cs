using HenriksHobbylager.Data;
using HenriksHobbylager.UI;

class Program
{
    static void Main(string[] args)
    {
        // Create the database and tables if they don't exist.
        using var context = new AppDbContext();
        context.Database.EnsureCreated();

        // TODO: Fix the menu..
        // Start the application.

        var productRepository = new Repository(context);
        var productFacade = new ProductFacade(productRepository);

        var menu = new Menu();
        menu.ShowMenu(productFacade);
    }
}
