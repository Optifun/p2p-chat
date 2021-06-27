using System;
using System.Collections.Generic;

namespace P2PChat.Packets
{
	public enum FetchAction
	{
		Null = 0,
		Fetch,
		Response
	}


	[Serializable]
	public class OnlineUsers : BasePacket
	{
		public override string Type => "fetch-pulse";
		public List<PublicUser> Users { get; protected set; }
		public FetchAction Action { get; protected set; }
		public int Port { get; protected set; }


		public OnlineUsers(List<PublicUser> list, FetchAction action)
		{
			Users = list;
			Action = action;
		}

		public OnlineUsers(List<PublicUser> list, FetchAction action, int port)
		{
			Users = list;
			Action = action;
			Port = port;
		}
	}
}