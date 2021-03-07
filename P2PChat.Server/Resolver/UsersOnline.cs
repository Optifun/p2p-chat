using System;
using System.Collections.Generic;
using System.Text;
using P2PChat.Packets;
using P2PChat.Reciever;
using P2PChat;
using System.Net;

namespace P2PChat.Server.Resolver
{
	public class UsersOnline: AbstractRoute
	{
		public event Action<IPEndPoint> UsersRequested;

		public override Action Handle (Packet packet)
		{
			var userPacket = OnlineUsers.Parse(packet);
			var ip = packet.Sender.Address;
			var port = userPacket.Port;
			if ( userPacket != null && userPacket.Users != null && userPacket.Action == FetchAction.Fetch )
				return () => UsersRequested?.Invoke(new IPEndPoint(ip, port));
			return base.Handle(packet);
		}
	}
}
