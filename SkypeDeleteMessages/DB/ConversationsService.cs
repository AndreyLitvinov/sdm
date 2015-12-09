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
			this.connection = new SQLiteConnection(string.Format("Data Source={0};", connectionString));
			this.connection.Open();
			if (this.connection.State != ConnectionState.Open)
			{
				throw new Exception("Не открыто подключение");
			}
		}

		public ConversationsService(SQLiteConnection connectionSqlite)
		{
			this.connection = connectionSqlite;
		}

		public List<Conversations> getAllConversations()
		{
			List<Conversations> result = new List<Conversations>();
			using (SQLiteCommand fmd = connection.CreateCommand())
			{
				fmd.CommandText = @"
SELECT 
Conversations.id,
Conversations.displayname,
Conversations.identity
FROM 
Conversations;
";
				fmd.CommandType = CommandType.Text;
				SQLiteDataReader r = fmd.ExecuteReader();
				while (r.Read())
				{
					Conversations tmp = new Conversations();
					tmp.Id = Convert.ToInt32(r["id"]);
					tmp.Identitys = Convert.ToString(r["identity"]);
					tmp.Name = Convert.ToString(r["displayname"]);
					result.Add(tmp);
				}
			}
			return result;
		}
	}
}
