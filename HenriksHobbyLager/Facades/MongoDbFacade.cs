using HenriksHobbylager.Interface;
using HenriksHobbylager.Models;
using HenriksHobbylager.Repositories;

namespace HenriksHobbyLager.Facades
{
    internal class MongoDbFacade : IProductFacade
    {
        private readonly IRepository<Product> _repository;
        public string DatabaseType => "MongoDB";

        public MongoDbFacade(IRepository<Product> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateProductAsync(string productName, int productStock, decimal productPrice, string category)
        {
            var product = new Product
            {
                Name = productName,
                Stock = productStock,
                Price = productPrice,
                Category = category,
                _id = MongoDB.Bson.ObjectId.GenerateNewId().ToString()
            };

            await _repository.AddAsync(product);
        }

        public async Task DeleteProductAsync(string productId)
        {
            var product = await _repository.GetByIdAsync(productId);
            if (product == null)
                throw new ArgumentException($"Produkten med ID {productId} hittades ej.");

            await _repository.DeleteAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateAsync(product);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Söktermen kan inte vara null eller tom.", nameof(searchTerm));

            var lowerSearchTerm = searchTerm.ToLower();
            return await _repository.SearchAsync(p =>
                p.Name.ToLower().Contains(lowerSearchTerm) ||
                (p.Category != null && p.Category.ToLower().Contains(lowerSearchTerm)));
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            Console.WriteLine("Hämtar produkter från MongoDB");
            return await _repository.GetAllAsync(p => true);
        }

        public async Task<Product?> GetProductByIdAsync(string productId)
        {
            return await _repository.GetByIdAsync(productId);
        }
    }
}
