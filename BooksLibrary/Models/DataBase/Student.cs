using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Student
	{
		public int Id { get; set; }
		[Column("last_name")]
		public string? LastName { get; set; }
		[Column("first_name")]
		public string? FirstName { get; set; }
		[Column("middle_name")]
		public string? MiddleName { get; set; }

		public Group? Group { get; set; }
		[Column("group_id")]
		public int GroupId { get; set; }

	}
}
