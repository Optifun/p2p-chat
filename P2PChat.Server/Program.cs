using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Sockets;
using System.Configuration;

namespace P2PChat.Server
{
	class Program
	{
		static int authServerPort = 1442;
		static Thread AuthServer;
		static SynchronizationContext context;
		static StanServer server;

		static void Main (string[] args)
		{
			var hostname = Dns.GetHostName();
			var ips = Dns.GetHostAddresses(hostname)
					.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
					.ToList();
			Console.WriteLine("Server started on host " + Dns.GetHostName());
			Console.WriteLine("With ip addresses");
			foreach ( var ip in ips )
				Console.WriteLine(ip);

			context = SynchronizationContext.Current ?? new SynchronizationContext();
			SynchronizationContext.SetSynchronizationContext(context);

			server = new StanServer(1600, context);
			Task.Factory.StartNew(() =>
			{
				SynchronizationContext.SetSynchronizationContext(context);
				server.Start();
			});

			while ( Console.ReadKey().Key != ConsoleKey.Q )
				Thread.Sleep(100);
		}
	}
}
