using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2PChat.Reciever;
using Open.Nat;
using System.Threading;
using P2PChat.Client.Routes;
using System.Diagnostics;
using System.Net.Sockets;
using P2PChat.Packets;
using System.Net;

namespace P2PChat.Client
{
	public partial class AuthForm : Form
	{
		IPEndPoint _stanIP;
		private UdpClient _client;
		private UDPObserver _observer;
		private AuthObserver authObserver;
		NatDiscoverer discoverer;
		NatDevice router;
		Thread authThread;
		int openPort;

		MainForm chat;

		public AuthForm (IPEndPoint stanIP)
		{
			InitializeComponent();
			_stanIP = stanIP;
		}

		private async void AuthForm_Load (object sender, EventArgs e)
		{
			openPort = await OpenPort();
			authObserver = new AuthObserver();
			authObserver.Error += printError;
			authObserver.Success += enterChat;
			_observer = new UDPObserver(openPort, WindowsFormsSynchronizationContext.Current, authObserver);
			_client = new UdpClient(AddressFamily.InterNetwork);
			authThread = new Thread(() => _observer.Start());
			authThread.IsBackground = true;
			authThread.Start();
		}

		private void enterChat (PublicUser user)
		{
			chat = new MainForm(user, _stanIP);
			chat.FormClosed += showAuthForm;
			chat.Show();
			Hide();
		}


		private void printError (string error)
		{
			errorLabel.Text = error;
		}

		private void showAuthForm (object sender, FormClosedEventArgs e)
		{
			this.Show();
		}

		private void loginButton_Click (object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
			sendAuthPacket(new AuthAction(login, password, AuthType.Login, openPort));
		}

		private void registerButton_Click (object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
			sendAuthPacket(new AuthAction(login, password, AuthType.Register, openPort));
		}


		private void sendAuthPacket (AuthAction packet)
		{
			var buffer = packet.ToBytes();
			_client.Send(buffer, buffer.Length, _stanIP);
		}

		public async Task<int> OpenPort ()
		{
			discoverer = new NatDiscoverer();
			var cts = new CancellationTokenSource(10000);
			router = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);

			//Занятые udp порты
			List<int> busyUdpPorts = new List<int>();

			//Нахождение всех занятых портов (публичных и приватных)
			var mappings = await router.GetAllMappingsAsync();
			foreach ( var mapping in mappings )
			{
				if ( mapping.Protocol == Protocol.Udp )
				{
					Debug.WriteLine(mapping);
					busyUdpPorts.Add(mapping.PrivatePort);
					busyUdpPorts.Add(mapping.PublicPort);
				}
			}

			//Нахождение свободного порта
			Random random = new Random();
			int availablePort = random.Next(30000, 65535);
			while ( busyUdpPorts.Contains(availablePort) )
			{
				availablePort = random.Next(30000, 65535);
			}

			await router.CreatePortMapAsync(new Mapping(Protocol.Udp, availablePort, availablePort, "P2P_Chat_User"));
			return availablePort;
		}

		private void AuthForm_FormClosing (object sender, FormClosingEventArgs e)
		{
			try
			{
				authThread.Abort();
				if ( chat == null || chat.IsDisposed )
					router.DeletePortMapAsync(new Mapping(Protocol.Udp, openPort, openPort));
			}
			catch
			{
			}
		}
	}
}
