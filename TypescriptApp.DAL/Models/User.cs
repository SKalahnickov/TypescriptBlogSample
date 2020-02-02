using System;
using System.Collections.Generic;
using System.Text;

namespace TypescriptApp.DAL.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<Post> Posts { get; set; } = new List<Post>();
	}
}
