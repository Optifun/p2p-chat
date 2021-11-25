using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PChat.Client
{
    public partial class MessageBubble : UserControl
    {
        public MessageBubble(string nickname, string text, bool self)
        {
            InitializeComponent();
            bubblePanel.BackColor = self ? Color.AliceBlue : Color.WhiteSmoke;
            senderNick1.Visible = !self;
            senderNick2.Visible = self;
            senderNick1.Text = nickname;
            senderNick2.Text = nickname;
            textMessage.Text = text;
        }

        //public void ResizeControl(object sender, EventArgs e)
        //{
        //	var panel = sender as Control;
        //	var newSize = new Size(panel.Size.Width, panel.Size.Height);
        //	Size = new Size(newSize.Width, Size.Height);
        //	var oldSize = Size;
        //	Scale(new SizeF(newSize.Width / oldSize.Width, 1));
        //}
    }
}