/* using HenriksHobbyLager.Data;
using HenriksHobbylager.Repositories;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace HenriksHobbylager.Models;

// This is the abstractionlayer.

public class Repository: IRepository<Product>
{
    
    // Create an instance of GameContext
    private AppDbContext _context;

    // Constructor that initializes the GameContext
    public Repository()
    {
        _context = new AppDbContext();
    }
    
    
    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void CreateProduct(Product product)
    {
        // if (!_context.Products.Any(p => p.Name == product.Name))
        // {
            _context.Products.Add(product);
            _context.SaveChanges();
        // }
        // else
        // {
        //     Console.WriteLine("Produkten finns redan i databasen.");
        // }
    }

    public void Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        throw new NotImplementedException();
    }
    
} */