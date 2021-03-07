using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using P2PChat.Auth;
using P2PChat.Server.Db;
using System.Linq;
using System.Threading.Tasks;

namespace P2PChat.Server
{
	public class Auth
	{
		UdpClient _client;
		SynchronizationContext _synchronization;
		CancellationToken _token;
		CancellationTokenSource _tokenSource;
		int _port;
		UserDb Db;

		public Auth (UserDb db)
		{
			Db = db;
		}

		public void Start (int authServerPort, SynchronizationContext context)
		{
			_synchronization = context;
			_port = authServerPort;

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
				resolvePacket(data, sender);

			}
		}

		async Task resolvePacket (byte[] data, IPEndPoint sender)
		{
			Packet responce = null;
			Packet packet = null;

			try
			{
				packet = Packet.Parse(data);
				responce = packet.Action switch
				{
					AuthAction.Register => await handleRegistration(packet),
					AuthAction.Login => await handleLogin(packet),
					_ => await handleUndefined()
				};
			}
			catch ( Exception ex )
			{
				_synchronization.Post(_logToConsole, sender.ToString() + " >>> " + ex.Message);
				responce = await handleUndefined();
			}

			byte[] bytes = Packet.ToBytes(responce).ToArray();
			_client.Send(bytes, bytes.Length, sender);
			_synchronization.Post(_logToConsole, sender.ToString() + " >>> " + responce.ToString());
		}

		private async Task<Packet> handleUndefined ()
		{
			return new Packet() { Action = AuthAction.Undefined };
		}

		private async Task<Packet> handleRegistration (Packet packet)
		{
			var entry = Db.SearchByNickname(packet.Nickname);
			Packet responce;
			if ( entry != null )
			{
				responce = new WrongCredentials(packet.Nickname, packet.Password);
			}
			else
			{
				User usr = new User()
				{
					UserID = Guid.NewGuid(),
					Nickname = packet.Nickname,
					PasswordHash = packet.Password
				};

				usr = Db.Users.Add(usr);
				responce = new Success(
					usr.UserID,
					usr.Nickname,
					usr.PasswordHash);
			}
			return responce;
		}

		private async Task<Packet> handleLogin (Packet packet)
		{
			var entry = Db.SearchByNickname(packet.Nickname);
			Packet responce;
			if ( entry != null )
			{
				if ( entry.PasswordHash == packet.Password && entry.Nickname == packet.Nickname )
					responce = new Success(entry.UserID, entry.Nickname, entry.PasswordHash);
				else
					responce = new WrongCredentials(packet.Nickname, packet.Password);
			}
			else
			{
				responce = new WrongCredentials(packet.Nickname, packet.Password);
			}
			return responce;
		}

		void _logToConsole (object message) => Console.WriteLine(message);

	}
}
