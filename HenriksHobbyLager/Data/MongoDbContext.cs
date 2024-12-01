
using HenriksHobbylager.Models;
using MongoDB.Driver;

namespace HenriksHobbylager.Data;
public class MongoDbContext
{
    public IMongoDatabase Database { get; }
    private static MongoDbContext? _instance;
    private static readonly object _lock = new();

    // Constructor: Initializes the database connection, used internally for the Singleton-pattern
    private MongoDbContext(string connectionString, string databaseName)
    {
        if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(databaseName))
            throw new ArgumentException("Connection string and database name must be provided.");

        var client = new MongoClient(connectionString);
        Database = client.GetDatabase(databaseName);
    }
    
    public static MongoDbContext Instance(string connectionString, string databaseName)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new MongoDbContext(connectionString, databaseName);
                }
            }
        }
        return _instance;
    }

    // Lambda shortcut to access the 'Products' collection in the database
    public IMongoCollection<Product> Products =>
        Database.GetCollection<Product>("Products");
}


