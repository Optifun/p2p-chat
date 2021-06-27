using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Open.Nat;

namespace P2PChat.Services.PortForwarding
{
	public abstract class NatTraversalService : IPortForwarder
	{
		protected NatDevice _router;
		protected int _openedPort;
		public abstract Task<int> TraversePort(NetProtocol protocol, int fallbackValue);

		public async Task DisableTraversal(NetProtocol protocol)
		{
				await _router.DeletePortMapAsync(new Mapping(protocol.ToPackageProtocol(), _openedPort, _openedPort));
		}

		protected static async Task MapPortForDevice(Protocol protocol, NatDevice router, int availablePort)
		{
			var mapping = new Mapping(protocol, availablePort, availablePort, "P2P_Chat_User");
			await router.CreatePortMapAsync(mapping);
		}

		/// <summary>
		/// Нахождение всех занятых портов (публичных и приватных)
		/// </summary>
		/// <param name="router"></param>
		/// <returns></returns>
		protected static async Task<List<int>> GetBusyPorts(NatDevice router)
		{
			List<int> busyUdpPorts = new List<int>();
			var mappings = await router.GetAllMappingsAsync();
			foreach (Mapping mapping in mappings)
			{
				if (mapping.Protocol == Protocol.Udp)
				{
					Debug.WriteLine(mapping);

					busyUdpPorts.Add(mapping.PrivatePort);
					busyUdpPorts.Add(mapping.PublicPort);
				}
			}

			return busyUdpPorts;
		}

		/// <summary>
		/// Нахождение свободного порта
		/// </summary>
		/// <param name="random"></param>
		/// <param name="busyUdpPorts"></param>
		/// <returns></returns>
		protected static int FindAvailablePort(Random random, List<int> busyUdpPorts)
		{
			int availablePort = random.Next(30000, 65535);
			while (busyUdpPorts.Contains(availablePort))
				availablePort = random.Next(30000, 65535);

			return availablePort;
		}
	}
}