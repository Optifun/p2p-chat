using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace P2PChat
{
	[Serializable]
	public class PublicUser
	{
		public Guid UserID;
		public string Nickname;
		[NonSerialized]
		public IPEndPoint Address;
		public string StrAddress;
		public int port;

		public PublicUser ()
		{
			UserID = Guid.NewGuid();
		}

		public PublicUser (IPAddress ip, int port, Guid id, string nickname)
		{
			UserID = id;
			Nickname = nickname;
			StrAddress = ip.ToString();
			this.port = port;
			Address = new IPEndPoint(IPAddress.Parse(StrAddress), this.port);
		}

		public PublicUser (IPEndPoint ip, Guid id, string nickname)
		{
			UserID = id;
			Nickname = nickname;
			StrAddress = ip.Address.ToString();
			port = ip.Port;
			Address = ip;
		}

		public override bool Equals (object obj)
		{
			var user = obj as PublicUser;
			if ( user != null )
				return user.UserID == UserID;
			else
				return false;
		}

		public override int GetHashCode ()
		{
			return UserID.GetHashCode();
		}

		public override string ToString ()
		{
			return Nickname + " [" + UserID + "]";
		}
	}
}
