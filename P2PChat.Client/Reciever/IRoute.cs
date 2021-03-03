using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Reciever
{
	interface IRoute
	{
		Action Handle (Packet packet);

		IRoute SetNext (IRoute route);
	}
}
