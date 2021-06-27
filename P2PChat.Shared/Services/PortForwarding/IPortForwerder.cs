using System.Threading.Tasks;

namespace P2PChat.Services.PortForwarding
{
	public interface IPortForwarder
	{
		Task<int> TraversePort(NetProtocol protocol, int fallbackValue);

		Task DisableTraversal(NetProtocol protocol);
	}
}