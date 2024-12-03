using System.Linq.Expressions;

namespace HenriksHobbylager.Repositories;

public interface IRepository<T> where T : class
{
	Task AddAsync(T entity); 
	Task UpdateAsync(T entity);
	Task DeleteAsync(string id);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate); 
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(string id);
    Task SaveChangesAsync(); 
}