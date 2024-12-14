using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Book
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public Author? Author { get; set; }
		public int AuthorId { get; set; }
		public Publisher? Publisher { get; set; }
		public int PublisherId { get; set; }
		public DateTime Year { get; set; }
		public int Pages { get; set; }
		public int Illustrations { get; set; }
		public int Price { get; set; }
	}
}
