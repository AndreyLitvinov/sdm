using Microsoft.Win32;
using SkypeDeleteMessages.DB;
using SkypeDeleteMessages.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkypeDeleteMessages
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private SQLiteConnection connection { get; set; }

		private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
		{
			// Create OpenFileDialog
			OpenFileDialog dlg = new OpenFileDialog();

			// Set filter for file extension and default file extension
			dlg.DefaultExt = ".db";
			dlg.Filter = "File DATABASE (.db)|*.db";

			// Display OpenFileDialog by calling ShowDialog method
			Nullable<bool> result = dlg.ShowDialog();

			// Get the selected file name and display in a TextBox
			if (result == true)
			{
				// Open document
				string filename = dlg.FileName;

				TextBoxURLDB.Text = filename;
				this.connection = new SQLiteConnection(string.Format("Data Source={0};", filename));
				this.connection.Open();

				if (this.connection.State != ConnectionState.Open)
				{
					throw new Exception("Не открыто подключение");
				}
				this.UpdateDataGridConversations();
			}
		}

		private void UpdateDataGridConversations()
		{
			ConversationsService cService = new ConversationsService(this.connection);
			List<Conversations> conv = cService.getAllConversations();
			this.DataGridConversations.ItemsSource = conv;
			//this.DataGridMessages.bin
		}

		private void DataGridConversations_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			Conversations itemSel = this.DataGridConversations.SelectedItem as Conversations;
			if (itemSel != null)
			{
				MessagesService mService = new MessagesService(this.connection);
				List<Message> mes = mService.getMessagesByConvo_id((int)itemSel.Id);
				this.DataGridMessages.ItemsSource = mes;
			}


		}
	}
}
