using System;
using System.Security.Cryptography;
using System.Threading;

namespace P2PChat.Server
{
	class Program
	{
		static int authServerPort = 1442;
		static int masterPort = 3434;
		static Thread AuthServer;
		static Auth AuthModel;
		static SynchronizationContext context;
		static void Main (string[] args)
		{
			Console.WriteLine("Hello World!");
			context = SynchronizationContext.Current ?? new SynchronizationContext();

			AuthServer = new Thread(() =>
			{
				AuthModel.Start(authServerPort, context);
			});
		}
	}
}
