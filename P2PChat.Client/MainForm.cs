using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using P2PChat.Client.Services;
using P2PChat.Services.Serializing;
using Message = P2PChat.Client.DB.Message;

namespace P2PChat.Client
{
    public partial class MainForm : Form
    {
        private PublicUser _selectedChat;
        private PublicUser _selectedUser;
        private List<PublicUser> ChatUsers => _chats.Keys.ToList();
        private List<PublicUser> _onlineUsers;
        private List<PublicUser> _hidingUsers;
        private Dictionary<PublicUser, List<Message>> _chats;
        private IClientInformation _clientInformation;
        private IMessageRepository _messageRepository;
        private IChatRepository _chatRepository;

        public MainForm(IClientInformation clientInformation, IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _clientInformation = clientInformation;
            InitializeComponent();

            userNameLabel.Text = _clientInformation.User.Nickname;
            _hidingUsers = new List<PublicUser> {_clientInformation.User};
            _chats = new Dictionary<PublicUser, List<Message>>();

            // Debug.WriteLine("Opening Client on ip" + self.Address + " on port" + self.port);
            //TODO: убрать ссылки на Client
            // Debug.WriteLine("Setting server ip" + _serverIp);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _showAuthForm();
        }

        private void _saveMessage(Packets.Message msg)
        {
            Guid senderId = msg.Reciever == _self.UserID ? msg.Sender : msg.Reciever;
            var message = new Message(msg.Id, msg.Sender, senderId == _self.UserID, msg.Text);

            PublicUser sender = _onlineUsers.First(usr => usr.UserID == senderId);
            if (_chats.ContainsKey(sender))
            {
                _chats[sender].Add(message);
            }
            else
            {
                _chats.Add(sender, new List<Message> {message});
                chatList.Items.Add(sender);
                chatList.Focus();
            }

            //TODO: add to DB
            _drawMessage(message);
        }

        private void _showAuthForm()
        {
            _clientInformation.User = null;
            Program.AuthForm.Show();
        }

        private void _drawMessage(Message msg)
        {
            if (msg.Self)
            {
                _appendBubble(_self.Nickname, msg, msg.Self);
            }
            else
            {
                PublicUser sender = _onlineUsers.Find(usr => usr.UserID == msg.ChatId);
                _appendBubble(sender.Nickname, msg, msg.Self);
            }
        }

        private void _appendBubble(string nick, Message msg, bool self)
        {
            var bubble = new MessageBubble(nick, msg.Text, self);
            messageLayout.Controls.Add(bubble);
            bubble.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

        private void _setOnlinetUsers(List<PublicUser> list)
        {
            _onlineUsers = list.Except(_hidingUsers).ToList();
            if (_onlineUsers.Any())
                onlineList.Items.AddRange(_onlineUsers.ToArray());
        }

        private void _setActiveAchats(List<PublicUser> list)
        {
            chatList.Items.AddRange(list.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_selectedChat == null && _selectedUser == null)
                return;

            PublicUser user = _selectedChat ?? _selectedUser;
            Message message = _sendMessage(user, textBox1.Text);
            _drawMessage(message);
            textBox1.Text = "";
        }

        private Message _sendMessage(PublicUser user, string text)
        {
            if (!_onlineUsers.Contains(user))
                return null;

            var packet = _client.Send(user.UserID, textBox1.Text);

            var message = new Message(packet.Id, user.UserID, true, text);
            //TODO: save to DB
            if (!ChatUsers.Contains(user))
            {
                _chats.Add(user, new List<Message> {message});
                chatList.Items.Add(user);
            }

            return message;
        }

        private void DrawChat(PublicUser user)
        {
            List<Message> chat;
            if (_chats.TryGetValue(user, out chat))
            {
                messageLayout.Controls.Clear();
                foreach (Message message in chat)
                    _drawMessage(message);
            }
        }


        private void chatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = (PublicUser) chatList.SelectedItem;
            if (user != null)
            {
                if (!Equals(user, _selectedChat ?? _selectedUser))
                    DrawChat(user);
                _selectedChat = user;
                _selectedUser = null;
                Text = _selectedChat.Nickname + @" | P2P Chat";
            }
        }

        private void onlineList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = (PublicUser) onlineList.SelectedItem;
            if (user != null)
            {
                if (!Equals(user, _selectedChat ?? _selectedUser))
                    DrawChat(user);
                _selectedUser = user;
                _selectedChat = null;
            }
        }
    }
}