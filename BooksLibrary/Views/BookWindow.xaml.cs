using BooksLibrary.Models.DataBase;
using System;
using System.Collections.Generic;
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
	public partial class BookWindow : Window
	{
		public Book Book { get; private set; }
		public BookWindow(Book book)
		{
			InitializeComponent();
			Book = book;
			this.DataContext = Book;
		}
		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
