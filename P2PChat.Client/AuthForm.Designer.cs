namespace P2PChat.Client
{
	partial class AuthForm
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

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.loginBox = new System.Windows.Forms.TextBox();
			this.passwordBox = new System.Windows.Forms.TextBox();
			this.loginButton = new System.Windows.Forms.Button();
			this.registerButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.errorLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.label1.Location = new System.Drawing.Point(140, 24);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "P2P Chat App";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(129, 76);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Login";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(129, 132);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Password";
			// 
			// loginBox
			// 
			this.loginBox.Location = new System.Drawing.Point(131, 92);
			this.loginBox.Margin = new System.Windows.Forms.Padding(2);
			this.loginBox.Name = "loginBox";
			this.loginBox.Size = new System.Drawing.Size(140, 20);
			this.loginBox.TabIndex = 3;
			// 
			// passwordBox
			// 
			this.passwordBox.Location = new System.Drawing.Point(131, 149);
			this.passwordBox.Margin = new System.Windows.Forms.Padding(2);
			this.passwordBox.Name = "passwordBox";
			this.passwordBox.PasswordChar = '*';
			this.passwordBox.Size = new System.Drawing.Size(140, 20);
			this.passwordBox.TabIndex = 4;
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(131, 194);
			this.loginButton.Margin = new System.Windows.Forms.Padding(2);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(68, 26);
			this.loginButton.TabIndex = 5;
			this.loginButton.Text = "Login";
			this.loginButton.UseVisualStyleBackColor = true;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// registerButton
			// 
			this.registerButton.Location = new System.Drawing.Point(203, 194);
			this.registerButton.Margin = new System.Windows.Forms.Padding(2);
			this.registerButton.Name = "registerButton";
			this.registerButton.Size = new System.Drawing.Size(68, 26);
			this.registerButton.TabIndex = 6;
			this.registerButton.Text = "Register";
			this.registerButton.UseVisualStyleBackColor = true;
			this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.Maroon;
			this.label4.Location = new System.Drawing.Point(199, 169);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(0, 13);
			this.label4.TabIndex = 7;
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.Maroon;
			this.label5.Location = new System.Drawing.Point(203, 178);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(0, 13);
			this.label5.TabIndex = 7;
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// errorLabel
			// 
			this.errorLabel.AutoSize = true;
			this.errorLabel.ForeColor = System.Drawing.Color.Maroon;
			this.errorLabel.Location = new System.Drawing.Point(204, 171);
			this.errorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.errorLabel.Name = "errorLabel";
			this.errorLabel.Size = new System.Drawing.Size(0, 13);
			this.errorLabel.TabIndex = 7;
			this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AuthForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(415, 288);
			this.Controls.Add(this.errorLabel);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.registerButton);
			this.Controls.Add(this.loginButton);
			this.Controls.Add(this.passwordBox);
			this.Controls.Add(this.loginBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "AuthForm";
			this.Text = "Auth";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthForm_FormClosing);
			this.Load += new System.EventHandler(this.AuthForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.Label errorLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox loginBox;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.TextBox passwordBox;
		private System.Windows.Forms.Button registerButton;

		#endregion
	}
}

