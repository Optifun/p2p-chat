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
			try
			{
				var address = resolveStanIP();
				stanAddress = new IPEndPoint(address, 23434);
			}
			catch ( JsonSerializationException ex )
			{
				Debug.Fail("settings.json has wrong format", ex.Message);
			}
			catch ( SocketException ex )
			{
				Debug.Fail("Cannot connect to the server, please check your internet connection", ex.Message);
			}
			catch ( FormatException ex )
			{
				Debug.Fail("IP in settings.json has wrong format", ex.Message);
			}

			if ( stanAddress != null )
				Application.Run(new AuthForm(stanAddress));
		}

		private static IPAddress resolveStanIP ()
		{
			const string settingsPath = "./settings.json";
			if ( !File.Exists(settingsPath) )
				using ( var writer = File.CreateText(settingsPath) )
					writer.Write("{ \"ip\": \"127.0.0.1\", \"hostname\": \"\"}");

			using ( var file = new StreamReader(settingsPath) )
			{
				string text = file.ReadToEnd();
				if ( text == "" )
					return IPAddress.Loopback;

				dynamic data = JsonConvert.DeserializeObject(text);
				string host = data.hostname;
				string ip = data.ip;
				if ( host != null && host != "" )
				{
					var collection = Dns.GetHostAddresses(host)
						.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork
									&& !ip.ToString().StartsWith("25."));
					return collection.Last();
				}
				if ( ip != null && ip != "" )
					return IPAddress.Parse(ip);
			}
			return IPAddress.Loopback;
		}

	}
}