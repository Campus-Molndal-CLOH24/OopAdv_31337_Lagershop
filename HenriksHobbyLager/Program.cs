using HenriksHobbylager.Data;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;
using HenriksHobbylager.Facades;
using HenriksHobbylager.UI;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // Ställ in Dependency Injection
        var services = new ServiceCollection();

        // Registrera DbContext
        services.AddDbContext<AppDbContext>();

        // Registrera Repository och Facade
        services.AddScoped<IRepository<Product>, Repository>();
        services.AddScoped<IProductFacade, ProductFacade>();

        // Bygg ServiceProvider
        var serviceProvider = services.BuildServiceProvider();

        // Starta menyn
        var menu = new Menu();
        menu.ShowMenu(serviceProvider.GetService<IProductFacade>());
    }
}
