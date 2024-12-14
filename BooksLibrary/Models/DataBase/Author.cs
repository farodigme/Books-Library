using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Author
	{
		public int Id { get; set; }
		[Column("last_name")]
		public string? LastName { get; set; }
		[Column("first_name")]
		public string? FirstName { get; set; }
		[Column("middle_name")]
		public string? MiddleName { get; set; }

	}
}
