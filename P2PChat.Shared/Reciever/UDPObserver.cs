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
	public class UDPObserver
	{
		readonly UdpClient _client;
		SynchronizationContext _synchronization;
		CancellationToken _token;
		CancellationTokenSource _tokenSource;
		IRoute _routes;

		//int _port;
		//IPEndPoint _mask;


		public UDPObserver (int port, SynchronizationContext context, IRoute routeChain)
		{
			_synchronization = context;
			_routes = routeChain;
			_client = new UdpClient(port);
		}

		public UDPObserver (IPEndPoint mask, SynchronizationContext context, IRoute routeChain)
		{
			//TODO: можно заменить маску на порт на который будут приходить сообщения
			_synchronization = context;
			_routes = routeChain;
			_client = new UdpClient(mask);
		}

		public void Start ()
		{
			_synchronization.Post((_) => Console.WriteLine("UDPObserver started"), null);
			Stop();
			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
			listen();
		}

		public void Stop ()
		{
			if ( _tokenSource != null )
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
