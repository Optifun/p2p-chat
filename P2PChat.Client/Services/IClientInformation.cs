using System.Net;

namespace P2PChat.Client.Services
{
    public interface IClientInformation
    {
        PublicUser User { get; }
        int OpenedPort { get; }
        IPEndPoint StunServer { get; }
    }
}