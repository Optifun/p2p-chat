using System.Net;

namespace P2PChat.Client.Services
{
    public class ClientInformation : IClientInformation
    {
        public ClientInformation(IPEndPoint stunServer)
        {
            StunServer = stunServer;
        }

        public PublicUser User { get; set; }
        public int OpenedPort { get; set; }
        public IPEndPoint StunServer { get; }
    }
}