using System;
using System.Net;
using P2PChat.Packets;
using P2PChat.Reciever;

namespace P2PChat.Server.Resolver
{
	public class UsersOnline : AbstractRoute
	{
		public event Action<IPEndPoint> UsersRequested;

		public override Action Handle(IPEndPoint sender, IPacket obj)
		{
			var userPacket = obj as OnlineUsers;
			if (userPacket == null || userPacket.Users == null || userPacket.Action == FetchAction.Null)
				return base.Handle(sender, obj);

			var ip = sender.Address;
			var port = userPacket.Port;
			return () => UsersRequested?.Invoke(new IPEndPoint(ip, port));
		}
	}
}
