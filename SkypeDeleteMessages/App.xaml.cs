using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SkypeDeleteMessages
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		private SQLiteConnection DBConnection(string source)
		{
			SQLiteConnection conn = new SQLiteConnection("Data Source=filename.db; Version=3;");
			try
			{
				conn.Open();
				return conn;
			}
			catch (SQLiteException ex)
			{
				return null;
			}

			if (conn.State == ConnectionState.Open)
			{
				// ******
			}
			//conn.Dispose();
		}
		// Create OpenFileDialog 
		//Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



		//// Set filter for file extension and default file extension 
		//dlg.DefaultExt = ".png";
		//dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; 


		//// Display OpenFileDialog by calling ShowDialog method 
		//Nullable<bool> result = dlg.ShowDialog();


		//// Get the selected file name and display in a TextBox 
		//if (result == true)
		//{
		//	// Open document 
		//	string filename = dlg.FileName;
		//	textBox1.Text = filename;
		//}
	}
}
