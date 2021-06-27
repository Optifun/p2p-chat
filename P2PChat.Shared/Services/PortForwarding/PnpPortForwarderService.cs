using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Open.Nat;

namespace P2PChat.Services.PortForwarding
{
	public class PnpPortForwarderService : NatTraversalService
	{
		public override async Task<int> TraversePort(NetProtocol protocol, int fallbackValue)
		{
			var discoverer = new NatDiscoverer();
			Random random = new Random();
			_router = null;
			try
			{
				var cts = new CancellationTokenSource(10000);
				_router = await discoverer.DiscoverDeviceAsync(PortMapper.Pmp, cts);
			}
			catch (NatDeviceNotFoundException ex)
			{
				Debug.Fail("Error: Enable port mapping on your router", ex.Message);
				return random.Next(4000, 6000);
			}

			List<int> busyUdpPorts = await GetBusyPorts(_router);
			_openedPort = FindAvailablePort(random, busyUdpPorts);

			await MapPortForDevice(protocol.ToPackageProtocol(), _router, _openedPort);
			return _openedPort;
		}
	}
}