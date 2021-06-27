using System.Threading.Tasks;
using Open.Nat;

namespace P2PChat.Services.PortForwarding
{
	public interface IPortForwarder
	{
		Task<int> TraversePort(Protocol protocol, int fallbackValue);

		Task DisableTraversal(Protocol protocol);
	}
}