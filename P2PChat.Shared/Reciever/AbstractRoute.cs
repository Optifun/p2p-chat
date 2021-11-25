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

		public TRoute Get<TRoute>() where TRoute : class, IRoute 
        {
			if (typeof(TRoute).IsAssignableFrom(this.GetType()))
				return this as TRoute;
			return _next?.Get<TRoute>();
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
