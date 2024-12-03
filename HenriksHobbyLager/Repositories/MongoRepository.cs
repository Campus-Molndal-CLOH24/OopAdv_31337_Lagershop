using System.Linq.Expressions;
using HenriksHobbylager.Data.MongoDb;
using HenriksHobbylager.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HenriksHobbylager.Repositories;

internal class MongoRepository
/*internal class MongoRepository<T> where T : class */
{
    private readonly IMongoCollection<Product> _collection;

    public MongoRepository(MongoDbContext context)
    {
        _collection = context.Products;
    }

    public async Task AddAsync(Product entity)
    {
        // Sätt MongoDB `_id` om det saknas
        if (string.IsNullOrEmpty(entity._id))
        {
            entity._id = ObjectId.GenerateNewId().ToString();
        }

        // Sätt `Id` för kompatibilitet med andra delar av applikationen
        if (entity.Id == 0)
        {
            entity.Id = ObjectId.Parse(entity._id).GetHashCode();
        }

        await _collection.InsertOneAsync(entity);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        // Returnera alla produkter som matchar predicate
        return await _collection.Find(predicate).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        // Försök hitta med både `_id` och `Id`
        var product = await _collection.Find(p => p._id == id).FirstOrDefaultAsync();
        if (product == null && int.TryParse(id, out var intId))
        {
            product = await _collection.Find(p => p.Id == intId).FirstOrDefaultAsync();
        }
        return product;
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
        // Försök radera med `_id`
        var result = await _collection.DeleteOneAsync(p => p._id == id);

        // Om inget raderades, försök med `Id` om det är numeriskt
        if (result.DeletedCount == 0 && int.TryParse(id, out var intId))
        {
            result = await _collection.DeleteOneAsync(p => p.Id == intId);
        }

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
        // Sök med hjälp av predicate
        return await _collection.Find(predicate).ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        // MongoDB sparar direkt
        return Task.CompletedTask;
    }
}
