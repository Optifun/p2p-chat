using System;

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
		public override string Type => "auth-action";
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
	}
}
