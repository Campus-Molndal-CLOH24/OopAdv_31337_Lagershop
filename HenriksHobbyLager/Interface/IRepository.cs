using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HenriksHobbylager.Repositories;



public interface IRepository<T> where T : class
{
	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
	void CreateProduct(T entity);
	void Update(T entity);
	void Delete(int id);
	IEnumerable<T> Search(Func<T, bool> predicate);
	Task SaveChangesAsync();
}
