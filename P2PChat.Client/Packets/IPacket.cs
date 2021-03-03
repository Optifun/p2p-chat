using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Client.Packets
{
	interface IPacket
	{
		byte[] ToBytes ();
	}
}
