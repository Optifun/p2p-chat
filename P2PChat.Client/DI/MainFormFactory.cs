﻿using Microsoft.Extensions.DependencyInjection;
using P2PChat.Client.Routes;
using P2PChat.Client.Services;
using P2PChat.Reciever;
using P2PChat.Services.Serializing;
using System;
using System.Net;
using System.Windows.Forms;

namespace P2PChat.Client.DI
{
    class MainFormFactory
    {
        IServiceScopeFactory _scopeFactory;
        private int _availablePort;
        private IPEndPoint _stunEndPoint;
        private IClientInformation _client;

        public MainFormFactory(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public MainForm CreateForm(IClientInformation clientInformation, int availablePort, IPEndPoint stunEP)
        {
            _availablePort = availablePort;
            _stunEndPoint = stunEP;
            _client = clientInformation;
            return SetupMainForm();
        }

        private MainForm SetupMainForm()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                IServiceProvider provider = scope.ServiceProvider;
                var serializer = provider.GetRequiredService<IPacketSerializerService>();

                var userObserver = new UserObserver();
                var messageObserver = new MessageObserver(_client.User.UserID);
                var blackHole = new UndefinedResolver();
                var routes = userObserver.Compose(messageObserver, blackHole);

                var mainFormObserver = InitializeUdpObserver(_availablePort, routes, serializer);

                IChatRepository chatRepository = new ChatRepository(mainFormObserver, userObserver, _availablePort, _stunEndPoint);
                IMessageRepository messageRepository = new MessageRepository(chatRepository, _client, messageObserver, mainFormObserver);

                var mainForm = new MainForm(_client, messageRepository, chatRepository);

                return mainForm;
            }
        }

        private UDPObserver InitializeUdpObserver(int availablePort, IRoute routes, IPacketSerializerService serializer) =>
            new UDPObserver(availablePort, serializer, WindowsFormsSynchronizationContext.Current, routes);
    }
}