using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Sockets;
using P2PChat.Reciever;
using P2PChat.Server.Resolver;
using P2PChat.Services.Serializing;

namespace P2PChat.Server
{
	class Program
	{

		static void Main (string[] args)
		{
			PrintServerLocation();
			var context = GetSynchronizationContext();
			int _serverPort = 23434;
			
			IPacketSerializerService serializerService = new BinarySerializerService();
			IRoute routes = SetupRoutes();
			var observer = new UDPObserver(_serverPort, serializerService, context, routes);
			var server = new StunServer(observer, 1600, context);
			Task.Factory.StartNew(() =>
			{
				SynchronizationContext.SetSynchronizationContext(context);
				server.Start();
			});

			while ( Console.ReadKey().Key != ConsoleKey.Q )
				Thread.Sleep(100);
		}

		private static void PrintServerLocation()
		{
			var hostname = Dns.GetHostName();
			var ips = Dns.GetHostAddresses(hostname)
				.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
				.ToList();
			Console.WriteLine("Server started on host " + Dns.GetHostName());
			Console.WriteLine("With ip addresses");
			foreach (var ip in ips)
				Console.WriteLine(ip);
		}

		private static SynchronizationContext GetSynchronizationContext()
		{
			var context = SynchronizationContext.Current ?? new SynchronizationContext();
			SynchronizationContext.SetSynchronizationContext(context);
			return context;
		}

		private static IRoute SetupRoutes()
		{
			var blackHole = new UndefinedResolver();
			var userResolver = new UsersOnline();
			var authresolver = new Authentification();
			IRoute routes = authresolver.Compose(userResolver, blackHole);
			return routes;
		}
	}
}
