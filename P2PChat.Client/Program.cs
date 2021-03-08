using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2PChat.Client.Properties;

namespace P2PChat.Client
{
	static class Program
	{
		public static IPEndPoint stanAddress;
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			resolveStanIP();
			Application.Run(new AuthForm(stanAddress));
		}

		private static void resolveStanIP ()
		{
			var settings = new Settings();
			var collection = Dns.GetHostAddresses(settings.StanHost)
				.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork);

			IPAddress address;
			if ( collection.Count() > 0 )
			{
				address = collection.Last();
				stanAddress = new IPEndPoint(address, 23434);
			}
			else
				throw new WebException("Can not resolve Host");

		}
	}
}
