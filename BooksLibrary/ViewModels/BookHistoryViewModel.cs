using BooksLibrary.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.ViewModels
{
	public class BookHistoryViewModel
	{
		public ObservableCollection<Rental> Rentals { get; }

		public BookHistoryViewModel(ObservableCollection<Rental> rentals)
		{
			Rentals = rentals;
		}
	}

}
