using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using P2PChat.Client.Routes;
using P2PChat.Packets;
using P2PChat.Reciever;

namespace P2PChat.Client.Services
{
    public class ChatRepository : IChatRepository, IDisposable
    {
        public event Action<List<PublicUser>> UsersUpdated;
        public List<PublicUser> Users { get; protected set; }

        private readonly UDPObserver _observer;
        private readonly int _openedPort;
        private readonly IPEndPoint _stunIp;
        private readonly UserObserver _userObserver;
        private CancellationTokenSource _tokenSource;
        private Task _observationTask;

        public ChatRepository(UDPObserver observer, UserObserver userObserver, int openedPort, IPEndPoint stunIp)
        {
            _userObserver = userObserver;
            _stunIp = stunIp;
            _openedPort = openedPort;
            _observer = observer;
        }

        public void ObserveUsers(int refreshTime)
        {
            void RefreshLoop(object tokenObj)
            {
                var token = (CancellationToken) tokenObj;
                while (!token.IsCancellationRequested)
                {
                    _fetchUsers();
                    Thread.Sleep(refreshTime);
                }
            }

            _userObserver.UsersReceived += AddUser;
            _tokenSource = new CancellationTokenSource();
            _observationTask = Task.Factory.StartNew(RefreshLoop, _tokenSource.Token, TaskCreationOptions.LongRunning);
        }

        private void AddUser(List<PublicUser> acceptedUsers)
        {
            List<PublicUser> newUsers = acceptedUsers.Except(Users).ToList();
            if (newUsers.Count == 0)
                return;

            Users.AddRange(newUsers);
            UsersUpdated?.Invoke(Users);
        }

        public void StopObservation()
        {
            if (_observationTask != null)
                _tokenSource?.Cancel();
        }

        private void _fetchUsers()
        {
            var packet = new OnlineUsers(new List<PublicUser>(), FetchAction.Fetch, _openedPort);
            _observer.Send(packet, _stunIp);
        }

        public void Dispose()
        {
            StopObservation();
        }
    }
}