namespace AuditaScanner
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginLabel = new Label();
            senhaLabel = new Label();
            auditaLabel = new Label();
            loginBtn = new Button();
            prestadoraLabel = new Label();
            loginTxt = new MaskedTextBox();
            senhaTxt = new MaskedTextBox();
            prestadorasCb = new ComboBox();
            SuspendLayout();
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Font = new Font("Segoe Fluent Icons", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            loginLabel.Location = new Point(153, 176);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(44, 21);
            loginLabel.TabIndex = 1;
            loginLabel.Text = "CPF:";
            // 
            // senhaLabel
            // 
            senhaLabel.AutoSize = true;
            senhaLabel.Font = new Font("Segoe Fluent Icons", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            senhaLabel.Location = new Point(153, 234);
            senhaLabel.Name = "senhaLabel";
            senhaLabel.Size = new Size(56, 21);
            senhaLabel.TabIndex = 1;
            senhaLabel.Text = "Senha:";
            // 
            // auditaLabel
            // 
            auditaLabel.AutoSize = true;
            auditaLabel.Font = new Font("Rubik", 24F, FontStyle.Bold, GraphicsUnit.Point);
            auditaLabel.Location = new Point(119, 86);
            auditaLabel.Name = "auditaLabel";
            auditaLabel.Size = new Size(266, 38);
            auditaLabel.TabIndex = 2;
            auditaLabel.Text = "Audita Scanner";
            // 
            // loginBtn
            // 
            loginBtn.Font = new Font("Rubik", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            loginBtn.Location = new Point(292, 394);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new Size(69, 32);
            loginBtn.TabIndex = 3;
            loginBtn.Text = "Logar";
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // prestadoraLabel
            // 
            prestadoraLabel.AutoSize = true;
            prestadoraLabel.Font = new Font("Segoe Fluent Icons", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            prestadoraLabel.Location = new Point(153, 302);
            prestadoraLabel.Name = "prestadoraLabel";
            prestadoraLabel.Size = new Size(83, 21);
            prestadoraLabel.TabIndex = 1;
            prestadoraLabel.Text = "Prestadora";
            // 
            // loginTxt
            // 
            loginTxt.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            loginTxt.Location = new Point(153, 200);
            loginTxt.Mask = "000,000,000-00";
            loginTxt.Name = "loginTxt";
            loginTxt.Size = new Size(208, 26);
            loginTxt.TabIndex = 4;
            // 
            // senhaTxt
            // 
            senhaTxt.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            senhaTxt.Location = new Point(153, 258);
            senhaTxt.Name = "senhaTxt";
            senhaTxt.PasswordChar = '*';
            senhaTxt.Size = new Size(208, 26);
            senhaTxt.TabIndex = 4;
            // 
            // prestadorasCb
            // 
            prestadorasCb.Font = new Font("Rubik", 12F, FontStyle.Regular, GraphicsUnit.Point);
            prestadorasCb.FormattingEnabled = true;
            prestadorasCb.Location = new Point(153, 326);
            prestadorasCb.Name = "prestadorasCb";
            prestadorasCb.Size = new Size(208, 27);
            prestadorasCb.TabIndex = 5;
            prestadorasCb.SelectedIndexChanged += prestadorasCb_SelectedIndexChanged;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 469);
            Controls.Add(prestadorasCb);
            Controls.Add(senhaTxt);
            Controls.Add(loginTxt);
            Controls.Add(loginBtn);
            Controls.Add(auditaLabel);
            Controls.Add(prestadoraLabel);
            Controls.Add(senhaLabel);
            Controls.Add(loginLabel);
            Name = "Login";
            Text = "Scanner Audita";
            Load += Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label loginLabel;
        private Label senhaLabel;
        private Label auditaLabel;
        private Button loginBtn;
        private Label prestadoraLabel;
        private MaskedTextBox loginTxt;
        private MaskedTextBox senhaTxt;
        private ComboBox prestadorasCb;
    }
}