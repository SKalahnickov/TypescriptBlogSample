using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TypescriptApp.DAL;
using TypescriptApp.DAL.Models;

namespace TypescriptApp.Logics
{
	public class PostsContext : BaseContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
