using System;
using System.Windows.Forms;
using P2PChat.Reciever;
using P2PChat.Packets;
using System.Net;
using P2PChat.Client.Services;

namespace P2PChat.Client
{
    public partial class AuthForm : Form
    {
        private UDPObserver _observer;
        private IClientInformation _clientInformation;
        private IPEndPoint _stunIp;
        private int _openPort;

        public AuthForm(IClientInformation clientInformation, IPEndPoint stunIp, UDPObserver observer, int openPort)
        {
            InitializeComponent();
            _stunIp = stunIp;
            _observer = observer;
            _clientInformation = clientInformation;
            _openPort = openPort;
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            _observer.Start();
        }

        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _observer.Stop();
        }

        public void PrintError(string error)
        {
            errorLabel.Text = error;
        }

        private void SendAuthPacket(AuthAction packet)
        {
            _observer.Send(packet, _stunIp);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string login = loginBox.Text;
            string password = passwordBox.Text;
            SendAuthPacket(new AuthAction(login, password, AuthType.Login, _openPort));
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string login = loginBox.Text;
            string password = passwordBox.Text;
            SendAuthPacket(new AuthAction(login, password, AuthType.Register, _openPort));
        }

        public void ShowMainForm(PublicUser userData)
        {
            _observer.Stop();
            _clientInformation.User = userData;
            Hide();
            Program.MainForm.Show();
        }
    }
}