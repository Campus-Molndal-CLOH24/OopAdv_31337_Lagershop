using System.Linq.Expressions;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;
using Microsoft.EntityFrameworkCore;
using HenriksHobbylager.Data;

namespace HenriksHobbyLager.Repositories;

public class SQLiteRepository : IRepository<Product>
{
    private readonly AppDbContext _context;

    public SQLiteRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Sorted all the methods alfabetically for easier reading

    public async Task AddAsync(Product entity)
    {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Products.FindAsync(id);
        if (entity != null)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Product entity)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _context.Products.Where(predicate).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _context.Products.FindAsync(name);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _context.Products.Where(predicate).ToListAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}