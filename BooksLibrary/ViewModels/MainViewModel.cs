﻿using BooksLibrary.Models.DataBase;
using BooksLibrary.Utils;
using BooksLibrary.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using static MaterialDesignThemes.Wpf.Theme;
using static System.Reflection.Metadata.BlobBuilder;

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
		private string _searchBookTitle;
		public string SearchBookTitle
		{
			get => _searchBookTitle;
			set
			{
				_searchBookTitle = value;
				OnPropertyChanged();
			}
		}

		private string _searchBookAuthor;
		public string SearchBookAuthor
		{
			get => _searchBookAuthor;
			set
			{
				_searchBookAuthor = value;
				OnPropertyChanged();
			}
		}

		private string _searchStudentGroup;
		public string SearchStudentGroup
		{
			get => _searchStudentGroup;
			set
			{
				_searchStudentGroup = value;
				OnPropertyChanged();
			}
		}

		private string _searchStudent;
		public string SearchStudent
		{
			get => _searchStudent;
			set
			{
				_searchStudent = value;
				OnPropertyChanged();
			}
		}

		private string _searchGroup;
		public string SearchGroup
		{
			get => _searchGroup;
			set
			{
				_searchGroup = value;
				OnPropertyChanged();
			}
		}

		private DateTime? _startGroupDate;
		public DateTime? StartGroupDate
		{
			get => _startGroupDate;
			set
			{
				_startGroupDate = value;
				OnPropertyChanged(nameof(StartGroupDate));
			}
		}

		private DateTime? _endGroupDate;
		public DateTime? EndGroupDate
		{
			get => _endGroupDate;
			set
			{
				_endGroupDate = value;
				OnPropertyChanged(nameof(EndGroupDate));
			}
		}

		private string _searchSpeciality;
		public string SearchSpeciality
		{
			get => _searchSpeciality;
			set
			{
				_searchSpeciality = value;
				OnPropertyChanged();
			}
		}

		private string _searchFaculty;
		public string SearchFaculty
		{
			get => _searchFaculty;
			set
			{
				_searchFaculty = value;
				OnPropertyChanged();
			}
		}

		private string _searchRentalStudent;
		public string SearchRentalStudent
		{
			get => _searchRentalStudent;
			set
			{
				_searchRentalStudent = value;
				OnPropertyChanged();
			}
		}

		private DateTime? _startRentalDate;
		public DateTime? StartRentalDate
		{
			get => _startRentalDate;
			set
			{
				_startRentalDate = value;
				OnPropertyChanged();
			}
		}

		private DateTime? _endRentalDate;
		public DateTime? EndRentalDate
		{
			get => _endRentalDate;
			set
			{
				_endRentalDate = value;
				OnPropertyChanged();
			}
		}

		private DateTime? _startReturnDate;
		public DateTime? StartReturnDate
		{
			get => _startReturnDate;
			set
			{
				_startReturnDate = value;
				OnPropertyChanged();
			}
		}

		private DateTime? _endReturnDate;
		public DateTime? EndReturnDate
		{
			get => _endReturnDate;
			set
			{
				_endReturnDate = value;
				OnPropertyChanged();
			}
		}

		private string _searchBookStorage;
		public string SearchBookStorage
		{
			get => _searchBookStorage;
			set
			{
				_searchBookStorage = value;
				OnPropertyChanged();
			}
		}

		private string _searchBranchStorage;
		public string SearchBranchStorage
		{
			get => _searchBranchStorage;
			set
			{
				_searchBranchStorage = value;
				OnPropertyChanged();
			}
		}

		private string _searchLocationStorage;
		public string SearchLocationStorage
		{
			get => _searchLocationStorage;
			set
			{
				_searchLocationStorage = value;
				OnPropertyChanged();
			}
		}

		private string _searchRentalFaculty;
		public string SearchRentalFaculty
		{
			get => _searchRentalFaculty;
			set
			{
				_searchRentalFaculty = value;
				OnPropertyChanged();
			}
		}

		private string _searchRentalSpeciality;
		public string SearchRentalSpeciality
		{
			get => _searchRentalSpeciality;
			set
			{
				_searchRentalSpeciality = value;
				OnPropertyChanged();
			}
		}

		private bool _checkReturn;
		public bool CheckReturn
		{
			get => _checkReturn;
			set
			{
				_checkReturn = value;
				OnPropertyChanged();
			}
		}

		private int _booksOnHandsCount;
		public int BooksOnHandsCount
		{
			get => _booksOnHandsCount;
			set
			{
				_booksOnHandsCount = Convert.ToInt32(value);
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
		}

		private RelayCommand? _searchForBookCommand;
		public RelayCommand SearchForBookCommand
		{
			get
			{
				return _searchForBookCommand ??
				  (_searchForBookCommand = new RelayCommand((o) =>
				  {
					  IEnumerable<Book> books = _context.Books
					  .Include(a => a.Author)
					  .Include(p => p.Publisher);

					  if (!string.IsNullOrWhiteSpace(SearchBookAuthor))
					  {
						  books = books.Where(s => s.Author.FirstName.Contains(SearchBookAuthor) ||
												   s.Author.LastName.Contains(SearchBookAuthor) ||
												   s.Author.MiddleName.Contains(SearchBookAuthor));
					  }
					  if (!string.IsNullOrWhiteSpace(SearchBookTitle))
					  {
						  books = books.Where(s => s.Title.Contains(SearchBookTitle));
					  }

					  // Очищаем и добавляем отфильтрованные данные
					  Books.Clear();
					  foreach (var book in books)
					  {
						  Books.Add(book);
					  }
				  }));
			}
		}

		private RelayCommand? _searchForStudentOrGroup;
		public RelayCommand SearchForStudentOrGroup
		{
			get
			{
				return _searchForStudentOrGroup ??
				  (_searchForStudentOrGroup = new RelayCommand((o) =>
				  {
					  IEnumerable<Student> students = _context.Students
						   .Include(a => a.Group)
						   .ToList();

					  if (!string.IsNullOrWhiteSpace(SearchStudent))
					  {
						  students = students.Where(s => s.FirstName.Contains(SearchStudent) ||
												   s.LastName.Contains(SearchStudent) ||
												   s.MiddleName.Contains(SearchStudent));
					  }
					  if (!string.IsNullOrWhiteSpace(SearchStudentGroup))
					  {
						  students = students.Where(s => s.Group.Name.Contains(SearchStudentGroup));
					  }

					  // Очищаем и добавляем отфильтрованные данные
					  Students.Clear();
					  foreach (var student in students)
					  {
						  Students.Add(student);
					  }
				  }));
			}
		}

		private RelayCommand? _searchForGroupOrDate;
		public RelayCommand SearchForGroupOrDate
		{
			get
			{
				return _searchForGroupOrDate ??
				  (_searchForGroupOrDate = new RelayCommand((o) =>
				  {
					  IEnumerable<Group> groups = _context.Groups
						.Include(b => b.Speciality);

					  if (!string.IsNullOrWhiteSpace(SearchGroup))
					  {
						  groups = groups.Where(s => s.Name.Contains(SearchGroup));
					  }

					  if (StartGroupDate.HasValue && EndGroupDate.HasValue)
					  {
						  groups = groups.Where(s => s.Created >= StartGroupDate.Value && s.Created <= EndGroupDate.Value);
					  }

					  // Очищаем и добавляем отфильтрованные данные
					  Groups.Clear();
					  foreach (var group in groups)
					  {
						  Groups.Add(group);
					  }
				  }));
			}
		}

		private RelayCommand? _searchForSpeciality;
		public RelayCommand SearchForSpeciality
		{
			get
			{
				return _searchForSpeciality ??
				  (_searchForSpeciality = new RelayCommand((o) =>
				  {
					  IEnumerable<Speciality> specialities = _context.Specialities
					  .Include(b => b.Faculty);

					  if (!string.IsNullOrWhiteSpace(SearchSpeciality))
					  {
						  specialities = specialities.Where(s => s.Name.Contains(SearchSpeciality) || s.Abbreviation.Contains(SearchSpeciality));
					  }

					  Specialities.Clear();
					  foreach (var speciality in specialities)
					  {
						  Specialities.Add(speciality);
					  }
				  }));
			}
		}

		private RelayCommand? _searchForFaculty;
		public RelayCommand SearchForFaculty
		{
			get
			{
				return _searchForFaculty ??
				  (_searchForFaculty = new RelayCommand((o) =>
				  {
					  IEnumerable<Faculty> faculties = _context.Faculties;

					  if (!string.IsNullOrWhiteSpace(SearchFaculty))
					  {
						  faculties = faculties.Where(s => s.Name.Contains(SearchFaculty) || s.Abbreviation.Contains(SearchFaculty));
					  }

					  Faculties.Clear();
					  foreach (var faculty in faculties)
					  {
						  Faculties.Add(faculty);
					  }
				  }));
			}
		}


		private RelayCommand? _searchForRental;
		public RelayCommand SearchForRental
		{
			get
			{
				return _searchForRental ??
				  (_searchForRental = new RelayCommand((o) =>
				  {
					  IEnumerable<Rental> rentals = _context.Rentals
					   .Include(b => b.Book)
					   .Include(s => s.Student);

					  if (!string.IsNullOrWhiteSpace(SearchRentalStudent))
					  {
						  rentals = rentals.Where(s => s.Student.FirstName.Contains(SearchRentalStudent) ||
													s.Student.LastName.Contains(SearchRentalStudent) ||
													s.Student.MiddleName.Contains(SearchRentalStudent));
					  }

					  if (!string.IsNullOrWhiteSpace(SearchRentalFaculty))
					  {
						  rentals = rentals.Where(s => s.Student.Group.Speciality.Faculty.Name.Contains(SearchRentalFaculty) || s.Student.Group.Speciality.Faculty.Abbreviation.Contains(SearchRentalFaculty));
					  }

					  if (!string.IsNullOrWhiteSpace(SearchRentalSpeciality))
					  {
						  rentals = rentals.Where(s => s.Student.Group.Speciality.Name.Contains(SearchRentalSpeciality) || s.Student.Group.Speciality.Abbreviation.Contains(SearchRentalSpeciality));
					  }

					  if (StartRentalDate.HasValue && EndRentalDate.HasValue)
					  {
						  rentals = rentals.Where(s => s.RentalDate >= StartRentalDate.Value && s.RentalDate <= EndRentalDate.Value);
					  }

					  if (StartReturnDate.HasValue && EndReturnDate.HasValue)
					  {
						  rentals = rentals.Where(s => s.ReturnDate >= StartReturnDate.Value && s.ReturnDate <= EndReturnDate.Value);
					  }
					  if (CheckReturn)
					  {
						  rentals = rentals.Where(s => s.isReturned);
					  }
					  if (BooksOnHandsCount > 0)
					  {
						  rentals = rentals
						 .Where(r => !r.isReturned)
						 .GroupBy(r => r.Student)
						 .Where(g => g.Count() >= BooksOnHandsCount)
						 .SelectMany(g => g);
					  }

					  Rentals.Clear();
					  foreach (var rental in rentals)
					  {
						  Rentals.Add(rental);
					  }
				  }));
			}
		}

		private RelayCommand? _searchDebtor;
		public RelayCommand SearchDebtor
		{
			get
			{
				return _searchDebtor ??
				  (_searchDebtor = new RelayCommand((o) =>
				  {
					  IEnumerable<Rental> rentals = _context.Rentals
					  .Include(b => b.Book)
					  .Include(s => s.Student)
					  .Where(x => x.isReturned == false);

					  Rentals.Clear();
					  foreach (var rental in rentals)
					  {
						  Rentals.Add(rental);
					  }
				  }));
			}
		}

		private RelayCommand? _searchForStorageCommand;
		public RelayCommand SearchForStorageCommand
		{
			get
			{
				return _searchForStorageCommand ??
				  (_searchForStorageCommand = new RelayCommand((o) =>
				  {
					  IEnumerable<Storage> storage = _context.Storage
					   .Include(b => b.Book)
					   .Include(b => b.Branch);

					  if (!string.IsNullOrWhiteSpace(SearchBookStorage))
					  {
						  storage = storage.Where(s => s.Book.Title.Contains(SearchBookStorage));
					  }
					  if (!string.IsNullOrWhiteSpace(SearchBranchStorage))
					  {
						  storage = storage.Where(s => s.Branch.Name.Contains(SearchBranchStorage));
					  }
					  if (!string.IsNullOrWhiteSpace(SearchLocationStorage))
					  {
						  storage = storage.Where(s => s.Branch.Location.Contains(SearchLocationStorage));
					  }

					  // Очищаем и добавляем отфильтрованные данные
					  Storage.Clear();
					  foreach (var s in storage)
					  {
						  Storage.Add(s);
					  }
				  }));
			}
		}

		private RelayCommand? _createReportCommand;
		public RelayCommand CreateReportCommand
		{
			get
			{
				return _createReportCommand ??
				  (_createReportCommand = new RelayCommand((parameter) =>
				  {
					  if (parameter == null) return;

					  var saveFileDialog = new SaveFileDialog
					  {
						  Filter = "Excel Files|*.xlsx",
						  Title = "Сохранить Excel файл",
						  FileName = "Export.xlsx"
					  };

					  if (saveFileDialog.ShowDialog() == true)
					  {
						  try
						  {
							  // Создание Excel файла
							  using (var package = new ExcelPackage())
							  {
								  var worksheet = package.Workbook.Worksheets.Add("Отчет");

								  int row = 2;
								  if (parameter.ToString() == "Rentals")
								  {
									  worksheet.Cells[1, 1].Value = "Название книги";
									  worksheet.Cells[1, 2].Value = "Фамилия студента";
									  worksheet.Cells[1, 3].Value = "Имя студента";
									  worksheet.Cells[1, 4].Value = "Название факультета";
									  worksheet.Cells[1, 5].Value = "Дата выдачи";
									  worksheet.Cells[1, 6].Value = "Дата возврата";
									  worksheet.Cells[1, 7].Value = "Возвращено";
									  worksheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;

									  foreach (var item in Rentals)
									  {
										  worksheet.Cells[row, 1].Value = item.Book.Title;
										  worksheet.Cells[row, 2].Value = item.Student.LastName;
										  worksheet.Cells[row, 3].Value = item.Student.FirstName;
										  worksheet.Cells[row, 4].Value = item.Student.Group.Speciality.Faculty.Name;
										  worksheet.Cells[row, 5].Value = item.RentalDate.ToString("yyyy-MM-dd");
										  worksheet.Cells[row, 6].Value = item.ReturnDate?.ToString("yyyy-MM-dd");
										  worksheet.Cells[row, 7].Value = item.isReturned;
										  row++;
									  }
								  }

								  if (parameter.ToString() == "Storage")
								  {
									  worksheet.Cells[1, 1].Value = "Название книги";
									  worksheet.Cells[1, 2].Value = "Наименование зала";
									  worksheet.Cells[1, 3].Value = "Местонахождение";
									  worksheet.Cells[1, 4].Value = "Количество";
									  worksheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;

									  foreach (var item in Storage)
									  {
										  worksheet.Cells[row, 1].Value = item.Book.Title;
										  worksheet.Cells[row, 2].Value = item.Branch.Name;
										  worksheet.Cells[row, 3].Value = item.Branch.Location;
										  worksheet.Cells[row, 4].Value = item.Count;

										  row++;
									  }
								  }

								  worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

								  var fileInfo = new FileInfo(saveFileDialog.FileName);
								  package.SaveAs(fileInfo);

								  System.Windows.MessageBox.Show("Отчет сформирован успешно", "Информация");
							  }
						  }
						  catch (Exception ex)
						  {
							  System.Windows.MessageBox.Show($"Ошибка формирования отчета: {ex.Message}", "Ошибка");
						  }
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

		private RelayCommand? _getBookHistory;

		public RelayCommand GetBookHistoryCommand
		{
			get
			{
				return _getBookHistory ??= new RelayCommand((selectedItem) =>
				{
					// Приведение выбранного элемента к типу Book
					Book? book = selectedItem as Book;
					if (book == null) return;

					// Запрос истории книги
					var bookHistory = _context.Rentals
						.Where(r => r.BookId == book.Id) // Фильтр по книге через Book.Id
						.OrderByDescending(r => r.RentalDate) // Сортировка по дате выдачи
						.Select(r => r)
						.ToList();

					// Открытие окна истории книги
					BookHistoryWindow userWindow = new BookHistoryWindow(bookHistory); // Передаём историю книги в окно
					userWindow.ShowDialog();
				});
			}
		}

	}
}
