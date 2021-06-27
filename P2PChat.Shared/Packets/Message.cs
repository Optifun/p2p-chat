using System;

namespace P2PChat.Packets
{
	[Serializable]
	public class Message : BasePacket
	{
		public override string Type => "user-message";
		public Guid Id { get; protected set; }
		public Guid Sender => _sender;
		public Guid Reciever => _receiver;
		public string Text => _msg;

		private Guid _sender;
		private Guid _receiver;
		private string _msg;


		public Message(Guid sender, Guid receiver, string message)
		{
			Id = Guid.NewGuid();
			_sender = sender;
			_receiver = receiver;
			_msg = message;
		}
	}
}