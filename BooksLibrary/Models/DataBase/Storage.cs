using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Storage
	{
		public int Id { get; set; }

		public Book? Book { get; set; }
		public int BookId { get; set; }
		public Branch? Branch { get; set; }
		public int BranchId { get; set;}

		public int Count { get; set; }
	}
}
