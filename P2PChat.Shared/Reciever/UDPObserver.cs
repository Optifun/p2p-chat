using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using P2PChat.Packets;
using P2PChat.Services.Serializing;

namespace P2PChat.Reciever
{
	public class UDPObserver
	{
		private UdpClient _client;
		private CancellationToken _token;
		private CancellationTokenSource _tokenSource;
		private readonly SynchronizationContext _synchronization;

		private readonly IPacketSerializerService _serializer;
		private readonly IRoute _routes;
		private readonly int _port;
		private bool _serverStopped = true;

		public UDPObserver(int port, IPacketSerializerService serializer, SynchronizationContext context, IRoute routeChain)
		{
			_serializer = serializer;
			_synchronization = context;
			_routes = routeChain;
			_port = port;
		}

		public void Start()
		{
			_serverStopped = false;
			_synchronization.Post((_) => Console.WriteLine("UDPObserver started on port " + _port), null);

			_client = new UdpClient(_port);
			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
			Task.Factory.StartNew(Listen, _token);
		}

		public async void Stop()
		{
			_serverStopped = true;
			Debug.WriteLine("Stopping UDPObserver on port " + _port);
			_tokenSource?.Cancel();
			_client?.Close();
		}


		private void Listen()
		{
			IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
			while (!_token.IsCancellationRequested)
			{
				try
				{
					byte[] data = _client.Receive(ref sender);
					var packet = _serializer.Deserialize<IPacket>(data);
					IPEndPoint address = sender;
					Task.Factory.StartNew(() => _synchronization.Post((_) => _routes.Handle(address, packet)(), null), _token);
				}
				catch (SocketException ex)
				{
					if (!_serverStopped)
						Debug.Fail(Process.GetCurrentProcess().ProcessName + "\n\r" + ex.ToString());
					return;
				}
			}
		}
	}
}