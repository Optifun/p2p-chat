using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2PChat.Client.Properties;
using System.Net.Sockets;
using System.Diagnostics;

namespace P2PChat.Client
{
	public partial class MainForm : Form
	{
		Client _client;
		IPEndPoint _serverIP;
		PublicUser self;
		PublicUser _selected;
		List<PublicUser> _users;
		List<PublicUser> hidingUsers;
		public MainForm (PublicUser self, IPEndPoint stanIP)
		{
			InitializeComponent();
			this.self = self;
			_serverIP = stanIP;

			userNameLabel.Text = self.Nickname;
			hidingUsers = new List<PublicUser> { self };

			Debug.WriteLine("Opening Client on ip" + self.Address + " on port" + self.port);
			Debug.WriteLine("Setting server ip" + _serverIP);
			_client = new Client(self.UserID, self.port, _serverIP, 700, WindowsFormsSynchronizationContext.Current);

			_seOnlinetUsers(_client.Users);

			_client.UsersUpdated += _seOnlinetUsers;
			_client.MessageRecieved += _drawMessage;
			_client.Listen();
		}

		private void _drawMessage (Packets.Message msg)
		{
			var sender = _users.Find(usr => usr.UserID == msg.Sender);
			_appendBubble(self.Nickname, msg, false);
		}

		private void _appendBubble (string nick, Packets.Message msg, bool self)
		{
			var bubble = new MessageBubble(nick, msg.Text, self);
			messageLayout.Controls.Add(bubble);
			bubble.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			//bubble.Dock = DockStyle.Bottom;
			//bubble.ResizeControl(messageLayout, null);
			//messageLayout.SizeChanged += bubble.ResizeControl;
		}

		private void _seOnlinetUsers (List<PublicUser> list)
		{
			_users = list.Except(hidingUsers).ToList();
			onlineList.Items.AddRange(_users.ToArray());
		}

		private void button1_Click (object sender, EventArgs e)
		{
			if ( _selected == null )
				return;

			var message = _client.Send(_selected.UserID, textBox1.Text);
			_appendBubble(self.Nickname, message, true);
		}

		private void chatList_SelectedIndexChanged (object sender, EventArgs e)
		{
			var index = chatList.SelectedIndex;
			if ( index >= 0 )
				_selected = _users[index];
		}
	}
}
