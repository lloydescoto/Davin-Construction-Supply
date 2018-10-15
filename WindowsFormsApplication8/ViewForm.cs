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

namespace WindowsFormsApplication8
{
    public partial class ViewForm : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        public ViewForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView3.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView4.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView5.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connectionString);
            conn2 = new MySqlConnection(connectionString);
            conn.Open();
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            cmd2.Connection = conn2;
            MySqlDataReader reader;
            MySqlDataReader reader2;
            cmd.CommandText = "SELECT * FROM Items";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"]);
                }
                reader.Close();
            }
            cmd.CommandText = "SELECT * FROM Stocks";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                    Globals.quantity = int.Parse(reader["Quantity"].ToString());
                    Globals.status = reader["Status"].ToString();
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    itemCParam.Value = Globals.itemcode;
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            dataGridView2.Rows.Add(reader2["ItemName"], Globals.quantity, Globals.status);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }
            cmd.CommandText = "SELECT * FROM Transactions";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView3.Rows.Add(reader["TransId"], reader["TotalAmount"], reader["TransType"], reader["Date"]);
                }
                reader.Close();
            }
            cmd.CommandText = "SELECT * FROM Sales";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Globals.salesid = int.Parse(reader["SalesId"].ToString());
                    Globals.quantity = int.Parse(reader["Quantity"].ToString());
                    Globals.transid = int.Parse(reader["TransId"].ToString());
                    Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    itemCParam.Value = Globals.itemcode;
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            dataGridView4.Rows.Add(Globals.salesid, reader2["ItemName"], reader2["ItemSellPrice"], Globals.quantity, Globals.transid, reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }
            cmd.CommandText = "SELECT * FROM Purchases";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Globals.purchid = int.Parse(reader["PurchId"].ToString());
                    Globals.quantity = int.Parse(reader["Quantity"].ToString());
                    Globals.transid = int.Parse(reader["TransId"].ToString());
                    Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    itemCParam.Value = Globals.itemcode;
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            dataGridView5.Rows.Add(Globals.purchid, reader2["ItemName"], reader2["ItemPrice"], Globals.quantity, Globals.transid, reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }

            cmd.CommandText = "SELECT * FROM Payables";
            using (reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    dataGridView6.Rows.Add(reader["PayableId"], reader["PurchId"], reader["Status"]);
                }
                reader.Close();
            }

            cmd.CommandText = "SELECT * FROM Receivables";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView7.Rows.Add(reader["ReceivableId"], reader["SalesId"], reader["Status"]);
                }
                reader.Close();
            }

        }
    }
}
