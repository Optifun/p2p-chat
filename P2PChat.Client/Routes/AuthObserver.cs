using System;
using System.Net;
using P2PChat.Reciever;
using P2PChat.Packets;

namespace P2PChat.Client.Routes
{
    internal class AuthObserver : AbstractRoute
    {
        public event Action<PublicUser> Success;
        public event Action<string> Error;

        public override Action Handle(IPEndPoint sender, IPacket obj)
        {
            var authPacket = obj as AuthAction;
            if (authPacket?.Nickname == null)
                return base.Handle(sender, obj);

            if (authPacket.OpenPort == null)
                return () => { new Exception("Server doesn't provided open port"); };

            var ip = new IPEndPoint(IPAddress.Loopback, authPacket.OpenPort.Value);

            switch (authPacket.Action)
            {
                case AuthType.UserExisting:
                    return () => { Error?.Invoke("Login is already reserved"); };
                case AuthType.WrongCredentials:
                    return () => { Error?.Invoke("Wrong password"); };
                case AuthType.Success:
                {
                    if (authPacket.Id == null)
                        return () => { new Exception("Server could not generate UUID"); };
                    Guid id = authPacket.Id.Value;
                    return () => { Success?.Invoke(new PublicUser(ip, id, authPacket.Nickname)); };
                }
                default:
                    return () => { };
            }
        }
    }
}