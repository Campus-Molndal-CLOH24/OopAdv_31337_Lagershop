using System.Linq.Expressions;
using HenriksHobbylager.Data.MongoDb;
using HenriksHobbylager.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HenriksHobbylager.Repositories;

public class MongoRepository : IRepository<Product>
{
    private readonly IMongoCollection<Product> _collection;

    public MongoRepository(MongoDbContext context)
    {
        _collection = context.Products;
    }

    public async Task AddAsync(Product entity)
    {
        if (string.IsNullOrEmpty(entity._id))
        {
            entity._id = ObjectId.GenerateNewId().ToString();
        }

        if (entity.Id == 0)
        {
            entity.Id = ObjectId.Parse(entity._id).GetHashCode();
        }

        await _collection.InsertOneAsync(entity);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _collection.Find(p => p._id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        if (string.IsNullOrEmpty(entity._id))
        {
            Console.WriteLine("Produkten saknar ett giltigt _id för MongoDB och kan inte uppdateras.");
            return;
        }

        var result = await _collection.ReplaceOneAsync(p => p._id == entity._id, entity);
        if (result.MatchedCount == 0)
        {
            Console.WriteLine("Produkten kunde inte uppdateras eftersom den inte hittades.");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(p => p._id == id);
        if (result.DeletedCount == 0)
        {
            Console.WriteLine("Produkten kunde inte tas bort eftersom den inte hittades.");
        }
    }

    public async Task DeleteAsync(Product entity)
    {
        if (string.IsNullOrEmpty(entity._id))
        {
            Console.WriteLine("Produkten saknar ett giltigt _id för MongoDB och kan inte tas bort.");
            return;
        }

        await _collection.DeleteOneAsync(p => p._id == entity._id);
    }

    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask; // MongoDB sparar direkt
    }
}
