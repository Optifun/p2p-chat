using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Client.DB
{
	class ChatDb : DbContext
	{
		public DbSet<User> Users;
		public DbSet<Message> Messages;

		public ChatDb () : base("DB.ChatDb")
		{
		}

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Message>().HasRequired(msg => msg.User);
		}
	}
}
