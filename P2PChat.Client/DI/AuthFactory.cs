using Microsoft.Extensions.DependencyInjection;
using P2PChat.Client.Routes;
using P2PChat.Client.Services;
using P2PChat.Reciever;
using P2PChat.Services.Serializing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace P2PChat.Client.DI
{
    internal class AuthFactory
    {
        private IServiceScopeFactory _scopeFactory;
        private int _availablePort;
        private IPEndPoint _stunEndPoint;

        public AuthFactory(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public AuthForm CreateForm(int availablePort, IPEndPoint stunEP)
        {
            _availablePort = availablePort;
            _stunEndPoint = stunEP;
            return SetupAuthForm();
        }

        private AuthForm SetupAuthForm()
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                var serializer = scope.ServiceProvider.GetRequiredService<IPacketSerializerService>();
                var clientInformation = scope.ServiceProvider.GetRequiredService<IClientInformation>();

                var authController = new AuthObserver();
                var blackHole = new UndefinedResolver();
                IRoute routes = authController.Compose(blackHole);

                UDPObserver authFormObserver = InitializeUdpObserver(_availablePort, routes, serializer);
                var authForm = new AuthForm(clientInformation, _stunEndPoint, authFormObserver, _availablePort);

                authController.Success += authForm.ShowMainForm;
                authController.Error += authForm.PrintError;
                return authForm;
            }
        }

        private UDPObserver InitializeUdpObserver(int availablePort, IRoute routes, IPacketSerializerService serializer)
        {
            return new UDPObserver(availablePort, serializer, SynchronizationContext.Current, routes);
        }
    }
}