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
        // var menu = new Menu();
        // menu.ShowMenu();
    }
}
