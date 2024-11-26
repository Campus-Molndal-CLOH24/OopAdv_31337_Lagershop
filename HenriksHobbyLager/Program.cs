/* using System;
using HenriksHobbyLager.Data;
using HenriksHobbylager.Models;
// using HenriksHobbylager.UI;

namespace HenriksHobbylager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Välkommen till HobbyLagret!");
            //
            // var menu = new Menu();
            // Menu.ShowMenu();
            
            // AppDbContext context = new AppDbContext();
            
            var repository = new Repository();
            repository.CreateProduct(new Product());
        }
    }
} */



using HenriksHobbylager.Data;

class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDbContext();

        // Skapa databasen och tabeller
        context.Database.EnsureCreated();

        Console.WriteLine("Databasen och tabeller har skapats i mappen 'Data'.");
    }
}
