using Microsoft.EntityFrameworkCore;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		protected DataContext Context { get; set; }

		public Repository(DataContext context)
		{
			this.Context = context;
		}

		public async Task<T> GetById(int id)
		{
			return await Context.Set<T>().FindAsync(id);
		}

		public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			return await Context.Set<T>().FirstOrDefaultAsync(predicate);
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await Context.Set<T>().ToListAsync();
		}

		public async Task Create(T entity)
		{
			await Context.Set<T>().AddAsync(entity);
			await Context.SaveChangesAsync();
		}

		public async Task Update(T entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			await Context.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			Context.Set<T>().Remove(entity);
			await Context.SaveChangesAsync();		}
	}
}
