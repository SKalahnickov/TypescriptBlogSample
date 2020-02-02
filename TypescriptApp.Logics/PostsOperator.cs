using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypescriptApp.Logics.Models;

namespace TypescriptApp.Logics
{
	public class PostsOperator
	{
		public async Task SavePostAsync(PostModel post)
		{
			using (var context = new PostsContext())
			{
				var user = await context.Users
					.Where(value => value.Name == post.CreatedBy)
					.SingleOrDefaultAsync()
					.ConfigureAwait(false);

				if (user == null)
				{
					user = new DAL.Models.User()
					{
						Name = post.CreatedBy
					};
					context.Users.Add(user);
				}

				var dalPost = await context.Posts
					.Where(value => value.Id == post.Id)
					.SingleOrDefaultAsync()
					.ConfigureAwait(false);

				if (dalPost == null)
				{
					dalPost = new DAL.Models.Post()
					{
						Category = post.Category,
						CreatedAt = DateTime.Now,
						Text = post.Text,
						Title = post.Title,
						User = user
					};

					context.Posts.Add(dalPost);
				}
				else
				{
					dalPost.User = user;
					dalPost.Category = post.Category;
					dalPost.CreatedAt = post.CreatedAt;
					dalPost.Text = post.Text;
					dalPost.Title = post.Title;
				}

				await context.SaveChangesAsync()
					.ConfigureAwait(false);
			}
		}

		public async Task<IReadOnlyCollection<PostModel>> GetPostsAsync()
		{
			using var context = new PostsContext();
			return await context.Posts
				.Select(value => new PostModel
				{
					CreatedAt = value.CreatedAt,
					Category = value.Category,
					CreatedBy = value.User.Name,
					Text = value.Text,
					Title = value.Title,
					Id = value.Id
				})
				.ToArrayAsync()
				.ConfigureAwait(false);
		}

		public async Task<PostModel> GetPostAsync(int id)
		{
			using var context = new PostsContext();
			return await context.Posts
				.Where(value => value.Id == id)
				.Join(context.Users, p => p.UserId, u => u.Id, (p, u) => new PostModel
				{
					Category = p.Category,
					CreatedAt = p.CreatedAt,
					CreatedBy = u.Name,
					Text = p.Text,
					Title = p.Title,
					Id = p.Id
				})
				.SingleAsync()
				.ConfigureAwait(false);
		}
	}
}
