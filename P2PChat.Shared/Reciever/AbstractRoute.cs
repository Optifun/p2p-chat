using System;
using System.Net;
using P2PChat.Packets;

namespace P2PChat.Reciever
{
	public class AbstractRoute : IRoute
	{
		private IRoute _next;
		public virtual Action Handle (IPEndPoint sender, IPacket obj)
		{
			return _next?.Handle(sender, obj);
		}

		public virtual IRoute SetNext (IRoute route)
		{
			_next = route;
			return route;
		}

		public IRoute Compose(params IRoute[] routes)
		{
			IRoute temp = _next ?? this;
			foreach ( var route in routes )
				temp = temp.SetNext(route);
			return this;
		}
	}
}
