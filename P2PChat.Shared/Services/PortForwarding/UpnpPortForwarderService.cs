using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Open.Nat;

namespace P2PChat.Services.PortForwarding
{
	public class UpnpPortForwarderService : NatTraversalService
	{
		public override async Task<int> TraversePort(Protocol protocol, int fallbackValue = 0)
		{
			var discoverer = new NatDiscoverer();
			Random random = new Random();
			NatDevice router = null;
			try
			{
				var cts = new CancellationTokenSource(10000);
				router = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);
			}
			catch (NatDeviceNotFoundException ex)
			{
				Debug.Fail("Error: Enable port mapping on your router", ex.Message);
				return random.Next(4000, 6000);
			}

			List<int> busyUdpPorts = await GetBusyPorts(router);
			int availablePort = FindAvailablePort(random, busyUdpPorts);

			await MapPortForDevice(protocol, router, availablePort);
			return availablePort;
		}
	}
}