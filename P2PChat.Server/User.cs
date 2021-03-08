using System;
using System.Collections.Generic;
using System.Text;

namespace P2PChat.Server.Db
{
	public class User
	{
		public Guid UserId { get; set; }
		public string Nickname { get; set; }
		public string PasswordHash { get; set; }
	}
}
