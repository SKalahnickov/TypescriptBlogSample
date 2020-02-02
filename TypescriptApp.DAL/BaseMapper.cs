using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypescriptApp.DAL
{
	public interface IBaseMapper
	{
		void Map(ModelBuilder modelBuilder);
	}

	public abstract class BaseMapper<TEntity> : IBaseMapper
		where TEntity : class
	{
		public void Map(ModelBuilder modelBuilder)
			=> Map(modelBuilder.Entity<TEntity>());

		public abstract void Map(EntityTypeBuilder<TEntity> entity);
	}
}
