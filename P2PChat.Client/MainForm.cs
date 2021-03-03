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
using P2PChat.Client.Packets;
using P2PChat.Reciever;

namespace P2PChat.Client
{
	public partial class MainForm : Form
	{
		Client _client;
		IPEndPoint _serverIP;
		PublicUser _selected;
		List<PublicUser> _users;

		public MainForm ()
		{
			InitializeComponent();
			_serverIP = new IPEndPoint(IPAddress.Loopback, 3434);
			_client = new Client(Guid.NewGuid(), _serverIP, 700, WindowsFormsSynchronizationContext.Current);

			_setUsers(_client.Users);
			_client.UsersUpdated += _setUsers;
			_client.Listen();
		}

		private void _setUsers (List<PublicUser> list)
		{
			_users = list;
			chatList.Items.AddRange(_users.ToArray());
		}

		private void button1_Click (object sender, EventArgs e)
		{
			_client.Send(_selected.UserID, textBox1.Text);
		}

		private void chatList_SelectedIndexChanged (object sender, EventArgs e)
		{
			var index = chatList.SelectedIndex;
			_selected = _users[index];
		}
	}
}
