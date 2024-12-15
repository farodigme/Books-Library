using BooksLibrary.Models.DataBase;
using BooksLibrary.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BooksLibrary.Views
{
	/// <summary>
	/// Логика взаимодействия для BookWindow.xaml
	/// </summary>
	public partial class BookHistoryWindow : Window
	{
		public BookHistoryWindow(IEnumerable<Rental> rentals)
		{
			InitializeComponent();		
			this.DataContext = new BookHistoryViewModel(new ObservableCollection<Rental>(rentals));
		}
		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
