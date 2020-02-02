using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypescriptApp.DAL.Models.Mappings
{
	public class UsersMapper : BaseMapper<User>
	{
		public override void Map(EntityTypeBuilder<User> entity)
		{
			entity
				.ToTable("Users")
				.HasKey(x => x.Id);

			entity
				.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.UseSerialColumn();
		}
	}
}
