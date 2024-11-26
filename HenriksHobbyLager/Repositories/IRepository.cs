/* using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HenriksHobbylager.Repositories;



public interface IRepository<T> where T : class
{
	T GetById(int id);
	void CreateProduct(T entity);
	void Update(T entity);
	void Delete(int id);
	IEnumerable<T> GetAll();
	IEnumerable<T> Search(Func<T, bool> predicate);
}
 */