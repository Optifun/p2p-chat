﻿using System;
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
namespace P2PChat.Server
{
	class StanServer
	{
		public event Action<List<PublicUser>> UsersUpdated;
		int _clientPort = 7676;
		int _serverPort = 3434;

		public Dictionary<IPAddress, PublicUser> Users = new Dictionary<IPAddress, PublicUser>();

		private Dictionary<IPAddress, PublicUser> _peers = new Dictionary<IPAddress, PublicUser>();

		private int _refreshMs;

		private UdpClient _client;

		private UDPObserver observer;
		private UsersOnline userResolver;
		private SynchronizationContext _synchronization;

		public StanServer (int refreshInterval, SynchronizationContext ctx)
		{
			_synchronization = ctx;
			_refreshMs = refreshInterval;
			userResolver = new UsersOnline();
			observer = new UDPObserver(_serverPort, ctx, userResolver);
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
			while ( true )
			{
				await Task.Factory.StartNew(() =>
				{
					_sendUserInfo();
					Thread.Sleep(_refreshMs);
				});
			}
		}

		private void _addRequestToQueue (IPEndPoint peer)
		{
			//var user = Users[peer.Address];
			//if ( user is not null )
			lock ( _peers )
			{
				if ( !_peers.ContainsKey(peer.Address) )
					_peers.Add(peer.Address, new PublicUser());
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

			_synchronization.Post((_) =>
			{
				foreach ( var user in Users )
					Console.WriteLine($"{user.Key}:{user.Value}");
			}, null);

			var packet = new OnlineUsers(online, FetchAction.Responce);
			var buffer = packet.ToBytes();
			foreach ( var host in hosts )
				_client.Send(buffer, buffer.Length, new IPEndPoint(host, _clientPort));

		}
	}
}
