using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;
using P2PChat.Packets;
using System.Net;

namespace P2PChat.Client.Routes
{
	class AuthObserver : AbstractRoute
	{
		public event Action<PublicUser> Success;
		public event Action<string> Error;

		public override Action Handle (NetworkData networkData)
		{
			var authPacket = AuthAction.Parse(networkData);
			if ( authPacket == null || authPacket.Nickname == null )
				return base.Handle(networkData);
			if ( authPacket.OpenPort == null )
				return () => { new Exception("Server doesn't provided open port"); };

			var ip = new IPEndPoint(IPAddress.Loopback, authPacket.OpenPort.Value);

			switch ( authPacket.Action )
			{
				case AuthType.UserExisting:
				return () => { Error?.Invoke("Login is already reeserved"); };
				case AuthType.WrongCredentials:
				return () => { Error?.Invoke("Wrong password"); };
				case AuthType.Success:
					{
						if ( authPacket.Id == null )
							return () => { new Exception("Server could not generete UUID"); };
						Guid id = authPacket.Id.Value;
						return () => { Success?.Invoke(new PublicUser(ip, id, authPacket.Nickname)); };
					}
				default:
				return () => { };
			}
		}
	}
}
