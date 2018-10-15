namespace WindowsFormsApplication8
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.titleLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.line5 = new System.Windows.Forms.Label();
            this.line4 = new System.Windows.Forms.Label();
            this.line6 = new System.Windows.Forms.Label();
            this.line2 = new System.Windows.Forms.Label();
            this.passTxt = new System.Windows.Forms.TextBox();
            this.userTxt = new System.Windows.Forms.TextBox();
            this.line1 = new System.Windows.Forms.Label();
            this.line3 = new System.Windows.Forms.Label();
            this.btnShowPassword = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.toolTipShowPass = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MintCream;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(152, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MintCream;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(275, 126);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 32);
            this.button2.TabIndex = 5;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.White;
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(27, 7);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(93, 92);
            this.logo.TabIndex = 15;
            this.logo.TabStop = false;
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Arial", 17F);
            this.titleLbl.ForeColor = System.Drawing.Color.Black;
            this.titleLbl.Location = new System.Drawing.Point(120, 43);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(366, 26);
            this.titleLbl.TabIndex = 14;
            this.titleLbl.Text = "DAVIN CONSTRUCTION SUPPLY";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.SeaGreen;
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(11, 304);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(495, 4);
            this.label3.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MintCream;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.line5);
            this.panel1.Controls.Add(this.line4);
            this.panel1.Controls.Add(this.line6);
            this.panel1.Controls.Add(this.line2);
            this.panel1.Controls.Add(this.passTxt);
            this.panel1.Controls.Add(this.userTxt);
            this.panel1.Controls.Add(this.line1);
            this.panel1.Controls.Add(this.line3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnShowPassword);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Controls.Add(this.lblPass);
            this.panel1.Location = new System.Drawing.Point(-4, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 180);
            this.panel1.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SeaGreen;
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(94, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(3, 33);
            this.label2.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SeaGreen;
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(94, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(3, 33);
            this.label1.TabIndex = 61;
            // 
            // line5
            // 
            this.line5.BackColor = System.Drawing.Color.SeaGreen;
            this.line5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.line5.Location = new System.Drawing.Point(435, 73);
            this.line5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.line5.Name = "line5";
            this.line5.Size = new System.Drawing.Size(3, 33);
            this.line5.TabIndex = 58;
            // 
            // line4
            // 
            this.line4.BackColor = System.Drawing.Color.SeaGreen;
            this.line4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.line4.Location = new System.Drawing.Point(95, 73);
            this.line4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.line4.Name = "line4";
            this.line4.Size = new System.Drawing.Size(343, 3);
            this.line4.TabIndex = 57;
            // 
            // line6
            // 
            this.line6.BackColor = System.Drawing.Color.SeaGreen;
            this.line6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.line6.Location = new System.Drawing.Point(94, 103);
            this.line6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.line6.Name = "line6";
            this.line6.Size = new System.Drawing.Size(343, 3);
            this.line6.TabIndex = 52;
            // 
            // line2
            // 
            this.line2.BackColor = System.Drawing.Color.SeaGreen;
            this.line2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.line2.Location = new System.Drawing.Point(435, 25);
            this.line2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(3, 33);
            this.line2.TabIndex = 56;
            // 
            // passTxt
            // 
            this.passTxt.BackColor = System.Drawing.SystemColors.Window;
            this.passTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passTxt.Font = new System.Drawing.Font("Arial", 13F);
            this.passTxt.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.passTxt.Location = new System.Drawing.Point(136, 80);
            this.passTxt.Name = "passTxt";
            this.passTxt.ShortcutsEnabled = false;
            this.passTxt.Size = new System.Drawing.Size(268, 20);
            this.passTxt.TabIndex = 4;
            this.passTxt.Text = "Password";
            this.passTxt.Enter += new System.EventHandler(this.passTxt_Enter);
            this.passTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passTxt_KeyDown);
            // 
            // userTxt
            // 
            this.userTxt.BackColor = System.Drawing.SystemColors.Window;
            this.userTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userTxt.Font = new System.Drawing.Font("Arial", 13F);
            this.userTxt.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.userTxt.Location = new System.Drawing.Point(136, 31);
            this.userTxt.Name = "userTxt";
            this.userTxt.Size = new System.Drawing.Size(300, 20);
            this.userTxt.TabIndex = 3;
            this.userTxt.Text = "Username";
            this.userTxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.userTxt.Enter += new System.EventHandler(this.userTxt_Enter);
            this.userTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userTxt_KeyDown);
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.SeaGreen;
            this.line1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.line1.Location = new System.Drawing.Point(95, 24);
            this.line1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(343, 3);
            this.line1.TabIndex = 55;
            // 
            // line3
            // 
            this.line3.BackColor = System.Drawing.Color.SeaGreen;
            this.line3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.line3.Location = new System.Drawing.Point(94, 55);
            this.line3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(343, 3);
            this.line3.TabIndex = 51;
            // 
            // btnShowPassword
            // 
            this.btnShowPassword.AutoSize = true;
            this.btnShowPassword.BackColor = System.Drawing.Color.White;
            this.btnShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowPassword.Font = new System.Drawing.Font("Arial", 19F);
            this.btnShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.btnShowPassword.Location = new System.Drawing.Point(403, 73);
            this.btnShowPassword.Name = "btnShowPassword";
            this.btnShowPassword.Size = new System.Drawing.Size(35, 31);
            this.btnShowPassword.TabIndex = 50;
            this.btnShowPassword.Text = "👁";
            this.toolTipShowPass.SetToolTip(this.btnShowPassword, "Long press to show");
            this.btnShowPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnShowPassword_MouseDown);
            this.btnShowPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnShowPassword_MouseUp);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.White;
            this.lblUser.Font = new System.Drawing.Font("Arial", 21F);
            this.lblUser.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblUser.Location = new System.Drawing.Point(100, 25);
            this.lblUser.Margin = new System.Windows.Forms.Padding(0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(36, 32);
            this.lblUser.TabIndex = 63;
            this.lblUser.Text = "👤";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.BackColor = System.Drawing.Color.White;
            this.lblPass.Font = new System.Drawing.Font("Arial", 17F);
            this.lblPass.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblPass.Location = new System.Drawing.Point(101, 76);
            this.lblPass.Margin = new System.Windows.Forms.Padding(0);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(30, 26);
            this.lblPass.TabIndex = 64;
            this.lblPass.Text = "🔑";
            // 
            // toolTipShowPass
            // 
            this.toolTipShowPass.AutoPopDelay = 5000;
            this.toolTipShowPass.InitialDelay = 500;
            this.toolTipShowPass.IsBalloon = true;
            this.toolTipShowPass.ReshowDelay = 100;
            this.toolTipShowPass.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipShowPass.ToolTipTitle = "Show Password";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(517, 317);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.label3);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.TransparencyKey = System.Drawing.Color.DimGray;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label line5;
        private System.Windows.Forms.Label line4;
        private System.Windows.Forms.Label line6;
        private System.Windows.Forms.Label line2;
        private System.Windows.Forms.TextBox passTxt;
        private System.Windows.Forms.TextBox userTxt;
        private System.Windows.Forms.Label line1;
        private System.Windows.Forms.Label line3;
        private System.Windows.Forms.Label btnShowPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.ToolTip toolTipShowPass;
    }
}

