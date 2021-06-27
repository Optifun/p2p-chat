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
		private UDPObserver _observer;
		private IPEndPoint _stanIP;
		private int _openPort;
		// delete binding
		private AuthObserver authObserver;
		MainForm chat;

		public AuthForm (IPEndPoint stanIP, UDPObserver observer, int openPort)
		{
			InitializeComponent();
			_stanIP = stanIP;
			_observer = observer;
			this._openPort = openPort;
		}

		private async void AuthForm_Load (object sender, EventArgs e)
		{
			authObserver.Error += PrintError;
			authObserver.Success += EnterChat;
			_observer.Start();
		}

		private void EnterChat (PublicUser user)
		{
			_observer.Stop();
			chat = new MainForm(user, _stanIP);
			chat.FormClosed += ShowAuthForm;
			chat.Show();
			Hide();
		}


		private void PrintError (string error)
		{
			errorLabel.Text = error;
		}

		private void ShowAuthForm (object sender, FormClosedEventArgs e)
		{
			this.Show();
		}

		private void loginButton_Click (object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
			SendAuthPacket(new AuthAction(login, password, AuthType.Login, _openPort));
		}

		private void registerButton_Click (object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
			SendAuthPacket(new AuthAction(login, password, AuthType.Register, _openPort));
		}


		private void SendAuthPacket (AuthAction packet)
		{
			_observer.Send(packet, _stanIP);
		}


		private void AuthForm_FormClosing (object sender, FormClosingEventArgs e)
		{
			try
			{
				_observer.Stop();
			}
			catch
			{
			}
		}
	}
}
