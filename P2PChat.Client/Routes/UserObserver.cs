using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;
using P2PChat.Packets;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace P2PChat.Client.Routes
{
    public class UserObserver : AbstractRoute
    {
        public event Action<List<PublicUser>> UsersReceived;

        public override Action Handle(IPEndPoint sender, IPacket obj)
        {
            if (obj is OnlineUsers users && users.Users != null && users.Action != FetchAction.Null)
                return () => UsersReceived?.Invoke(users.Users);
            return base.Handle(sender, obj);
        }
    }
}