using RefactoringExercise.Repositories;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace RefactoringExercise.Models;

public class Repository<T>: IRepository<T>
{
    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Search(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }
}