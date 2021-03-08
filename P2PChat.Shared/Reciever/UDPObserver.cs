using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace P2PChat.Reciever
{
	public class UDPObserver
	{
		UdpClient _client;
		SynchronizationContext _synchronization;
		CancellationToken _token;
		CancellationTokenSource _tokenSource;
		Task observeTask;
		IRoute _routes;
		int _port;
		//IPEndPoint _mask;
		bool serverStopped = true;

		public UDPObserver (int port, SynchronizationContext context, IRoute routeChain)
		{
			_synchronization = context;
			_routes = routeChain;
			_port = port;
		}

		public void Start ()
		{
			serverStopped = false;
			_synchronization.Post((_) => Console.WriteLine("UDPObserver started on port " + _port), null);

			_client = new UdpClient(_port);
			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
			observeTask = Task.Factory.StartNew(listen, _token);
		}

		public async void Stop ()
		{
			serverStopped = true;
			Debug.WriteLine("Stopping UDPObserver on port " + _port);
			if ( _tokenSource != null )
				_tokenSource.Cancel();
			if ( _client != null )
				_client.Close();
		}

		private void listen ()
		{
			IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
			byte[] data;
			while ( !_token.IsCancellationRequested )
			{
				try
				{
					data = _client.Receive(ref sender);
					var packet = new Packet(data, sender);
					Task.Factory.StartNew(() => _synchronization.Post((_) => _routes.Handle(packet)(), null), _token);
				}
				catch ( SocketException ex )
				{
					if ( !serverStopped )
						Debug.Fail(Process.GetCurrentProcess().ProcessName + "\n\r" + ex.ToString());
					return;
				}
			}
		}

	}
}
