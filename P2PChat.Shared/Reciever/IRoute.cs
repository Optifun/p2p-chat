using System;
using System.Net;
using P2PChat.Packets;

namespace P2PChat.Reciever
{
	public interface IRoute
	{
        TRoute Get<TRoute>() where TRoute : class, IRoute;
        Action Handle (IPEndPoint sender, IPacket obj);

		IRoute SetNext (IRoute route);
	}
}
