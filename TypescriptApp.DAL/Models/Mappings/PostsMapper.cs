using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypescriptApp.DAL.Models.Mappings
{
	public class PostsMapper : BaseMapper<Post>
	{
		public override void Map(EntityTypeBuilder<Post> entity)
		{
			entity
				.ToTable("Posts")
				.HasKey(x => x.Id);

			entity
				.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.UseSerialColumn();

			entity
				.HasOne(x => x.User)
				.WithMany(x => x.Posts)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
