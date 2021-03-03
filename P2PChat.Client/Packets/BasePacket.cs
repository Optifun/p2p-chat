using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Client.Packets
{
	[Serializable]
	public abstract class BasePacket : IPacket
	{
		protected static BinaryFormatter _fmt = new BinaryFormatter();

		public virtual byte[] ToBytes ()
		{
			MemoryStream stream = new MemoryStream();
			_fmt.Serialize(stream, this);
			return stream.ToArray();
		}
	}
}
