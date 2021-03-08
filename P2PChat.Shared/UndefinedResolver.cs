using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;

namespace P2PChat
{
	public class UndefinedResolver : AbstractRoute
	{
		public override Action Handle (Packet packet)
		{
			return () => Debug.WriteLine($"Undefined packet recieved from " + packet.Sender);
		}
	}
}
