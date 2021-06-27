using System;
using System.Collections.Generic;
using System.Text;
using P2PChat.Packets;
using P2PChat.Reciever;
using P2PChat;
using System.Net;
using P2PChat.Packets;
using P2PChat.Server.Db;
using System.Threading.Tasks;

namespace P2PChat.Server.Resolver
{
	class Authentification : AbstractRoute
	{
		public event Action<AuthAction, IPEndPoint> AuthRequested;

		UserDb userDb;

		public Authentification ()
		{
		}

		public override Action Handle (NetworkData networkData)
		{
			var authPacket = AuthAction.Parse(networkData);
			if ( authPacket == null || authPacket.Action == AuthType.Null || authPacket.OpenPort == null )
				return base.Handle(networkData);

			int port = authPacket.OpenPort ?? 0;
			var responce = resolveAuthAction(authPacket);
			return () => AuthRequested?.Invoke(responce, new IPEndPoint(networkData.Sender.Address, port));
		}

		private AuthAction resolveAuthAction (AuthAction request)
		{
			switch ( request.Action )
			{
				case AuthType.Null:
				return request;
				case AuthType.Register:
				return handleRegistration(request);
				case AuthType.Login:
				return handleLogin(request);
				default:
				return new AuthAction(request.Nickname, request.Password, AuthType.Null);
			}
		}


		private AuthAction handleRegistration (AuthAction packet)
		{
			using ( var userDb = new UserDb() )
			{
				int port = packet.OpenPort.Value;
				var entry = userDb.SearchByNickname(packet.Nickname);
				AuthAction responce;
				if ( entry != null )
				{
					responce = new AuthAction(packet.Nickname, packet.Password, AuthType.WrongCredentials, port);
				}
				else
				{
					User usr = new User()
					{
						UserId = Guid.NewGuid(),
						Nickname = packet.Nickname,
						PasswordHash = packet.Password
					};

					usr = userDb.Users.Add(usr);
					userDb.SaveChangesAsync();
					responce = new AuthAction(usr.UserId, usr.Nickname, usr.PasswordHash, AuthType.Success, port);
				}
				return responce;
			}
		}

		private AuthAction handleLogin (AuthAction packet)
		{
			using ( userDb = new UserDb() )
			{
				int port = packet.OpenPort.Value;
				var entry = userDb.SearchByNickname(packet.Nickname);
				AuthAction responce;
				if ( entry != null )
				{
					if ( entry.PasswordHash == packet.Password && entry.Nickname == packet.Nickname )
						responce = new AuthAction(entry.UserId, entry.Nickname, entry.PasswordHash, AuthType.Success, port);
					else
						responce = new AuthAction(packet.Nickname, packet.Password, AuthType.WrongCredentials, port);
				}
				else
				{
					responce = new AuthAction(packet.Nickname, packet.Password, AuthType.WrongCredentials, port);
				}
				return responce;
			}
		}
	}
}
