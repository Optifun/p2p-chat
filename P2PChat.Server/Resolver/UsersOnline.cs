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
			var users = OnlineUsers.Parse(packet);
			if ( users != null && users.Users != null && users.Action == FetchAction.Fetch )
				return () => UsersRequested?.Invoke(packet.Sender);
			return base.Handle(packet);
		}
	}
}
