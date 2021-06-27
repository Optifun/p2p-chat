using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;
using P2PChat.Packets;
using System.Diagnostics;

namespace P2PChat.Client.Routes
{
	class UserObserver : AbstractRoute
	{
		public event Action<List<PublicUser>> UsersRecieved;

		public override Action Handle (NetworkData networkData)
		{
			var users = OnlineUsers.Parse(networkData);
			if ( users != null && users.Users != null && users.Action!= FetchAction.Null)
				return () => UsersRecieved?.Invoke(users.Users);
			return base.Handle(networkData);

		}
	}
}
