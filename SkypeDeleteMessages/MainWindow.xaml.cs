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
using System.Windows.Media.Animation;
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
			this.TextBoxURLDB.Text = string.Format("{0}\\Skype\\mifodiman\\main.db", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			this.OpenDB(this.TextBoxURLDB.Text);
			this.TextBoxFindeMes.Text =
					this.TextBoxFindeCon.Text = CONST_FINDE;
		}

		private SQLiteConnection connection { get; set; }
		private ConversationsService cService { get; set; }
		private MessagesService mService { get; set; }
		private List<Message> currentListMessages { get; set; }
		private List<Conversations> currentListConversations { get; set; }
		const string CONST_FINDE = "Поиск";

		private enum StatusWork
		{
			Success,
			Error
		}

		//		try
		//		{
		//			this.SetStatusWork(StatusWork.Success, "");
		//		}
		//		catch (Exception ex)
		//		{
		//			this.SetStatusWork(StatusWork.Error, ex.Message);
		//		}

		private void SetStatusWork(StatusWork status, string mes = "")
		{
			this.TextBlockInfo.Text = string.Format("{0}. {1}", status == StatusWork.Success ? "Готово" : "Ошибка", mes);
			//TODO: возможно не нужно мегание при Success либо сделать его еле заметным
			SolidColorBrush colorInfo = Application.Current.Resources["BgSuccess"] as SolidColorBrush;
			Color startColor = Colors.Green;
			if (status == StatusWork.Error)
			{
				colorInfo = Application.Current.Resources["BgDanger"] as SolidColorBrush;
				startColor = Colors.Red;
			}

			this.BorderTBInfo.Background = new SolidColorBrush(colorInfo.Color);
			ColorAnimation da = new ColorAnimation();
			da.From = startColor;
			da.By = colorInfo.Color;
			da.Duration = TimeSpan.FromSeconds(1);
			BorderTBInfo.Background.BeginAnimation(SolidColorBrush.ColorProperty, da);
		}

		private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
		{
			try
			{

				// Create OpenFileDialog
				OpenFileDialog dlg = new OpenFileDialog();

				// Set filter for file extension and default file extension
				dlg.DefaultExt = ".db";
				dlg.Filter = "File DATABASE (.db)|*.db";
				dlg.InitialDirectory = string.Format("{0}\\Skype\\", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				// Display OpenFileDialog by calling ShowDialog method
				Nullable<bool> result = dlg.ShowDialog();

				// Get the selected file name and display in a TextBox
				if (result == true)
				{
					// Open document
					string filename = dlg.FileName;

					TextBoxURLDB.Text = filename;
					this.OpenDB(filename);
				}
			}
			catch (Exception ex)
			{
				this.SetStatusWork(StatusWork.Error, ex.Message);
			}
		}

		public void OpenDB(string FileDB) 
		{
			try
			{
				this.connection = new SQLiteConnection(string.Format("Data Source={0};", FileDB));
					this.connection.Open();

					if (this.connection.State != ConnectionState.Open)
					{
						throw new Exception("Не открыто подключение");
					}
					mService = new MessagesService(this.connection);
					cService = new ConversationsService(this.connection);
					this.SetStatusWork(StatusWork.Success);
					this.UpdateListBoxConversations();
			}
			catch (Exception ex)
			{
				this.SetStatusWork(StatusWork.Error, ex.Message);
			}
		}

		private void UpdateListBoxConversations()
		{
			try
			{
				this.currentListConversations = cService.getAllConversations();
				this.ListBoxConversations.ItemsSource = this.currentListConversations;
				this.SetStatusWork(StatusWork.Success, "Диалоги получены");
			}
			catch (Exception ex)
			{
				this.SetStatusWork(StatusWork.Error, ex.Message);
			}
		}

		private async void UpdateListBoxMessages()
		{
			try
			{
				Conversations itemSel = this.ListBoxConversations.SelectedItem as Conversations;
				if (itemSel != null)
				{
					this.currentListMessages = await mService.getMessagesByConvo_id((int)itemSel.Id);
					string countMes = "";
					if (this.currentListMessages.Count != 0)
					{
						countMes = "Сообщения получены";
					}
					else
					{
						countMes = "Нет сообщений";
					}
					this.ListBoxMessages.ItemsSource = this.currentListMessages;
					this.SetStatusWork(StatusWork.Success, countMes);
				}

			}
			catch (Exception ex)
			{
				this.SetStatusWork(StatusWork.Error, ex.Message);
			}
		}

		private void ListBoxConversations_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.UpdateListBoxMessages();
		}

		private void ButtonDeleteMessage_Click(object sender, RoutedEventArgs e)
		{
			Button btnDel = sender as Button;
			if (btnDel != null)
			{
				try
				{
					mService.DeleteMesageById((int)btnDel.Tag);
					this.UpdateListBoxMessages();
					this.SetStatusWork(StatusWork.Success, "Сообщение удалено!");
				}
				catch (Exception ex)
				{
					this.SetStatusWork(StatusWork.Error, ex.Message);
				}
			}
		}

		private void ButtomDeeleteMessages_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Conversations itemSel = this.ListBoxConversations.SelectedItem as Conversations;
				if (itemSel != null)
				{
					this.mService.DeleteMessagesByConvo_id((int)itemSel.Id);
					this.UpdateListBoxMessages();
					this.SetStatusWork(StatusWork.Success, "Переписка удалена");
				}
			}
			catch (Exception ex)
			{
				this.SetStatusWork(StatusWork.Error, ex.Message);
			}

		}

		private void TextBoxFinde_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = sender as TextBox;
			if (tb != null)
			{
				if (tb.Text == CONST_FINDE)
				{
					tb.Text = "";
				}
			}
		}

		private void TextBoxFinde_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = sender as TextBox;
			if (tb != null)
			{
				if (tb.Text == "")
				{
					tb.Text = CONST_FINDE;
				}
			}
		}

		private void FindeMessage(string filter)
		{
			if (filter != "" && filter != CONST_FINDE)
			{
				if (this.currentListMessages != null)
				{
					this.ListBoxMessages.ItemsSource = this.currentListMessages.Where(mes => mes.Text.ToUpper().Contains(filter.ToUpper()));
				}
			}
			else
			{
				this.ListBoxMessages.ItemsSource = this.currentListMessages;
			}
		}

		private void FindeConversations(string filter)
		{
			if (filter != "" && filter != CONST_FINDE)
			{
				if (this.currentListConversations != null)
				{
					this.ListBoxConversations.ItemsSource = this.currentListConversations.Where(con => con.Name.ToUpper().Contains(filter.ToUpper()));
				}
			}
			else
			{
				this.ListBoxConversations.ItemsSource = this.currentListConversations;
			}
		}

		private void TextBoxFindeCon_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.FindeConversations(TextBoxFindeCon.Text);
		}

		private void TextBoxFindeMes_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.FindeMessage(TextBoxFindeMes.Text);
		}

		private void CleanerFindeMes_Click(object sender, RoutedEventArgs e)
		{
			this.TextBoxFindeMes.Text = CONST_FINDE;
		}

		private void CleanerFindeCon_Click(object sender, RoutedEventArgs e)
		{
			this.TextBoxFindeCon.Text = CONST_FINDE;
		}
	}
}
