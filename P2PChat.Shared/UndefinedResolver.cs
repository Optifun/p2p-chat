using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Packets;
using P2PChat.Reciever;

namespace P2PChat
{
	public class UndefinedResolver : AbstractRoute
	{
		public override Action Handle(IPEndPoint sender, IPacket obj)
		{
			return () => Debug.WriteLine($"Undefined packet received from " + sender);
		}
	}
}
