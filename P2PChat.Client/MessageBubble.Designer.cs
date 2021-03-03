namespace P2PChat.Client
{
	partial class MessageBubble
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose (bool disposing)
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent ()
		{
			this.bubblePanel = new System.Windows.Forms.Panel();
			this.senderNick1 = new System.Windows.Forms.Label();
			this.senderNick2 = new System.Windows.Forms.Label();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.bubblePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// bubblePanel
			// 
			this.bubblePanel.AutoSize = true;
			this.bubblePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.bubblePanel.Controls.Add(this.senderNick2);
			this.bubblePanel.Controls.Add(this.senderNick1);
			this.bubblePanel.Controls.Add(this.textMessage);
			this.bubblePanel.Location = new System.Drawing.Point(8, 20);
			this.bubblePanel.Margin = new System.Windows.Forms.Padding(8, 20, 8, 20);
			this.bubblePanel.Name = "bubblePanel";
			this.bubblePanel.Size = new System.Drawing.Size(420, 144);
			this.bubblePanel.TabIndex = 0;
			// 
			// senderNick1
			// 
			this.senderNick1.AutoSize = true;
			this.senderNick1.Location = new System.Drawing.Point(8, 8);
			this.senderNick1.Name = "senderNick1";
			this.senderNick1.Size = new System.Drawing.Size(46, 17);
			this.senderNick1.TabIndex = 1;
			this.senderNick1.Text = "label1";
			// 
			// senderNick2
			// 
			this.senderNick2.AutoSize = true;
			this.senderNick2.Location = new System.Drawing.Point(342, 8);
			this.senderNick2.Name = "senderNick2";
			this.senderNick2.Size = new System.Drawing.Size(46, 17);
			this.senderNick2.TabIndex = 2;
			this.senderNick2.Text = "label1";
			// 
			// textMessage
			// 
			this.textMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textMessage.CausesValidation = false;
			this.textMessage.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textMessage.Location = new System.Drawing.Point(8, 31);
			this.textMessage.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.textMessage.MaxLength = 350;
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.ReadOnly = true;
			this.textMessage.Size = new System.Drawing.Size(396, 98);
			this.textMessage.TabIndex = 0;
			// 
			// MessageBubble
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.bubblePanel);
			this.MinimumSize = new System.Drawing.Size(430, 115);
			this.Name = "MessageBubble";
			this.Size = new System.Drawing.Size(436, 184);
			this.bubblePanel.ResumeLayout(false);
			this.bubblePanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel bubblePanel;
		private System.Windows.Forms.Label senderNick2;
		private System.Windows.Forms.Label senderNick1;
		private System.Windows.Forms.TextBox textMessage;
	}
}
