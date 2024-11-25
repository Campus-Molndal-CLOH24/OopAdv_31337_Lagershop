using System;
using HenriksHobbylager.UI;

namespace HenriksHobbylager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till HobbyLagret!");

            var menu = new Menu();
            Menu.ShowMenu();
        }
    }
}