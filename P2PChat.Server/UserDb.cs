using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Linq;
using System.Data.SQLite;

namespace P2PChat.Server.Db
{
	public static class UserExtension
	{
		public static User SearchByNickname (this UserDb db, string nickname)
		{
			if ( db.Users.Count() == 0 )
				return null;

			var result = db.Users.Where(usr => usr.Nickname == nickname);
			if ( result.Count() == 0 )
				return null;

			return result.First();
		}
	}

	public class UserDb : DbContext
	{
		public DbSet<User> Users { get; set; }

		public UserDb () : base("DB.UserDb")//: base("Data Source=(localdb)/Users.sqlite;Initial Catalog=UserDb;Integrated Security=True;ApplicationIntent = ReadWrite;")
		{
		}

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			CreateTables();
			base.OnModelCreating(modelBuilder);
		}

		protected void CreateTables ()
		{
			const string sqlTextCreateTables = @"
			CREATE TABLE IF NOT EXISTS Users
			(
				UserId NVARCHAR(32) PRIMARY KEY NOT NULL,
				Nickname NVARCHAR(32) NOT NULL,
				PasswordHash NVARCHAR(64) NOT NULL
			);";

			var connectionString = this.Database.Connection.ConnectionString;
			using ( var dbConnection = new System.Data.SQLite.SQLiteConnection(connectionString) )
			{
				dbConnection.Open();
				using ( var dbCommand = dbConnection.CreateCommand() )
				{
					dbCommand.CommandText = sqlTextCreateTables;
					dbCommand.ExecuteNonQuery();
				}
			}
		}
	}
}
