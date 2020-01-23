using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}