using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using P2PChat.Reciever;

namespace P2PChat.Packets
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
		public int Port { get; protected set; }

		public OnlineUsers (List<PublicUser> list, FetchAction action)
		{
			Users = list;
			Action = action;
		}
		public OnlineUsers (List<PublicUser> list, FetchAction action, int port)
		{
			Users = list;
			Action = action;
			Port = port;
		}

		public static OnlineUsers Parse (NetworkData networkData)
		{
			MemoryStream stream = new MemoryStream(networkData.Data);
			var msg = _fmt.Deserialize(stream) as OnlineUsers;

			if ( msg is null || msg.Users == null )
				return null;

			return msg;
		}
	}
}
