using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
	public interface IRepository<T> where T : BaseEntity
	{
		Task<T> GetById(int id);
		Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetAll();
		Task Create(T entity);
		Task Update(T entity);
		Task Delete(T entity);
	}
}
