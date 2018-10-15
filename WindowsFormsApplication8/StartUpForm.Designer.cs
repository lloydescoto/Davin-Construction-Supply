namespace WindowsFormsApplication8
{
    partial class StartUpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartUpForm));
            this.lblIS = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDH = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIS
            // 
            this.lblIS.AutoSize = true;
            this.lblIS.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIS.ForeColor = System.Drawing.Color.Black;
            this.lblIS.Location = new System.Drawing.Point(133, 60);
            this.lblIS.Name = "lblIS";
            this.lblIS.Size = new System.Drawing.Size(253, 27);
            this.lblIS.TabIndex = 13;
            this.lblIS.Text = "INVENTORY SYSTEM";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(16, 286);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(483, 4);
            this.label3.TabIndex = 12;
            // 
            // lblDH
            // 
            this.lblDH.AutoSize = true;
            this.lblDH.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDH.ForeColor = System.Drawing.Color.Black;
            this.lblDH.Location = new System.Drawing.Point(95, 26);
            this.lblDH.Name = "lblDH";
            this.lblDH.Size = new System.Drawing.Size(332, 36);
            this.lblDH.TabIndex = 11;
            this.lblDH.Text = "DAVIN\'S HARDWARE";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-44, -131);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(603, 426);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCopyright.Location = new System.Drawing.Point(454, 292);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(47, 15);
            this.lblCopyright.TabIndex = 17;
            this.lblCopyright.Text = "© 2017";
            // 
            // StartUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(517, 317);
            this.ControlBox = false;
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblIS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDH);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartUpForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDH;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCopyright;
    }
}