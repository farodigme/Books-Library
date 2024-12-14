using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Storage
	{
		public int Id { get; set; }

		public Book? Book { get; set; }
		[Column("book_id")]
		public int BookId { get; set; }
		public Branch? Branch { get; set; }
		[Column("branch_id")]
		public int BranchId { get; set;}

		public int Count { get; set; }
	}
}
