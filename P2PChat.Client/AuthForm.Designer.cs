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
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent ()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.loginBox = new System.Windows.Forms.TextBox();
			this.passwordBox = new System.Windows.Forms.TextBox();
			this.loginButton = new System.Windows.Forms.Button();
			this.registerButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(186, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(162, 29);
			this.label1.TabIndex = 0;
			this.label1.Text = "P2P Chat App";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(172, 93);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Login";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(172, 163);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Password";
			// 
			// loginBox
			// 
			this.loginBox.Location = new System.Drawing.Point(175, 113);
			this.loginBox.Name = "loginBox";
			this.loginBox.Size = new System.Drawing.Size(186, 22);
			this.loginBox.TabIndex = 3;
			// 
			// passwordBox
			// 
			this.passwordBox.Location = new System.Drawing.Point(175, 183);
			this.passwordBox.Name = "passwordBox";
			this.passwordBox.PasswordChar = '*';
			this.passwordBox.Size = new System.Drawing.Size(186, 22);
			this.passwordBox.TabIndex = 4;
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(175, 239);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(90, 32);
			this.loginButton.TabIndex = 5;
			this.loginButton.Text = "Login";
			this.loginButton.UseVisualStyleBackColor = true;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// registerButton
			// 
			this.registerButton.Location = new System.Drawing.Point(271, 239);
			this.registerButton.Name = "registerButton";
			this.registerButton.Size = new System.Drawing.Size(90, 32);
			this.registerButton.TabIndex = 6;
			this.registerButton.Text = "Register";
			this.registerButton.UseVisualStyleBackColor = true;
			this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
			// 
			// AuthForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(553, 355);
			this.Controls.Add(this.registerButton);
			this.Controls.Add(this.loginButton);
			this.Controls.Add(this.passwordBox);
			this.Controls.Add(this.loginBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "AuthForm";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox loginBox;
		private System.Windows.Forms.TextBox passwordBox;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.Button registerButton;
	}
}

