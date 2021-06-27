using System;
using System.Collections.Generic;
using System.Text;
using P2PChat.Packets;
using P2PChat.Reciever;
using P2PChat;
using System.Net;

namespace P2PChat.Server.Resolver
{
	public class UsersOnline : AbstractRoute
	{
		public event Action<IPEndPoint> UsersRequested;

		public override Action Handle (NetworkData networkData)
		{
			var userPacket = OnlineUsers.Parse(networkData);
			if ( userPacket == null || userPacket.Users == null || userPacket.Action == FetchAction.Null )
				return base.Handle(networkData);

			var ip = networkData.Sender.Address;
			var port = userPacket.Port;
			return () => UsersRequested?.Invoke(new IPEndPoint(ip, port));
		}
	}
}
