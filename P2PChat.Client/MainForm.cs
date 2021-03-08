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

namespace P2PChat.Client
{
	public partial class MainForm : Form
	{
		Client _client;
		IPEndPoint _serverIP;
		PublicUser self;
		PublicUser _selected;
		List<PublicUser> _users;

		public MainForm (PublicUser self, IPEndPoint stanIP)
		{
			InitializeComponent();
			this.self = self;
			userNameLabel.Text = self.Nickname;

			_serverIP = stanIP;
			_client = new Client(Guid.NewGuid(), self.port, _serverIP, 700, WindowsFormsSynchronizationContext.Current);

			_setUsers(_client.Users);
			_selected = chatList.Items[0] as PublicUser;

			_client.UsersUpdated += _setUsers;
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

		private void _setUsers (List<PublicUser> list)
		{
			_users = list;
			chatList.Items.AddRange(_users.ToArray());
		}

		private void button1_Click (object sender, EventArgs e)
		{
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
