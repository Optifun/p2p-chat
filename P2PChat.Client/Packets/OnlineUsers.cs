using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;

namespace P2PChat.Client.Packets
{
	public enum FetchAction
	{
		Null = 0,
		Fetch,
		Responce
	}


	[Serializable]
	public class OnlineUsers : BasePacket
	{
		public List<PublicUser> Users { get; protected set; }
		public FetchAction Action { get; protected set; }

		public OnlineUsers (List<PublicUser> list, FetchAction action)
		{
			Users = list;
			Action = action;
		}

		public static OnlineUsers Parse (Packet packet)
		{
			MemoryStream stream = new MemoryStream(packet.Data);
			var msg = _fmt.Deserialize(stream) as OnlineUsers;

			if ( msg is null || msg.Users == null )
				return null;

			return msg;
		}
	}
}
