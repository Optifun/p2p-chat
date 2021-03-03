using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace P2PChat.Reciever
{
	class UDPObserver
	{
		readonly UdpClient _client;
		SynchronizationContext _synchronization;
		CancellationToken _token;
		CancellationTokenSource _tokenSource;
		IRoute _routes;

		int _port;
		IPEndPoint _mask;

		public UDPObserver (IPEndPoint mask, SynchronizationContext context, IRoute routeChain)
		{
			_mask = mask;
			_synchronization = context;
			_routes = routeChain;
			_client = new UdpClient(mask);
		}

		public void Start ()
		{
			Stop();
			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
			listen();
		}

		public void Stop ()
		{
			if ( _tokenSource is not null )
				_tokenSource.Cancel();
		}

		private void listen ()
		{
			IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
			byte[] data;
			while ( !_token.IsCancellationRequested )
			{
				data = _client.Receive(ref sender);
				var packet = new Packet(data, sender);
				Task.Factory.StartNew(() => _synchronization.Post((_) => _routes.Handle(packet)(), null), _token);
			}
		}

	}
}
