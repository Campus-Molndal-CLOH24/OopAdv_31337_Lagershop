using HenriksHobbylager.Repositories;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace HenriksHobbylager.Models;

public class Repository: IRepository<Product>
{
    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        throw new NotImplementedException();
    }
}