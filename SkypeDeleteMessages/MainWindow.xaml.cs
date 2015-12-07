using Microsoft.Win32;
using SkypeDeleteMessages.DB;
using SkypeDeleteMessages.Model;
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
				this.UpdateDataGridConversations(filename);
			}
		}

		private void UpdateDataGridConversations(string fileURL) 
		{
			MessagesService mService = new MessagesService(fileURL);
			List<Message> mes = mService.getMessagesByConvo_id(2621);
			this.DataGridMessages.ItemsSource = mes;
			//this.DataGridMessages.bin
		}
	}
}
