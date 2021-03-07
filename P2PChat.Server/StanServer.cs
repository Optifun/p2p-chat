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
	class StanServer
	{
		public event Action<List<PublicUser>> UsersUpdated;
		int _clientPort = 7676;
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

		public StanServer (int refreshInterval, SynchronizationContext ctx)
		{
			_synchronization = ctx;
			_refreshMs = refreshInterval;
			userDb = new UserDb();
			userResolver = new UsersOnline();
			authresolver = new Authentification(userDb);

			var routes = authresolver.Compose(userResolver);
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
				var entry = userDb.SearchByNickname(responce.Nickname);
				var user = new PublicUser(sender, entry.UserID, entry.Nickname);
				Users.Add(sender.Address, user);
			}

			var buffer = responce.ToBytes();
			_client.Send(buffer, buffer.Length, sender);
		}

		private void _addRequestToQueue (IPEndPoint peer)
		{
			// TODO: переписать с авторизацией
			//var user = Users[peer.Address];
			//if ( user is not null )
			lock ( _peers )
			{
				if ( !_peers.ContainsKey(peer) )
					_peers.Add(peer, new PublicUser());
			}
		}

		private void _sendUserInfo ()
		{
			var online = _peers.Values.ToList();
			var hosts = _peers.Keys.ToList();
			Users = _peers.Select(p => p).ToDictionary(p => p.Key, p => p.Value);

			lock ( _peers )
			{
				_peers.Clear();
			}

			if ( Users.Count() > 0 )
				_synchronization.Post((_) =>
				{
					Console.WriteLine("------");
					foreach ( var user in Users )
						Console.WriteLine($"{user.Key}:{user.Value}");
					Console.WriteLine("------");
				}, null);

			var packet = new OnlineUsers(online, FetchAction.Responce);
			var buffer = packet.ToBytes();
			foreach ( var host in hosts )
				_client.Send(buffer, buffer.Length, host);

		}
	}
}
