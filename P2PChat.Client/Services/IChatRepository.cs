using System;
using System.Collections.Generic;

namespace P2PChat.Client.Services
{
    public interface IChatRepository
    {
        event Action<List<PublicUser>> UsersUpdated;
        public List<PublicUser> Users { get; }
        void ObserveUsers(int refreshTime);
        void StopObservation();
    }
}