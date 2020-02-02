using System;
using System.Collections.Generic;
using System.Text;

namespace TypescriptApp.DAL.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int UserId { get; set; }
		public string Category { get; set; }
		public string Text { get; set; }
		public DateTime CreatedAt { get; set; }
		public virtual User User { get; set; }
	}
}
