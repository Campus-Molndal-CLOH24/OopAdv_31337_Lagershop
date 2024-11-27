using System.Linq.Expressions;
using HenriksHobbylager.Data;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;
using Microsoft.EntityFrameworkCore;

public class Repository : IRepository<Product>
{
    private readonly AppDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
    }

    // Interface
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(Product entity)
    {
        if (!await _dbSet.AnyAsync(p => p.Name == entity.Name))
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("Produkten finns redan i databasen.");
        }
    }


    public async Task<IEnumerable<Product>> GetAllAsync(Func<Product, bool> predicate)
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id) // TODO: Possible null reference exception, consider returning Task<Product?>
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _dbSet.FindAsync(name);
    }

    public async Task UpdateAsync(Product entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    // TODO: Megan har tittat på koden nedan, låter den stå, men syns detta i konflikt så överväg hennes awsomesauce-kod! :)
    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
}