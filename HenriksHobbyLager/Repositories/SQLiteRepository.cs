using System.Linq.Expressions;
using HenriksHobbylager.Data;
using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbylager.Repositories;

public class SQLiteRepository : IRepository<Product>
{
    private readonly SQLiteDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public SQLiteRepository(SQLiteDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Product>();
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
            throw new Exception("Produkten finns redan i databasen.");
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        if (int.TryParse(id, out var intId))
        {
            var entity = await _dbSet.FindAsync(intId);
            if (entity != null)
            {
                return entity;
            }
          else
            {
                throw new Exception("Kunde inte hitta produkten.");
            }
        }
        else
        {
            throw new Exception("Ogiltigt ID-format.");
        }
    }

    public async Task UpdateAsync(Product entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    { var entity = await GetByIdAsync(id);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
