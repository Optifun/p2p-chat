using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Linq;

namespace P2PChat.Server.Db
{
	public class UserDb : DbContext
	{
		public DbSet<User> Users;

		public User SearchByNickname (string nickname)
		{
			var result = from u in Users.AsParallel()
						 where u.Nickname == nickname
						 select u;
			try
			{
				return result.Single();
			}
			catch ( InvalidOperationException ex )
			{
				return null;
			}
		}
	}
}
