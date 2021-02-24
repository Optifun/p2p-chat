namespace P2PChat.Client
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.chatList = new System.Windows.Forms.ListView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.onlineList = new System.Windows.Forms.ListView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.usernamePanel = new System.Windows.Forms.Panel();
			this.chatPanel = new System.Windows.Forms.Panel();
			this.chatboxPanel = new System.Windows.Forms.Panel();
			this.userNameLabel = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.usernamePanel.SuspendLayout();
			this.chatboxPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.usernamePanel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.chatPanel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.chatboxPanel, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(494, 446);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(296, 446);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.chatList);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(288, 417);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Chats";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// chatList
			// 
			this.chatList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatList.HideSelection = false;
			this.chatList.Location = new System.Drawing.Point(3, 3);
			this.chatList.Name = "chatList";
			this.chatList.Size = new System.Drawing.Size(282, 411);
			this.chatList.TabIndex = 0;
			this.chatList.UseCompatibleStateImageBehavior = false;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.onlineList);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(288, 417);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Online Users";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// onlineList
			// 
			this.onlineList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.onlineList.HideSelection = false;
			this.onlineList.Location = new System.Drawing.Point(3, 3);
			this.onlineList.Name = "onlineList";
			this.onlineList.Size = new System.Drawing.Size(282, 411);
			this.onlineList.TabIndex = 0;
			this.onlineList.UseCompatibleStateImageBehavior = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			this.splitContainer1.Panel1MinSize = 150;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel2MinSize = 300;
			this.splitContainer1.Size = new System.Drawing.Size(800, 450);
			this.splitContainer1.SplitterDistance = 300;
			this.splitContainer1.SplitterWidth = 2;
			this.splitContainer1.TabIndex = 3;
			// 
			// usernamePanel
			// 
			this.usernamePanel.Controls.Add(this.userNameLabel);
			this.usernamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.usernamePanel.Location = new System.Drawing.Point(3, 3);
			this.usernamePanel.Name = "usernamePanel";
			this.usernamePanel.Size = new System.Drawing.Size(488, 39);
			this.usernamePanel.TabIndex = 0;
			// 
			// chatPanel
			// 
			this.chatPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatPanel.Location = new System.Drawing.Point(3, 48);
			this.chatPanel.Name = "chatPanel";
			this.chatPanel.Size = new System.Drawing.Size(488, 320);
			this.chatPanel.TabIndex = 1;
			// 
			// chatboxPanel
			// 
			this.chatboxPanel.Controls.Add(this.button1);
			this.chatboxPanel.Controls.Add(this.textBox1);
			this.chatboxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatboxPanel.Location = new System.Drawing.Point(3, 374);
			this.chatboxPanel.Name = "chatboxPanel";
			this.chatboxPanel.Size = new System.Drawing.Size(488, 69);
			this.chatboxPanel.TabIndex = 2;
			// 
			// userNameLabel
			// 
			this.userNameLabel.AutoSize = true;
			this.userNameLabel.Font = new System.Drawing.Font("Roboto Lt", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.userNameLabel.Location = new System.Drawing.Point(3, 4);
			this.userNameLabel.Name = "userNameLabel";
			this.userNameLabel.Size = new System.Drawing.Size(76, 29);
			this.userNameLabel.TabIndex = 0;
			this.userNameLabel.Text = "Name";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(8, 8);
			this.textBox1.Margin = new System.Windows.Forms.Padding(8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(385, 49);
			this.textBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(400, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(79, 32);
			this.button1.TabIndex = 1;
			this.button1.Text = "Send";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(600, 480);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.usernamePanel.ResumeLayout(false);
			this.usernamePanel.PerformLayout();
			this.chatboxPanel.ResumeLayout(false);
			this.chatboxPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListView chatList;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView onlineList;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel usernamePanel;
		private System.Windows.Forms.Label userNameLabel;
		private System.Windows.Forms.Panel chatPanel;
		private System.Windows.Forms.Panel chatboxPanel;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
	}
}