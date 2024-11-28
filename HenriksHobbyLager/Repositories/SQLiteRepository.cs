using System.Linq.Expressions;
using HenriksHobbylager.Data;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Repositories;

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
            Console.WriteLine("Produkten finns redan i databasen.");
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }


    public async Task<IEnumerable<Product>> GetAllAsync(Func<Product, bool> predicate)
    {
        return await Task.FromResult(_dbSet.Where(predicate).AsEnumerable());
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
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

    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task DeleteAsync(Product entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
}