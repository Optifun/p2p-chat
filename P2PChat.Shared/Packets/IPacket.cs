﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Packets
{
	public interface IPacket
	{
		byte[] ToBytes ();
	}
}
