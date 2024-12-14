using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Models.DataBase
{
	public class Group
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public DateTime Created { get; set; }

		public Speciality? Speciality { get; set; }
		[Column("speciality_id")]
		public int SpecialityId { get; set; }

	}
}
