using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PChat.Client
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			var user = new PublicUser(IPAddress.Parse("127.0.0.1"), 6666, Guid.NewGuid(), "Nick");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(user));
		}
	}
}
