using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;

namespace P2PChat.Client.Routes
{
	class MessageObserver : AbstractRoute
	{
		public override Action Handle (Packet packet)
		{
			throw new NotImplementedException();
		}

	}
}
