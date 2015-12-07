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
	class ConversationsService
	{
		private SQLiteConnection connection { get; set; }

		public ConversationsService(string connectionString)
		{
			this.connection = new SQLiteConnection(connectionString);
			this.connection.Open();
			if (this.connection.State != ConnectionState.Open)
			{
				throw new Exception("Не открыто подключение");
			}
		}

		public List<Conversations> getAllConversations()
		{
			//				fmd.CommandText = @"
			//SELECT 
			//Conversations.id,
			//Conversations.displayname
			//FROM 
			//Conversations
			//WHERE 
			//Conversations.id = @convo_id;";
			return null;
		}
	}
}
