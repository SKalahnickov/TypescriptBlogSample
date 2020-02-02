using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypescriptApp.DAL
{
	public class BaseContext : DbContext
	{
		public BaseContext()
		{
			Database.EnsureCreated();
		}

		protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=PostsStorage;UserName=postgres;Password=123");
		}

		protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var assembly = typeof(IBaseMapper).Assembly;

			var mappers = assembly.GetTypes()
				.Where(value => !value.IsAbstract)
				.Where(value => !value.IsInterface)
				.Where(value => !value.IsGenericType)
				.Where(value => !value.IsNested)
				.Where(value => typeof(IBaseMapper).IsAssignableFrom(value))
				.Select(value => Activator.CreateInstance(value) as IBaseMapper);

			foreach (var mapper in mappers)
				mapper.Map(modelBuilder);
		}
	}
}
