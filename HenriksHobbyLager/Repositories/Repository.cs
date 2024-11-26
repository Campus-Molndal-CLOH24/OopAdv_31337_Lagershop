
using System.Linq.Expressions;
using HenriksHobbylager.Data;
using HenriksHobbylager.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace HenriksHobbylager.Models;

// This is the abstractionlayer.

public class Repository: IRepository<Product>
{
    private readonly AppDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
        {
           return await _dbSet.ToListAsync(); 
        }
   

    public async Task<Product?> GetByIdAsync(int id)
    {
      return await _dbSet.FindAsync(id);
    }


    

    public void CreateProduct(Product product)
    {
         if (!_context.Products.Any(p => p.Name == product.Name))
         {
            _context.Products.Add(product);
            _context.SaveChanges();
         }
         else 
         { Console.WriteLine("Produkten finns redan i databasen.");
         }
    }

    public void Update(Product entity)
    {
       _dbSet.Update(entity);
    }

    public void Delete(int id)
    { 
        _dbSet.Remove(_dbSet.Find(id));
    }


    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
} 