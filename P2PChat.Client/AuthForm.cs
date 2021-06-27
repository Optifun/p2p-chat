using System;
using System.Windows.Forms;
using P2PChat.Reciever;
using P2PChat.Packets;
using System.Net;

namespace P2PChat.Client
{
	public partial class AuthForm : Form
	{
		private UDPObserver _observer;
		private IPEndPoint _stunIp;
		private int _openPort;

		public AuthForm(IPEndPoint stunIp, UDPObserver observer, int openPort)
		{
			InitializeComponent();
			_stunIp = stunIp;
			_observer = observer;
			this._openPort = openPort;
		}

		private void AuthForm_Load(object sender, EventArgs e)
		{
			_observer.Start();
		}

		public void PrintError(string error)
		{
			errorLabel.Text = error;
		}

		private void SendAuthPacket(AuthAction packet)
		{
			_observer.Send(packet, _stunIp);
		}

		private void loginButton_Click(object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
			SendAuthPacket(new AuthAction(login, password, AuthType.Login, _openPort));
		}

		private void registerButton_Click(object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
			SendAuthPacket(new AuthAction(login, password, AuthType.Register, _openPort));
		}
	}
}