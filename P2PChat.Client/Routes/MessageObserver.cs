using System;
using System.Net;
using P2PChat.Reciever;
using P2PChat.Packets;

namespace P2PChat.Client.Routes
{
	public class MessageObserver : AbstractRoute
	{
		public event Action<Message> MessageReceived;
		private readonly Guid _seldId;

		public MessageObserver(Guid self)
		{
			_seldId = self;
		}

		public override Action Handle(IPEndPoint sender, IPacket obj)
		{
			if (obj is Message msg && msg.Reciever == _seldId)
				return () => MessageReceived?.Invoke(msg);

			return base.Handle(sender, obj);
		}
	}
}