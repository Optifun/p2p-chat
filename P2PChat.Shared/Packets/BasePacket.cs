using System;

namespace P2PChat.Packets
{
	[Serializable]
	public abstract class BasePacket : IPacket
	{
		public abstract string Type { get; }
	}
}