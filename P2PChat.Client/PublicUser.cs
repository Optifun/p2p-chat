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
		public IPEndPoint Address;

		public override string ToString ()
		{
			return Nickname + " [" + UserID + "]";
		}
	}
}
