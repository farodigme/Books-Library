using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Speciality
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public string Code { get; set; }

		public Faculty? Faculty { get; set; }
		[Column("faculty_id")]
		public int FacultyId { get; set; }
	}
}
