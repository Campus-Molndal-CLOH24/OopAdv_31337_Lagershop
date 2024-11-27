using HenriksHobbylager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace HenriksHobbylager.Data;
public class MongoDbContext
{
    public IMongoDatabase Database { get; }

    public MongoDbContext(string connectionString, string databaseName)
    {
        if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(databaseName))
            throw new ArgumentException("Connection string and database name must be provided.");

        var client = new MongoClient(connectionString);
        Database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Product> Products =>
        Database.GetCollection<Product>("Products");
}

