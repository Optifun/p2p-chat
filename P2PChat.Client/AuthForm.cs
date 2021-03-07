using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PChat.Client
{
	public partial class AuthForm : Form
	{
		public AuthForm ()
		{
			InitializeComponent();
		}

		private void loginButton_Click (object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
		}

		private void registerButton_Click (object sender, EventArgs e)
		{
			var login = loginBox.Text;
			var password = passwordBox.Text;
		}
	}
}
