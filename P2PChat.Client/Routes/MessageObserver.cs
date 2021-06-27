using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;
using P2PChat.Packets;

namespace P2PChat.Client.Routes
{
	class MessageObserver : AbstractRoute
	{
		public event Action<Message> MessageRecieved;
		Guid seldId;

		public MessageObserver (Guid self)
		{
			seldId = self;
		}

		public override Action Handle (NetworkData networkData)
		{
			var msg = Message.Parse(networkData);
			if ( msg != null )
			{
				// TODO: раскомментировать
				return () => MessageRecieved?.Invoke(msg);
				//if ( msg.Reciever == seldId )
				//	return () => MessageRecieved?.Invoke(msg);
				//else
				//	return () => { };
			}
			return base.Handle(networkData);
		}

	}
}
