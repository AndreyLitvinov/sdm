using SkypeDeleteMessages.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeDeleteMessages.DB
{
	public class MessagesService
	{

		private SQLiteConnection connection { get; set; }

		public MessagesService(string connectionString)
		{
			this.connection = new SQLiteConnection(connectionString);
			this.connection.Open();
			if (this.connection.State != ConnectionState.Open)
			{
				throw new Exception("Не открыто подключение");
			}
		}

		public List<Message> getMessagesByConvo_id(int convo_id)
		{
			return new List<Message>();
		}

		public void DeleteMesageById(int id_message)
		{

		}

		public void DeleteMessagesByConvo_id(int convo_id)
		{

		}
	}
}
