using System;
using System.Collections.Generic;
using System.Text;

namespace TypescriptApp.Logics.Models
{
	public class PostModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Category { get; set; }
		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public string Text { get; set; }
	}
}
