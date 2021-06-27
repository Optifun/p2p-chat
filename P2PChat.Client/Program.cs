using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using P2PChat.Client.Routes;
using P2PChat.Client.Services;
using P2PChat.Reciever;
using P2PChat.Services.PortForwarding;
using P2PChat.Services.Serializing;

namespace P2PChat.Client
{
	static class Program
	{
		public static IPEndPoint StunAddress;
		private static MessageObserver _messageObserver;
		private static UserObserver _userObserver;

		private static AuthForm _authForm;
		private static UDPObserver _authFormObserver;
		private static MainForm _mainForm;
		private static UDPObserver _mainFormObserver;
		private static int _availablePort;

		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static async Task Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			IPortForwarder forwarder = new UpnpPortForwarderService();

			var stunIp = await ResolveStunIp();
			StunAddress = new IPEndPoint(stunIp, 23434);
			_availablePort = await forwarder.TraversePort(NetProtocol.Udp, Int32.MinValue);
			
			
			_client = new Client(self.UserID, self.port, _serverIp, 700, new BinarySerializerService(), SynchronizationContext.Current);

			_setOnlinetUsers(_client.Users);

			_client.UsersUpdated += _setOnlinetUsers;
			_client.MessageReceived += _saveMessage;
			_client.Listen();
			
			SetupAuthForm();
			
			if (StunAddress != null)
				Application.Run(_authForm);
		}

		private static void ShowAuthForm(object sender, FormClosedEventArgs e)
		{
			_authForm.Show();
		}

		private static void ShowMainMenu(PublicUser userData)
		{
			SetupMainMenu(userData);
			_authForm.Hide();
			_authFormObserver.Stop();
			_mainForm.Show();
			
		}

		private static void SetupMainMenu(PublicUser user)
		{
		}

		private static void SetupAuthForm()
		{
			_authFormObserver = InitializeAuthUDPObserver(_availablePort);
			_authForm = new AuthForm(StunAddress, _authFormObserver, _availablePort);
			_authForm.Closed += AuthFormOnClosed;
		}

		private static UDPObserver InitializeAuthUDPObserver(int availablePort)
		{
			var authController = new AuthObserver();
			authController.Success += ShowMainMenu;
			authController.Error += _authForm.PrintError;
			var blackHole = new UndefinedResolver();
			var routes = authController.Compose(blackHole);
			_authFormObserver = new UDPObserver(availablePort, new BinarySerializerService(), WindowsFormsSynchronizationContext.Current, routes);
			return _authFormObserver;
		}

		private static void AuthFormOnClosed(object sender, EventArgs e)
		{
			_authFormObserver.Stop();
		}

		private static async Task<IPAddress> ResolveStunIp()
		{
			const string settingsPath = "./settings.json";
			if (!File.Exists(settingsPath))
				return IPAddress.Loopback;
			var parser = new JsonConfigParserService(settingsPath);
			parser.Parse();

			if (!string.IsNullOrEmpty(parser.Host))
			{
				var ipAddresses = await Dns.GetHostAddressesAsync(parser.Host);
				var publicIp = ipAddresses
					.First(ipAddress => !ipAddress.ToString().StartsWith("25.")
					                    && ipAddress.AddressFamily == AddressFamily.InterNetwork);
				return publicIp;
			}

			if (!string.IsNullOrEmpty(parser.Ip))
				return IPAddress.Parse(parser.Ip);
			else
				return IPAddress.Loopback;
		}
	}
}