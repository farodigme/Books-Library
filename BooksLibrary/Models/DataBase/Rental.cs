using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Rental
	{
		public int Id { get; set; }
		public Book? Book { get; set; }
		public int BookId { get; set; }

		public Student? Student { get; set; }
		public int StudentId { get; set; }

		[Column("rental_date")]
		public DateTime RentalDate {  get; set; }

		[Column("return_date")]
		public DateTime ReturnDate { get; set; }
		[Column("returned")]
		public bool isReturned { get; set; }
	}
}
