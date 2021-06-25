using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2PChat.Client.Properties;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using P2PChat.Client.Services;

namespace P2PChat.Client
{
	static class Program
	{
		public static IPEndPoint stanAddress;

		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var address = resolveStanIP();
			stanAddress = new IPEndPoint(address, 23434);


			if (stanAddress != null)
				Application.Run(new AuthForm(stanAddress));
		}

		private static IPAddress resolveStanIP()
		{
			const string settingsPath = "./settings.json";
			if (File.Exists(settingsPath))
			{
				var parser = new JsonConfigParserService(settingsPath);
				parser.Parse();

				if (!string.IsNullOrEmpty(parser.Host))
				{
					var collection = Dns.GetHostAddresses(parser.Host)
						.Where(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork
						                    && !ipAddress.ToString().StartsWith("25."));
					return collection.First();
				}

				if (!string.IsNullOrEmpty(parser.Ip))
					return IPAddress.Parse(parser.Ip);
			}

			return IPAddress.Loopback;
		}
	}
}