using BooksLibrary.Models.DataBase;
using BooksLibrary.Utils;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

		private RelayCommand? _createReportCommand;
		public RelayCommand CreateReportCommand
		{
			get
			{
				return _createReportCommand ??
				  (_createReportCommand = new RelayCommand((o) =>
				  {
					  if (Rentals.Count <= 0) return;

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
							 
							  using (var package = new ExcelPackage())
							  {
								  var worksheet = package.Workbook.Worksheets.Add("Отчет");

								  int row = 2;

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
	}

}
