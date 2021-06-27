using System;
using Open.Nat;

namespace P2PChat
{
	public enum NetProtocol
	{
		Tcp = 0,
		Udp
	}

	public static class NetExtenson
	{
		public static Protocol ToPackageProtocol(this NetProtocol protocol)
		{
			switch (protocol)
			{
				case NetProtocol.Tcp:
					return Protocol.Tcp;
				case NetProtocol.Udp:
					return Protocol.Udp;
				default:
					throw new ArgumentException("Specified unknown protocol");
			}
		}
	}
}