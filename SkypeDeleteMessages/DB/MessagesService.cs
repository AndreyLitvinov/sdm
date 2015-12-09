using SkypeDeleteMessages.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
			this.connection = new SQLiteConnection(string.Format("Data Source={0};",connectionString));
			this.connection.Open();
			if (this.connection.State != ConnectionState.Open)
			{
				throw new Exception("Не открыто подключение");
			}
		}

		public MessagesService(SQLiteConnection connectionSqlite)
		{
			this.connection = connectionSqlite;
		}

		public async Task<List<Message>> getMessagesByConvo_id(int convo_id)
		{
			List<Message> result = new List<Message>();
			using (SQLiteCommand fmd = connection.CreateCommand())
			{
				
				fmd.CommandText = @"
SELECT
Messages.id,
Messages.convo_id,
Messages.author,
Messages.body_xml
FROM
Messages
WHERE Messages.convo_id = @convoId;
";
				fmd.CommandType = CommandType.Text;
				fmd.Parameters.Add("@convoId", DbType.Int32);
				fmd.Parameters["@convoId"].Value = convo_id;

				DbDataReader r = await fmd.ExecuteReaderAsync();
				while (r.Read())
				{
					Message tmp = new Message();
					tmp.Id = Convert.ToInt32(r["id"]);
					tmp.convo_id = Convert.ToInt32(r["convo_id"]);
					tmp.Author = Convert.ToString(r["author"]);
					tmp.Text = Convert.ToString(r["body_xml"]);
					result.Add(tmp);
				}
			}
			return result;
		}

		public void DeleteMesageById(int id_message)
		{
			using (SQLiteCommand fmd = connection.CreateCommand())
			{

				fmd.CommandText = @"
DELETE FROM Messages
WHERE Messages.id = @mesId;
";
				fmd.CommandType = CommandType.Text;
				fmd.Parameters.Add("@mesId", DbType.Int32);
				fmd.Parameters["@mesId"].Value = id_message;

				fmd.ExecuteNonQuery();
			}
		}

		public void DeleteMessagesByConvo_id(int convo_id)
		{
			using (SQLiteCommand fmd = connection.CreateCommand())
			{

				fmd.CommandText = @"
DELETE FROM Messages
WHERE Messages.convo_id = @convoId;
";
				fmd.CommandType = CommandType.Text;
				fmd.Parameters.Add("@convoId", DbType.Int32);
				fmd.Parameters["@convoId"].Value = convo_id;

				fmd.ExecuteNonQuery();
			}
		}
	}
}
