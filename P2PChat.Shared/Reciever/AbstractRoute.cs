using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Reciever
{
	public class AbstractRoute : IRoute
	{
		private IRoute _next;
		public virtual Action Handle (NetworkData networkData)
		{
			return _next != null ? _next.Handle(networkData) : null;
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
