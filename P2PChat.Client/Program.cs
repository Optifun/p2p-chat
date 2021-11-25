using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using P2PChat.Client.Routes;
using P2PChat.Client.Services;
using P2PChat.Reciever;
using P2PChat.Services.PortForwarding;
using P2PChat.Services.Serializing;
using Microsoft.Extensions.DependencyInjection;
using P2PChat.Client.DI;

namespace P2PChat.Client
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IClientInformation ClientInformation;
        public static IPEndPoint StunAddress;
        public static AuthForm AuthForm;
        public static MainForm MainForm;

        private static int _availablePort;

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services
                .AddSingleton<IPortForwarder, UpnpPortForwarderService>()
                .AddSingleton<IClientInformation, ClientInformation>()
                .AddTransient<IPacketSerializerService, BinarySerializerService>()
                .AddSingleton<AuthFactory>()
                .AddSingleton<MainFormFactory>();

            ServiceProvider = services.BuildServiceProvider();
        }

        private static async Task SetupServices(IServiceProvider app)
        {
            var forwarder = app.GetRequiredService<IPortForwarder>();
            var clientInformation = app.GetRequiredService<IClientInformation>();
            _availablePort = await forwarder.TraversePort(NetProtocol.Udp, 0);
            clientInformation.OpenedPort = _availablePort;
            var stunIp = await ResolveStunIp();
            StunAddress = new IPEndPoint(stunIp, 23434);

            var authFactory = app.GetRequiredService<AuthFactory>();
            AuthForm = authFactory.CreateForm(_availablePort, StunAddress);

            var mainFactory = app.GetRequiredService<MainFormFactory>();
            MainForm = mainFactory.CreateForm(clientInformation, _availablePort, StunAddress);
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigureServices();
            await SetupServices(ServiceProvider);

            if (StunAddress != null)
                Application.Run(AuthForm);
        }

        private static async Task<IPAddress> ResolveStunIp()
        {
            const string settingsPath = "./settings.json";
            if (!File.Exists(settingsPath))
                return IPAddress.Loopback;
            var parser = new JsonConfigParserService(settingsPath);
            parser.Parse();

            if (!string.IsNullOrEmpty(parser.Host))
            {
                var ipAddresses = await Dns.GetHostAddressesAsync(parser.Host);
                var publicIp = ipAddresses
                    .First(ipAddress => !ipAddress.ToString().StartsWith("25.")
                                        && ipAddress.AddressFamily == AddressFamily.InterNetwork);
                return publicIp;
            }

            if (!string.IsNullOrEmpty(parser.Ip))
                return IPAddress.Parse(parser.Ip);
            else
                return IPAddress.Loopback;
        }
    }
}