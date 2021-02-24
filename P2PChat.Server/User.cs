using System;
using System.Collections.Generic;
using System.Text;

namespace P2PChat.Server.Db
{
	public class User
	{
		public Guid UserID;
		public string Nickname;
		public string PasswordHash;
	}
}
