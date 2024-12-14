using BooksLibrary.Models.DataBase;
using BooksLibrary.Utils;
using BooksLibrary.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BooksLibrary.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly LibraryContext _context;

		public ObservableCollection<Book> Books { get; set; }
		public ObservableCollection<Student> Students { get; set; }
		public ObservableCollection<Rental> Rentals { get; set; }
		public ObservableCollection<Storage> Storage { get; set; }

		public ObservableCollection<Group> Groups { get; set; }
		public ObservableCollection<Speciality> Specialities { get; set; }
		public ObservableCollection<Faculty> Faculties { get; set; }

		private Book _selectedBook;
		public Book SelectedBook
		{
			get => _selectedBook;
			set
			{
				_selectedBook = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
			_context = new LibraryContext();

			LoadBooks();
			LoadStudents();
			LoadRentals();
			LoadStorage();
			LoadGroups();
			LoadSpecialities();
			LoadFaculties();

			// Привязка команд к методам.
			//AddBookCommand = new RelayCommand(AddBook);
			//EditBookCommand = new RelayCommand(EditBook, () => SelectedBook != null);
			//DeleteBookCommand = new RelayCommand(DeleteBook, () => SelectedBook != null);
		}

		private RelayCommand? _addBookCommand;
		public RelayCommand AddBookCommand
		{
			get
			{
				return _addBookCommand ??
				  (_addBookCommand = new RelayCommand((o) =>
				  {
					  BookWindow userWindow = new BookWindow(new Book());
					  if (userWindow.ShowDialog() == true)
					  {
						  Book book = userWindow.Book;
						  _context.Books.Add(book);
						  _context.SaveChanges();
					  }
				  }));
			}
		}
		private RelayCommand? _editBookCommand;

		public RelayCommand EditBookCommand
		{
			get
			{
				return _editBookCommand ??
				  (_editBookCommand = new RelayCommand((selectedItem) =>
				  {

					  Book? book = selectedItem as Book;
					  if (book == null) return;

					  Book vm = new Book
					  {
						  Title = book.Title,
						  Author = book.Author,
						  Publisher = book.Publisher,
						  Year = book.Year,
						  Pages = book.Pages,
						  Illustrations = book.Illustrations,
						  Price = book.Price,
					  };
					  BookWindow userWindow = new BookWindow(vm);

					  if (userWindow.ShowDialog() == true)
					  {
						  book.Title = userWindow.Book.Title;
						  book.Author = userWindow.Book.Author;
						  book.Publisher = userWindow.Book.Publisher;
						  book.Year = userWindow.Book.Year;
						  book.Pages = userWindow.Book.Pages;
						  book.Illustrations = userWindow.Book.Illustrations;
						  book.Price = userWindow.Book.Price;
						  _context.Entry(book).State = EntityState.Modified;
						  _context.SaveChanges();
					  }
				  }));
			}
		}

		private void LoadBooks()
		{
			// Загрузка книг из базы данных.
			IEnumerable<Book> books = _context.Books
				.Include(a => a.Author)
				.Include(p => p.Publisher)
				.ToList();

			Books = new ObservableCollection<Book>(books);
		}

		private void AddBook()
		{
			var newBook = new Book
			{
				Title = "Новая книга",
				Author = new Author
				{
					FirstName = "",
					LastName = "",
					MiddleName = ""
				},
				Publisher = new Publisher
				{
					Name = "",
					Address = "",
					Phone = ""
				},
				Year = 0,
				Illustrations = 0,
				Pages = 0,
				Price = 0
			};

			_context.Books.Add(newBook);
			_context.SaveChanges();

			Books.Add(newBook); // Обновление коллекции.
		}

		private void EditBook()
		{
			_context.Books.Update(SelectedBook);
			_context.SaveChanges();
			LoadBooks(); // Перезагрузка коллекции.
		}

		private void DeleteBook()
		{
			_context.Books.Remove(SelectedBook);
			_context.SaveChanges();
			Books.Remove(SelectedBook); // Удаление из коллекции.
		}

		private void SearchBookByAuthor(Book book)
		{

		}

	
		private void LoadStudents()
		{
			IEnumerable<Student> students = _context.Students
				.Include(a => a.Group)
				.ToList();

			Students = new ObservableCollection<Student>(students);
		}

		private void LoadRentals()
		{
			IEnumerable<Rental> rentals = _context.Rentals
				.Include(b => b.Book)
				.Include(s => s.Student)
				.ToList();
			Rentals = new ObservableCollection<Rental>(rentals);
		}

		private void LoadStorage()
		{
			IEnumerable<Storage> storage = _context.Storage
				.Include(b => b.Book)
				.Include(b => b.Branch)
				.ToList();
			Storage = new ObservableCollection<Storage>(storage);
		}
		private void LoadGroups()
		{
			IEnumerable<Group> groups = _context.Groups
				.Include(b => b.Speciality)
				.ToList();
			Groups = new ObservableCollection<Group>(groups);
		}

		private void LoadSpecialities()
		{
			IEnumerable<Speciality> specialities = _context.Specialities
				.Include(b => b.Faculty)
				.ToList();
			Specialities = new ObservableCollection<Speciality>(specialities);
		}

		private void LoadFaculties()
		{
			IEnumerable<Faculty> faculties = _context.Faculties
				.ToList();
			Faculties = new ObservableCollection<Faculty>(faculties);
		}


		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
