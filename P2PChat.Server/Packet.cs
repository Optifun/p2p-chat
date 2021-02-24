using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using P2PChat.Server.Db;

namespace P2PChat.Auth
{
	enum AuthAction
	{
		Undefined = 0,
		Register,
		Login,
		UserExisting,
		WrongCredentials,
		Success
	}

	[Serializable]
	class Packet
	{
		static BinaryFormatter _fmt = new BinaryFormatter();
		public string Nickname;
		public string Password;
		public AuthAction Action;

		public static Packet Parse (byte[] data)
		{
			MemoryStream stream = new MemoryStream(data);
			var action = (AuthAction)stream.ReadByte();
			var packet = _fmt.Deserialize(stream) as Packet;

			if ( packet is null || packet.Nickname == "" || packet.Password == "" )
				throw new Exception("Bad request");

			return action switch
			{
				AuthAction.Register => packet as RegisterPacket,
				AuthAction.Login => packet as LoginPacket,
				_ => null,
			};
		}

		public static IEnumerable<byte> ToBytes (Packet packet)
		{
			MemoryStream stream = new MemoryStream();
			_fmt.Serialize(stream, packet);
			var buffer = stream.ToArray();
			return buffer.Prepend((byte)packet.Action);
		}
	}

	[Serializable]
	class RegisterPacket : Packet
	{
		public RegisterPacket (string nickname, string password)
		{
			Nickname = nickname;
			Password = password;
		}
	}

	[Serializable]
	class LoginPacket : Packet
	{
		public LoginPacket (string nickname, string password)
		{
			Nickname = nickname;
			Password = password;
		}
	}

	[Serializable]
	class WrongCredentials : Packet
	{
		public WrongCredentials (string nickname, string password)
		{
			Nickname = nickname;
			Password = password;
		}
	}

	[Serializable]
	class Success : Packet
	{
		public User User => _user;
		readonly User _user;
		Guid UserID;

		public Success (Guid id, string nickname, string password)
		{
			UserID = id;
			Nickname = nickname;
			Password = password;
			_user = new User() { UserID = id, Nickname = nickname, PasswordHash = password };
		}

	}

}