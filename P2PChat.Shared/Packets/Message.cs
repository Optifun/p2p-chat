using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;

namespace P2PChat.Packets
{
	[Serializable]
	public class Message : BasePacket
	{
		public Guid Id { get; protected set; }
		public Guid Sender => _sender;
		public Guid Reciever => _receiver;
		public string Text => _msg;

		private Guid _sender;
		private Guid _receiver;
		private string _msg;

		public Message (Guid sender, Guid receiver, string message)
		{
			Id = Guid.NewGuid();
			_sender = sender;
			_receiver = receiver;
			_msg = message;
		}

		public static Message Parse (Packet packet)
		{
			MemoryStream stream = new MemoryStream(packet.Data);
			var msg = _fmt.Deserialize(stream) as Message;

			if ( msg is null || msg.Text == null )
				return null;

			return msg;
		}

	}
}
