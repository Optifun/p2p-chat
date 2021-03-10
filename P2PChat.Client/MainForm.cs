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
using P2PChat.Client.DB;

namespace P2PChat.Client
{
	public partial class MainForm : Form
	{
		Client _client;
		IPEndPoint _serverIP;
		PublicUser self;
		PublicUser _selectedChat;
		PublicUser _selectedUser;
		List<PublicUser> _chatUsers => _chats.Keys.ToList();
		List<PublicUser> _onlineUsers;
		List<PublicUser> hidingUsers;
		Dictionary<PublicUser, List<DB.Message>> _chats;

		public MainForm (PublicUser self, IPEndPoint stanIP)
		{
			InitializeComponent();
			this.self = self;
			_serverIP = stanIP;

			userNameLabel.Text = self.Nickname;
			hidingUsers = new List<PublicUser> { self };
			_chats = new Dictionary<PublicUser, List<DB.Message>>();

			Debug.WriteLine("Opening Client on ip" + self.Address + " on port" + self.port);
			Debug.WriteLine("Setting server ip" + _serverIP);
			_client = new Client(self.UserID, self.port, _serverIP, 700, WindowsFormsSynchronizationContext.Current);

			_setOnlinetUsers(_client.Users);

			_client.UsersUpdated += _setOnlinetUsers;
			_client.MessageRecieved += _saveMessage;
			_client.Listen();
		}

		private void _saveMessage (Packets.Message msg)
		{
			var senderId = msg.Reciever == self.UserID ? msg.Sender : msg.Reciever;
			var message = new DB.Message(msg.Id, msg.Sender, senderId == self.UserID, msg.Text);

			var sender = _onlineUsers.First(usr => usr.UserID == senderId);
			if ( _chats.ContainsKey(sender) )
				_chats[sender].Add(message);
			else
			{
				_chats.Add(sender, new List<DB.Message> { message });
				chatList.Items.Add(sender);
				chatList.Focus();
			}
			//TODO: add to DB
			_drawMessage(message);
		}

		private void _drawMessage (DB.Message msg)
		{
			if ( msg.Self )
				_appendBubble(self.Nickname, msg, msg.Self);
			else
			{
				var sender = _onlineUsers.Find(usr => usr.UserID == msg.ChatId);
				_appendBubble(sender.Nickname, msg, msg.Self);
			}
		}

		private void _appendBubble (string nick, DB.Message msg, bool self)
		{
			var bubble = new MessageBubble(nick, msg.Text, self);
			messageLayout.Controls.Add(bubble);
			bubble.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
		}

		private void _setOnlinetUsers (List<PublicUser> list)
		{
			_onlineUsers = list.Except(hidingUsers).ToList();
			onlineList.Items.AddRange(_onlineUsers.ToArray());
		}

		private void _setActiveAchats (List<PublicUser> list)
		{
			chatList.Items.AddRange(list.ToArray());
		}


		private void button1_Click (object sender, EventArgs e)
		{
			if ( _selectedChat == null && _selectedUser == null )
				return;

			var user = _selectedChat ?? _selectedUser;
			var message = _sendMessage(user, textBox1.Text);
			_drawMessage(message);
			textBox1.Text = "";
		}

		private DB.Message _sendMessage (PublicUser user, string text)
		{
			if ( !_onlineUsers.Contains(user) )
				return null;

			var packet = _client.Send(user.UserID, textBox1.Text);

			var message = new DB.Message(packet.Id, user.UserID, true, text);
			//TODO: save to DB
			if ( !_chatUsers.Contains(user) )
			{
				_chats.Add(user, new List<DB.Message> { message });
				chatList.Items.Add(user);
			}
			return message;
		}

		private void drawChat (PublicUser user)
		{
			List<DB.Message> chat;
			if ( _chats.TryGetValue(user, out chat) )
			{
				messageLayout.Controls.Clear();
				foreach ( var message in chat )
					_drawMessage(message);
			}
		}


		private void chatList_SelectedIndexChanged (object sender, EventArgs e)
		{
			var user = (PublicUser)chatList.SelectedItem;
			if ( user != null )
			{
				if ( user != ( _selectedChat ?? _selectedUser ) )
					drawChat(user);
				_selectedChat = user;
				_selectedUser = null;
				this.Text = _selectedChat.Nickname + " | P2P Chat";
			}
		}

		private void onlineList_SelectedIndexChanged (object sender, EventArgs e)
		{
			var user = (PublicUser)onlineList.SelectedItem;
			if ( user != null )
			{
				if ( user != ( _selectedChat ?? _selectedUser ) )
					drawChat(user);
				_selectedUser = user;
				_selectedChat = null;
			}
		}
	}
}
