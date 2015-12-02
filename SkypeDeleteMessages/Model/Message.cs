using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeDeleteMessages.Model
{
	class Message
	{
		public int? Id { get; set; }
		public int? convo_id { get; set; }
		public string Author { get; set; }
		public string Text { get; set; }
	}
}