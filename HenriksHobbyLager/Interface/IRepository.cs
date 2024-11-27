using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HenriksHobbylager.Models;

namespace HenriksHobbylager.Repositories;

public interface IRepository<T> where T : class
{
	Task AddAsync(T entity); 
	Task UpdateAsync(T entity);
	Task DeleteAsync(int id);
	Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate); // Use Expression for database filtering
	Task<IEnumerable<T>> GetAllAsync(); 
	Task<Product> GetByIdAsync(int id);
	Task<IEnumerable<Product>> GetAllAsync(Func<Product, bool> predicate);
	Task SaveChangesAsync(); // Save all changes to the database
}

