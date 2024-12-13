using System.Configuration;
using System.Data;
using System.Windows;

namespace BooksLibrary
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
	}

}
dotnet ef dbcontext scaffold "server=localhost;database=bookslibrary;user=root;password=1234;" Pomelo.EntityFrameworkCore.MySql -o Models
