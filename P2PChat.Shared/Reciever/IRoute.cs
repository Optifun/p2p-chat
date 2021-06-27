using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Reciever
{
	public interface IRoute
	{
		Action Handle (NetworkData networkData);

		IRoute SetNext (IRoute route);
	}
}
