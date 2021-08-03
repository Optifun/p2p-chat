using System.Net;

namespace P2PChat.Client.Services
{
    public interface IClientInformation
    {
        PublicUser User { get; set; }
        int OpenedPort { get; set; }
        IPEndPoint StunServer { get; }
    }
}