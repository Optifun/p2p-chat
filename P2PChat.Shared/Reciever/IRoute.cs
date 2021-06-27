using System;
using System.Net;
using P2PChat.Packets;

namespace P2PChat.Reciever
{
	public interface IRoute
	{
		Action Handle (IPEndPoint sender, IPacket obj);

		IRoute SetNext (IRoute route);
	}
}
