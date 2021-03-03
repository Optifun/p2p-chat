using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Reciever
{
	[Serializable]
	class Packet
	{
		public readonly byte[] Data;
		public readonly IPEndPoint Sender;
		public Packet(byte[] data, IPEndPoint sender)
		{
			Data = data;
			Sender = sender;
		}
	}
}

