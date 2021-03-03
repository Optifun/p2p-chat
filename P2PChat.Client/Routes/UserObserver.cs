using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;
using P2PChat.Client.Packets;
using System.Diagnostics;

namespace P2PChat.Client.Routes
{
	class UserObserver : AbstractRoute
	{
		public event Action<List<PublicUser>> UsersRecieved;

		public override Action Handle (Packet packet)
		{
			var users = OnlineUsers.Parse(packet);
			if ( users != null && users.Users != null && users.Action!= FetchAction.Null)
				return () => UsersRecieved?.Invoke(users.Users);
			return base.Handle(packet);

		}
	}
}
