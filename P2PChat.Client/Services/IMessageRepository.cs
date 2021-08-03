using System;
using System.Collections.Generic;
using P2PChat.Packets;

namespace P2PChat.Client.Services
{
    public interface IMessageRepository
    {
        public event Action<Message> MessageReceived;
        Dictionary<PublicUser, List<Message>> Chats { get; }
    }
}