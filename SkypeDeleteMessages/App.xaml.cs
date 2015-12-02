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
	}
}
