﻿using System.Linq.Expressions;
using HenriksHobbylager.Data;
using HenriksHobbylager.Models;
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
        var existingProduct = await _collection.Find(p => p.Name == entity.Name).FirstOrDefaultAsync();
        if (existingProduct == null)
        {
            await _collection.InsertOneAsync(entity);
        }
        else
        {
            Console.WriteLine("Produkten finns redan i databasen.");
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        var result = await _collection.ReplaceOneAsync(p => p.Id == entity.Id, entity);
        if (result.MatchedCount == 0)
        {
            Console.WriteLine("Produkten kunde inte uppdateras eftersom den inte hittades.");
        }
    }

    public async Task DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(p => p.Id == id);
        if (result.DeletedCount == 0)
        {
            Console.WriteLine("Produkten kunde inte tas bort eftersom den inte hittades.");
        }
    }

    public async Task DeleteAsync(Product entity)
    {
        await _collection.DeleteOneAsync(p => p.Id == entity.Id);
    }

    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    // No-op for MongoDB since changes are saved immediately. We keep this for now though since it's part of the interface.
    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}