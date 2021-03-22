using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using P2PChat.Packets;
using P2PChat.Reciever;
using P2PChat.Server.Resolver;
using P2PChat.Server.Db;

namespace P2PChat.Server
{
	class StunServer
	{
		public event Action<List<PublicUser>> UsersUpdated;
		int _serverPort = 23434;

		public Dictionary<IPEndPoint, PublicUser> Users = new Dictionary<IPEndPoint, PublicUser>();

		private Dictionary<IPEndPoint, PublicUser> _peers = new Dictionary<IPEndPoint, PublicUser>();

		private int _refreshMs;

		private UserDb userDb;

		private UdpClient _client;
		private UDPObserver observer;
		private UsersOnline userResolver;
		private Authentification authresolver;
		private SynchronizationContext _synchronization;

		public StunServer (int refreshInterval, SynchronizationContext ctx)
		{
			_synchronization = ctx;
			_refreshMs = refreshInterval;
			var blackHole = new UndefinedResolver();
			userResolver = new UsersOnline();
			authresolver = new Authentification();

			var routes = authresolver.Compose(userResolver, blackHole);
			observer = new UDPObserver(_serverPort, ctx, routes);
			_client = new UdpClient(AddressFamily.InterNetwork);
		}

		public void Start ()
		{
			_startFetching();
		}

		private async void _startFetching ()
		{
			Task.Factory.StartNew(observer.Start);
			userResolver.UsersRequested += _addRequestToQueue;
			authresolver.AuthRequested += sendAuthResponce;

			while ( true )
			{
				await Task.Factory.StartNew(() =>
				{
					_sendUserInfo();
					Thread.Sleep(_refreshMs);
				});
			}
		}

		private void sendAuthResponce (AuthAction responce, IPEndPoint sender)
		{
			if ( responce.Action == AuthType.Success )
			{
				var user = new PublicUser(sender, responce.Id.Value, responce.Nickname);
				if ( Users.ContainsKey(sender) )
					Console.WriteLine($"Multiple client access from ip " + sender);
				else
					Users.Add(sender, user);
			}

			var buffer = responce.ToBytes();
			_client.Send(buffer, buffer.Length, sender);
		}

		private void _addRequestToQueue (IPEndPoint peer)
		{
			if ( Users.Keys.Contains(peer) )
				lock ( _peers )
				{
					var user = Users[peer];
					if ( !_peers.ContainsKey(peer) )
						_peers.Add(peer, user);
				}
		}

		private void _sendUserInfo ()
		{
			if ( _peers.Count() == 0 )
				return;

			var online = _peers.Values.ToList();
			var hosts = _peers.Keys.ToList();

			lock ( _peers )
			{
				_peers.Clear();
			}

			_synchronization.Post((_) =>
			{
				Console.WriteLine("------");
				for ( int i = 0; i < online.Count(); i++ )
					Console.WriteLine($"{hosts[i]}:{online[i]}");
				Console.WriteLine("------");
			}, null);

			var packet = new OnlineUsers(online, FetchAction.Responce);
			var buffer = packet.ToBytes();
			foreach ( var host in hosts )
				_client.Send(buffer, buffer.Length, host);
		}
	}
}
