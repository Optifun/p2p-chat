using System;
using System.Collections.Generic;
using System.Linq;
using P2PChat.Client.Routes;
using P2PChat.Packets;
using P2PChat.Reciever;

namespace P2PChat.Client.Services
{
    public class MessageRepository : IMessageRepository
    {
        public event Action<Message> MessageReceived;
        public Dictionary<PublicUser, List<Message>> Chats { get; } = new Dictionary<PublicUser, List<Message>>();

        private readonly IChatRepository _chatRepository;
        private readonly IClientInformation _clientInformation;
        private readonly UDPObserver _observer;

        public MessageRepository(IChatRepository chatRepository, IClientInformation clientInformation, MessageObserver messageObserver, UDPObserver observer)
        {
            _observer = observer;
            _clientInformation = clientInformation;
            _chatRepository = chatRepository;
            messageObserver.MessageReceived += OnMessageReceived;
        }

        public Message Send(PublicUser receiver, string text)
        {
            PublicUser user = FindReceiver(receiver);
            if (user == null)
                return null;

            var message = new Message(_clientInformation.User.UserID, receiver.UserID, text);
            _observer.Send(message, user.Address);
            StoreMessage(message, receiver);

            return message;
        }

        private PublicUser FindReceiver(PublicUser receiver)
        {
            return _chatRepository.Users.Find(usr => usr.UserID == receiver.UserID);
        }

        private void OnMessageReceived(Message message)
        {
            PublicUser sender = _chatRepository.Users.First(user => user.UserID == message.Sender);
            if (sender != null)
                StoreMessage(message, sender);
        }

        private void StoreMessage(Message message, PublicUser sender)
        {
            if (Chats.ContainsKey(sender))
                Chats[sender].Add(message);
            else
                Chats[sender] = new List<Message> {message};

            MessageReceived?.Invoke(message);
        }
    }
}