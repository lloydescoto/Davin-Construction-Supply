using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace WindowsFormsApplication8
{
    public partial class ReportForm : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        MySqlConnection conn3;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";

        public ReportForm()
        {
            InitializeComponent();
            this.cbxReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            tblDaily.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tblDaily.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in tblDaily.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tblDaily.Hide();
            printdoc1.PrintPage += new PrintPageEventHandler(printdoc1_PrintPage);
        }
        PageSettings settings = new PageSettings();
        PrintDocument printdoc1 = new PrintDocument();
        PrintPreviewDialog previewdlg = new PrintPreviewDialog();
        Panel pannel = null;

        Bitmap MemoryImage;
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(1131, 529, pnl.CreateGraphics());
            Rectangle rect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            pnl.DrawToBitmap(MemoryImage, rect);
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(MemoryImage, 1131, 529);
        //    base.OnPaint(e);
        //}
        void printdoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (1131 / 2) - (1131 / 2), pannel.Location.Y);
        }

        private void InitializeComboBox()
        {
            this.cbxReportType.SelectedIndexChanged += new System.EventHandler(cbxReportType_SelectedIndexChanged);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            conn = new MySqlConnection(connectionString);
            conn2 = new MySqlConnection(connectionString);
            conn3 = new MySqlConnection(connectionString);
            conn.Open();
            conn2.Open();
            conn3.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            MySqlCommand cmd3 = new MySqlCommand();
            cmd.Connection = conn;
            cmd2.Connection = conn2;
            cmd3.Connection = conn3;
            MySqlDataReader reader;
            MySqlDataReader reader2;
            MySqlDataReader reader3;
            if (cbxReportType.Text == "")
            {
                MessageBox.Show("Please select a report type.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cbxReportType.Text == "Daily Sales Report")
            {
                if (date != "")
                {
                    lblDate.Text = "Date : " + dateTimePicker1.Value.ToString("MMMMMMMMMMMMM") + " " + dateTimePicker1.Value.Day + ", " + dateTimePicker1.Value.Year;
                    lblDay.Text = "Day : " + dateTimePicker1.Value.DayOfWeek;
                    panel1.Controls.Clear();
                    panel1.Controls.Add(tblDaily);
                    tblDaily.Rows.Clear();
                    tblDaily.Show();
                    cmd.CommandText = "SELECT * FROM transactions WHERE Date like '%" + date + "%' AND TransType like 'Sales'";
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Globals.transid = int.Parse(reader["TransId"].ToString());
                                int transid = int.Parse(reader["TransId"].ToString());
                                cmd.Parameters.Clear();
                                cmd2.Parameters.Clear();
                                cmd3.Parameters.Clear();
                                cmd2.CommandText = "SELECT * FROM sales " + "WHERE TransId like '" + transid + "'";
                                using (reader2 = cmd2.ExecuteReader())
                                {
                                    if (reader2.Read())
                                    {
                                        int salesInvoice = int.Parse(reader2["SalesInvoiceNumber"].ToString());
                                        int customerId = int.Parse(reader2["CustomerId"].ToString());
                                        cmd.Parameters.Clear();
                                        cmd2.Parameters.Clear();
                                        cmd3.Parameters.Clear();
                                        cmd3.CommandText = "SELECT * FROM customers " + "WHERE CustomerId like '" + customerId + "'";
                                        using (reader3 = cmd3.ExecuteReader())
                                        {
                                            while (reader3.Read())
                                            {
                                                tblDaily.Rows.Add(salesInvoice.ToString("000000000"), reader2["PaymentType"], reader["TotalAmount"], reader3["CustomerName"]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        reader.Close();
                    }
                }
                else
                    MessageBox.Show("Please choose date in the text box.");
            }
        }

        private void btnSearch_MouseEnter(object sender, EventArgs e)
        {
            btnSearch.BackColor = Color.SeaGreen;
            btnSearch.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnSearch.ForeColor = Color.White;
        }

        private void btnSearch_MouseLeave(object sender, EventArgs e)
        {
            btnSearch.BackColor = Color.FromArgb(229, 245, 242);
            btnSearch.FlatAppearance.BorderColor = Color.SeaGreen;
            btnSearch.ForeColor = Color.Black;
        }

        private void cbxReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedItem = (string)cbxReportType.SelectedItem;

            if (selectedItem == "Daily Sales Report")
            {
                lblSalesSummary.Text = "DAILY SALES SUMMARY";
            }

            else if (selectedItem == "Annual Report")
            {
                lblSalesSummary.Text = "ANNUAL SALES SUMMARY";
                MessageBox.Show("Sorry, Annual Sales Report is still under development.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
            //doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);
            //doc.Print();
            pannel = panel1;
            GetPrintArea(panel1);
            printdoc1.DefaultPageSettings.Landscape = true;
            previewdlg.Document = printdoc1;
            previewdlg.ShowDialog();
        }


        private void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Panel grd = new Panel();
            //Bitmap bmp = new Bitmap(grd.Width, grd.Height, grd.CreateGraphics());
            //grd.DrawToBitmap(bmp, new Rectangle(0, 0, grd.Width, grd.Height));
            //RectangleF bounds = e.PageSettings.PrintableArea;
            //float factor = ((float)bmp.Height / (float)bmp.Width);
            //e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, bounds.Width, factor * bounds.Width);
        }
    }
}
