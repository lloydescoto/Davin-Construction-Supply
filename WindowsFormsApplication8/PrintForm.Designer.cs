namespace WindowsFormsApplication8
{
    partial class PrintForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.panelLine = new System.Windows.Forms.Panel();
            this.tblItems = new System.Windows.Forms.DataGridView();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.tblItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Arial", 11F);
            this.lblName.Location = new System.Drawing.Point(74, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(59, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name : ";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Arial", 11F);
            this.lblAddress.Location = new System.Drawing.Point(57, 44);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(74, 17);
            this.lblAddress.TabIndex = 1;
            this.lblAddress.Text = "Address : ";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Arial", 11F);
            this.lblDate.Location = new System.Drawing.Point(516, 44);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(47, 17);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Date :";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Font = new System.Drawing.Font("Arial", 11F);
            this.lblNo.Location = new System.Drawing.Point(526, 24);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(38, 17);
            this.lblNo.TabIndex = 2;
            this.lblNo.Text = "No : ";
            // 
            // panelLine
            // 
            this.panelLine.BackColor = System.Drawing.Color.Black;
            this.panelLine.Location = new System.Drawing.Point(29, 72);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(710, 4);
            this.panelLine.TabIndex = 4;
            // 
            // tblItems
            // 
            this.tblItems.AllowUserToAddRows = false;
            this.tblItems.AllowUserToDeleteRows = false;
            this.tblItems.AllowUserToResizeRows = false;
            this.tblItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tblItems.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.tblItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tblItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tblItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Quantity,
            this.Unit,
            this.ItemDescription,
            this.Price,
            this.Subtotal});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tblItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.tblItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tblItems.GridColor = System.Drawing.Color.DarkGray;
            this.tblItems.Location = new System.Drawing.Point(29, 75);
            this.tblItems.Name = "tblItems";
            this.tblItems.ReadOnly = true;
            this.tblItems.RowHeadersVisible = false;
            this.tblItems.Size = new System.Drawing.Size(710, 552);
            this.tblItems.TabIndex = 5;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // ItemDescription
            // 
            this.ItemDescription.HeaderText = "Item Description";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // Subtotal
            // 
            this.Subtotal.HeaderText = "Sub-total";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Arial", 11F);
            this.lblTotalAmount.Location = new System.Drawing.Point(470, 740);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(130, 17);
            this.lblTotalAmount.TabIndex = 6;
            this.lblTotalAmount.Text = "TOTAL AMOUNT =";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(29, 629);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 4);
            this.panel1.TabIndex = 7;
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(768, 780);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.tblItems);
            this.Controls.Add(this.panelLine);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("Arial", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PrintForm";
            ((System.ComponentModel.ISupportInitialize)(this.tblItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.DataGridView tblItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel panel1;
    }
}