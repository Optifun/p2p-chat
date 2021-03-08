using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using P2PChat.Reciever;
using P2PChat.Client.Routes;
using P2PChat.Packets;
using System.Diagnostics;

namespace P2PChat.Client
{
	public class Client
	{
		public event Action<List<PublicUser>> UsersUpdated;
		public event Action<Message> MessageRecieved;
		int _clientPort;

		public List<PublicUser> Users = new List<PublicUser>();

		private UdpClient _client;

		private IPEndPoint _stanIP;
		private int _refreshMs;

		private Guid _selfId;

		private UDPObserver observer;
		private MessageObserver messageObserver;
		private UserObserver userObserver;


		public Client (Guid userId, int clientPort, IPEndPoint stanServerIP, int refreshInterval, SynchronizationContext ctx)
		{
			_selfId = userId;
			_stanIP = stanServerIP;
			_refreshMs = refreshInterval;
			_clientPort = clientPort;
			userObserver = new UserObserver();
			messageObserver = new MessageObserver(_selfId);
			var blackHole = new UndefinedResolver();
			var routes = userObserver.Compose(messageObserver, blackHole);
			observer = new UDPObserver(_clientPort, ctx, routes);
		}


		public async void Listen ()
		{
			_client = new UdpClient(AddressFamily.InterNetwork);
			_startFetching();
		}

		public Message Send (Guid userId, string text)
		{
			Message msg = new Message(_selfId, userId, text);
			var receiver = Users.Find(user => user.UserID == userId);
			if ( receiver == null )
				return null;

			// если клиент будет принимать пакеты от пиров и сервера
			// рандомно в разных местах, то надо будет делать роутер
			var buffer = msg.ToBytes();
			_client.Send(buffer, buffer.Length, receiver.Address);
			return msg;
		}

		private async void _startFetching ()
		{
			Task.Factory.StartNew(observer.Start);
			messageObserver.MessageRecieved += _messageRecieved;
			userObserver.UsersRecieved += _usersUpdated;
			while ( true )
			{
				await Task.Factory.StartNew(() =>
				{
					_fetchUsers();
					Thread.Sleep(_refreshMs);
				});
			}
		}

		private void _messageRecieved (Message msg)
		{
			MessageRecieved?.Invoke(msg);
		}

		private void _usersUpdated (List<PublicUser> list)
		{
			var newUsers = list.Except(Users).ToList();
			if ( newUsers.Count == 0 )
				return;
			Users.AddRange(newUsers);
			UsersUpdated?.Invoke(newUsers);
		}

		private void _fetchUsers ()
		{
			var packet = new OnlineUsers(new List<PublicUser>(), FetchAction.Fetch, _clientPort);
			var buffer = packet.ToBytes();
			_client.Send(buffer, buffer.Length, _stanIP);
		}
	}
}
