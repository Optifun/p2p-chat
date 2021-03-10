using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2PChat.Client.DB
{
	[Table("Message")]
	public class Message
	{
		public Guid MessageId { get; set; }

		[ForeignKey("UserId")]
		public Guid ChatId { get; set; }

		public virtual User User { get; set; }

		public bool Self { get; set; }

		public string Text { get; set; }

		public Message (Guid id, Guid sender, bool self, string text)
		{
			MessageId = id;
			ChatId = sender;
			Self = self;
			Text = text;
		}
	}
}
