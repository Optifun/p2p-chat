using System.Net;

namespace P2PChat.Client.Services
{
    public class ClientInformation : IClientInformation
    {
        public ClientInformation(IPEndPoint stunServer)
        {
            StunServer = stunServer;
        }

        public PublicUser User { get; }
        public int OpenedPort { get; }
        public IPEndPoint StunServer { get; }
    }
}