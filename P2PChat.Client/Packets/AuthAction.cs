using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Packets;
using P2PChat;
using System.IO;
using P2PChat.Reciever;

namespace P2PChat.Packets
{
	public enum AuthType
	{
		Null = 0,
		Register,
		Login,
		UserExisting,
		WrongCredentials,
		Success
	}

	[Serializable]
	public class AuthAction : BasePacket
	{
		public Guid? Id { get; protected set; }
		public string Nickname { get; protected set; }
		public string Password { get; protected set; }
		public AuthType Action { get; protected set; }

		public int? OpenPort { get; protected set; }

		public AuthAction (string nickname, string password, AuthType action) : this(null, nickname, password, action, null)
		{
		}

		public AuthAction (string nickname, string password, AuthType action, int port) : this(null, nickname, password, action, port)
		{
		}

		public AuthAction (Guid? id, string nickname, string password, AuthType action, int? port=null)
		{
			Id = id;
			Nickname = nickname;
			Password = password;
			Action = action;
			OpenPort = port;
		}

		public static AuthAction Parse (Packet packet)
		{
			MemoryStream stream = new MemoryStream(packet.Data);
			var _packet = _fmt.Deserialize(stream) as AuthAction;

			if ( _packet == null || _packet.Nickname == null || _packet.Password == null )
				return null;

			return _packet;
		}


	}
}
