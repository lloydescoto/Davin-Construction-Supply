using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication8
{
    public partial class Update : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        string updateCategory = "";
        string[] availFraction = { "1", "1/2", "1/4", "1/8" };
        double[] availFractionValue = { 1, 0.50, 0.25, 0.125 };

        public Update()
        {
            InitializeComponent();
            HideShow();
            btnClear.Hide();
            btnUpdate.Hide();
            cbxD6.SelectedIndex = 0;
        }

        private void btnItems_MouseEnter(object sender, EventArgs e)
        {
            btnItems.BackColor = Color.SeaGreen;
            btnItems.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnItems.ForeColor = Color.White;
        }

        private void btnItems_MouseLeave(object sender, EventArgs e)
        {
            btnItems.BackColor = Color.FromArgb(229, 245, 242);
            btnItems.FlatAppearance.BorderColor = Color.SeaGreen;
            btnItems.ForeColor = Color.Black;
        }

        private void btnReceivables_MouseEnter(object sender, EventArgs e)
        {
            btnReceivables.BackColor = Color.SeaGreen;
            btnReceivables.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnReceivables.ForeColor = Color.White;
        }

        private void btnReceivables_MouseLeave(object sender, EventArgs e)
        {
            btnReceivables.BackColor = Color.FromArgb(229, 245, 242);
            btnReceivables.FlatAppearance.BorderColor = Color.SeaGreen;
            btnReceivables.ForeColor = Color.Black;
        }

        private void btnPayables_MouseEnter(object sender, EventArgs e)
        {
            btnPayables.BackColor = Color.SeaGreen;
            btnPayables.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnPayables.ForeColor = Color.White;
        }

        private void btnPayables_MouseLeave(object sender, EventArgs e)
        {
            btnPayables.BackColor = Color.FromArgb(229, 245, 242);
            btnPayables.FlatAppearance.BorderColor = Color.SeaGreen;
            btnPayables.ForeColor = Color.Black;
        }

        private void btnTransactions_MouseEnter(object sender, EventArgs e)
        {
            btnTransactions.BackColor = Color.SeaGreen;
            btnTransactions.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnTransactions.ForeColor = Color.White;
        }

        private void btnTransactions_MouseLeave(object sender, EventArgs e)
        {
            btnTransactions.BackColor = Color.FromArgb(229, 245, 242);
            btnTransactions.FlatAppearance.BorderColor = Color.SeaGreen;
            btnTransactions.ForeColor = Color.Black;
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            updateCategory = "Items";
            Clear();
            HideShow();
            lblD1.Text = "Item Code :";
            lblD2.Text = "Item Name :";
            lblD3.Text = "Unit Price :";
            lblD4.Text = "Selling Price :";
            txtD4.Location = new Point(142, 157);

            tblItems.Rows.Clear();
            tblItems.Show();
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
                    cmd.Parameters.Clear();
                    cmd2.Parameters.Clear();
                    int itemcode = int.Parse(reader["ItemCode"].ToString());
                    cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itemcode";
                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    itemCParam.Value = itemcode;
                    cmd2.Parameters.Add(itemCParam);

                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            tblItems.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"], reader2["Quantity"]);
                        }
                    }
                }
                reader.Close();
            }
        }

        private void btnReceivables_Click(object sender, EventArgs e)
        {
            updateCategory = "Receivables";
            Clear();
            HideShow();
            lblD1.Text = "Sales ID :";
            lblD2.Text = "Item Code :";
            lblD3.Text = "Transaction ID :";
            lblD4.Text = "Status :";
            txtD5.Items.Clear();
            txtD5.Items.Add("Paid");
            txtD5.Items.Add("Unpaid");
            txtD5.Items.Add("Returned");
            txtD5.Items.Add("Shipping");
            lblD6.Visible = true;
            cbxD6.Visible = true;
            cbxD6.Items.Clear();
            cbxD6.Items.Add("None");
            cbxD6.Items.Add("Paid");
            cbxD6.Items.Add("Unpaid");
            cbxD6.Items.Add("Returned");
            cbxD6.Items.Add("COD");
            cbxD6.Items.Add("Shipping");

            tblSales.Rows.Clear();
            tblSales.Show();
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
            cmd.CommandText = "SELECT * FROM Sales";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            if(reader["PaymentType"].ToString() == "COD" && reader["Status"].ToString() == "Unpaid")
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], "ToShip");
                            else
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], reader["Status"]);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }
        }

        private void btnPayables_Click(object sender, EventArgs e)
        {
            updateCategory = "Payables";
            Clear();
            HideShow();
            lblD1.Text = "Purchase ID :";
            lblD2.Text = "Item Code :";
            lblD3.Text = "Transaction ID :";
            lblD4.Text = "Status :";
            txtD5.Items.Clear();
            txtD5.Items.Add("Paid");
            txtD5.Items.Add("Unpaid");
            txtD5.Items.Add("Returned");
            lblD6.Visible = true;
            cbxD6.Visible = true;
            cbxD6.Items.Clear();
            cbxD6.Items.Add("None");
            cbxD6.Items.Add("Paid");
            cbxD6.Items.Add("Unpaid");
            cbxD6.Items.Add("Returned");

            tblPurchases.Rows.Clear();
            tblPurchases.Show();
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
            cmd.CommandText = "SELECT * FROM Purchases";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            tblPurchases.Rows.Add(reader["PurchId"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["Status"]);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            updateCategory = "Transactions";
            Clear();
            HideShow();
            lblD1.Text = "Transaction ID :";
            lblD2.Text = "Total Amount :";
            lblD3.Text = "Category :";
            lblD4.Text = "Status :";
            txtD5.Items.Clear();
            txtD5.Items.Add("Paid");
            txtD5.Items.Add("Unpaid");
            txtD5.Items.Add("Returned");
            txtD5.Items.Add("Shipping - applies only to Sales");

            tblTransactions.Rows.Clear();
            tblTransactions.Show();
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            MySqlDataReader reader;
            cmd.CommandText = "SELECT * FROM Transactions";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tblTransactions.Rows.Add(reader["TransId"], reader["TotalAmount"], reader["TransType"], reader["Date"]);
                }
                reader.Close();
            }
        }

        private void HideShow()
        {
            tblItems.Hide();
            tblPurchases.Hide();
            tblSales.Hide();
            tblTransactions.Hide();
            btnClear.Show();
            btnUpdate.Show();

            if (updateCategory == "Items")
            {
                lblD1.Show();
                lblD2.Show();
                lblD3.Show();
                lblD4.Show();
                lblD5.Hide();
                txtD1.Show();
                txtD2.Show();
                txtD3.Show();
                txtD4.Show();
                txtD5.Hide();
                btnUpdate.Location = new Point(142, 194);
                btnClear.Location = new Point(263, 194);
            }
            else if (updateCategory == "Receivables")
            {
                lblD1.Show();
                lblD2.Show();
                lblD3.Show();
                lblD4.Show();
                lblD5.Hide();
                txtD1.Show();
                txtD2.Show();
                txtD3.Show();
                txtD4.Hide();
                txtD5.Show();
                txtD5.Location = new Point(142, 157);
                btnUpdate.Location = new Point(142, 194);
                btnClear.Location = new Point(263, 194);
            }
            else if (updateCategory == "Payables")
            {
                lblD1.Show();
                lblD2.Show();
                lblD3.Show();
                lblD4.Show();
                lblD5.Hide();
                txtD1.Show();
                txtD2.Show();
                txtD3.Show();
                txtD4.Hide();
                txtD5.Show();
                txtD5.Location = new Point(142, 157);
                btnUpdate.Location = new Point(142, 194);
                btnClear.Location = new Point(263, 194);
            }
            else if (updateCategory == "Transactions")
            {
                lblD1.Show();
                lblD2.Show();
                lblD3.Show();
                lblD4.Show();
                lblD5.Hide();
                txtD1.Show();
                txtD2.Show();
                txtD3.Show();
                txtD4.Hide();
                txtD5.Show();
                txtD5.Location = new Point(142, 157);
                btnUpdate.Location = new Point(142, 194);
                btnClear.Location = new Point(263, 194);
            }
        }

        private void tblItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = tblItems.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tblItems.Rows[index];
            txtD1.Text = selectedRow.Cells["ItemCode"].Value.ToString();
            txtD2.Text = selectedRow.Cells["ItemName"].Value.ToString();
            //txtD3.Text = selectedRow.Cells["UnitPrice"].Value.ToString();
            //txtD4.Text = selectedRow.Cells["SellingPrice"].Value.ToString();
            txtD1.Enabled = false;
            txtD2.Enabled = false;
            txtD3.Enabled = true;
            txtD4.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Validation
            if (updateCategory == "Items")
            {
                if (txtD1.Text == "")
                    MessageBox.Show("Please double a cell in the table to update item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtD3.Text == "")
                    MessageBox.Show("Please enter new unit price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtD4.Text == "")
                    MessageBox.Show("Please enter new sell price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //ITEMS UPDATE
                    if (updateCategory == "Items")
                    {
                        conn = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(connectionString);
                        cmd.Connection = conn;
                        MySqlDataReader reader;
                        conn.Open();

                        cmd.CommandText = "SELECT * FROM items WHERE ItemCode = @ic";
                        MySqlParameter ic = new MySqlParameter("@ic", MySqlDbType.Int32);
                        ic.Value = int.Parse(txtD1.Text.ToString());
                        cmd.Parameters.Add(ic);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reader.Close();
                                cmd.CommandText = "UPDATE Items SET ItemSellPrice = @upsellprice , ItemPrice = @upitemprice WHERE ItemCode = @upitemcode";
                                MySqlParameter upItCode = new MySqlParameter("@upitemcode", MySqlDbType.Int32);
                                MySqlParameter upSellP = new MySqlParameter("@upsellprice", MySqlDbType.Decimal);
                                MySqlParameter upItPrice = new MySqlParameter("@upitemprice", MySqlDbType.Decimal);
                                upItCode.Value = int.Parse(txtD1.Text.ToString());
                                upSellP.Value = double.Parse(txtD4.Text.ToString());
                                upItPrice.Value = double.Parse(txtD3.Text.ToString());
                                cmd.Parameters.Add(upItCode);
                                cmd.Parameters.Add(upSellP);
                                cmd.Parameters.Add(upItPrice);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnItems_Click(this, null);
                            }
                            reader.Close();
                        }
                    }
                }
            }
            else if (updateCategory == "Receivables")
            {
                if (txtD1.Text == "")
                    MessageBox.Show("Please double a cell in the table to update item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtD5.SelectedIndex == -1)
                    MessageBox.Show("Please select an update operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //UPDATE SALES RECEIVABLES - paid
                    if (updateCategory == "Receivables" && txtD5.SelectedIndex == 0)
                    {
                        conn = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(connectionString);
                        cmd.Connection = conn;
                        MySqlDataReader reader;
                        conn.Open();

                        cmd.CommandText = "SELECT * FROM sales WHERE SalesId = @salesId";
                        cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && (reader["Status"].ToString() == "Unpaid" || reader["Status"].ToString() == "Shipping"))
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE sales SET Status = @status WHERE SalesId = @salesId";
                                cmd.Parameters.AddWithValue("@status", "Paid");
                                cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                //Receivables
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                cmd.Parameters.AddWithValue("@status", "Paid");
                                cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnReceivables_Click(this, null);
                            }
                            else if (reader["Status"].ToString() == "Returned")
                                MessageBox.Show("Item was returned.\nCannot change status.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show("Item status is already paid.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //UPDATE SALES RECEIVABLES - unpaid
                    else if (updateCategory == "Receivables" && txtD5.SelectedIndex == 1)
                    {
                        conn = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(connectionString);
                        cmd.Connection = conn;
                        MySqlDataReader reader;
                        conn.Open();

                        cmd.CommandText = "SELECT * FROM sales WHERE SalesId = @salesId";
                        cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["Status"].ToString() == "Paid")
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE sales SET Status = @status WHERE SalesId = @salesId";
                                cmd.Parameters.AddWithValue("@status", "Unpaid");
                                cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                //Receivables
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                cmd.Parameters.AddWithValue("@status", "Unpaid");
                                cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnReceivables_Click(this, null);
                            }
                            else if (reader["Status"].ToString() == "Returned")
                                MessageBox.Show("Item was returned.\nCannot change status.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show("Item status is already unpaid.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //UPDATE SALES STOCKS - return items
                    else if (updateCategory == "Receivables" && txtD5.SelectedIndex == 2)
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

                        double price = 0, totalAmount = 0, newTotalAmount = 0, stockQuantity = 0;
                        int quantity = 0, newQuantity = 0, itemCode = 0;
                        double unitMeasurementValue = 0;
                        int checkQuan;
                        if (int.TryParse(txtD4.Text, out checkQuan))
                        {
                            int returnQuantity = int.Parse(txtD4.Text);
                            string status = "";

                            cmd.CommandText = "SELECT * FROM sales WHERE SalesId = @salesId";
                            cmd.Parameters.AddWithValue("salesId", int.Parse(txtD1.Text));
                            using (reader = cmd.ExecuteReader())
                            {
                                if (reader.Read() && (reader["Status"].ToString() != "Returned" || reader["Status"].ToString() != "Shipping"))
                                {
                                    if (reader["UnitMeasurement"].ToString() == "1")
                                        unitMeasurementValue = 1;
                                    if (reader["UnitMeasurement"].ToString() == "1/2")
                                        unitMeasurementValue = 0.50;
                                    if (reader["UnitMeasurement"].ToString() == "1/4")
                                        unitMeasurementValue = 0.25;
                                    if (reader["UnitMeasurement"].ToString() == "1/8")
                                        unitMeasurementValue = 0.125;
                                    price = double.Parse(reader["ItemSellPrice"].ToString());
                                    quantity = int.Parse(reader["Quantity"].ToString());
                                    status = reader["Status"].ToString();
                                    reader.Close();
                                    newQuantity = quantity - returnQuantity;
                                    if (newQuantity < 0)
                                    {
                                        MessageBox.Show("Return quantity exceeds sold item quantity.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else if (newQuantity >= 0)
                                    {
                                        //Get Total Amount
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM transactions WHERE TransId = @transId";
                                        cmd2.Parameters.AddWithValue("transId", txtD3.Text);
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                totalAmount = double.Parse(reader2["TotalAmount"].ToString());
                                            }
                                            reader2.Close();
                                        }

                                        //Get Item Code
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM items WHERE ItemName = @name";
                                        cmd2.Parameters.AddWithValue("@name", txtD2.Text);
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                itemCode = int.Parse(reader2["ItemCode"].ToString());
                                            }
                                            reader2.Close();
                                        }

                                        //Get Stock Quantity
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itCode AND UnitMeasurement = @itUnit";
                                        cmd2.Parameters.AddWithValue("@itCode", itemCode);
                                        cmd2.Parameters.AddWithValue("@itUnit", "1");
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                stockQuantity = double.Parse(reader2["Quantity"].ToString());
                                            }
                                            reader2.Close();
                                        }
                                        stockQuantity += (returnQuantity * unitMeasurementValue);
                                        //Stocks
                                        for (int x = 0; x < availFraction.Length; x++)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandText = "UPDATE Stocks SET Quantity = @updateQuantity WHERE ItemCode = @updateICode AND UnitMeasurement = @updateUM";
                                            cmd.Parameters.AddWithValue("@updateQuantity", stockQuantity / availFractionValue[x]);
                                            cmd.Parameters.AddWithValue("@updateICode", itemCode);
                                            cmd.Parameters.AddWithValue("@updateUM", availFraction[x]);
                                            cmd.Prepare();
                                            cmd.ExecuteNonQuery();
                                        }
                                        //Transactions
                                        newTotalAmount = totalAmount - (price * returnQuantity);
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE transactions SET TotalAmount = @total WHERE TransId = @transId";
                                        cmd.Parameters.AddWithValue("@total", newTotalAmount);
                                        cmd.Parameters.AddWithValue("@transId", txtD3.Text);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        if (newQuantity == 0)
                                        {
                                            //Sales
                                            cmd.Parameters.Clear();
                                            cmd.CommandText = "UPDATE sales SET Status = @status, Quantity = @quantity WHERE SalesId = @salesId";
                                            cmd.Parameters.AddWithValue("@status", "Returned");
                                            cmd.Parameters.AddWithValue("@quantity", newQuantity);
                                            cmd.Parameters.AddWithValue("@salesId", txtD1.Text);
                                            cmd.Prepare();
                                            cmd.ExecuteNonQuery();

                                            if (status == "Receivable" || status == "COD")
                                            {
                                                //Receivables
                                                cmd.Parameters.Clear();
                                                cmd.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                                cmd.Parameters.AddWithValue("@status", "Returned");
                                                cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                                                cmd.Prepare();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            //Sales
                                            cmd.Parameters.Clear();
                                            cmd.CommandText = "UPDATE sales SET Quantity = @quantity WHERE SalesId = @salesId";
                                            cmd.Parameters.AddWithValue("@quantity", newQuantity);
                                            cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                                            cmd.Prepare();
                                            cmd.ExecuteNonQuery();
                                        }

                                        //SalesReturns
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "INSERT INTO salesreturns (ReturnQuantity, ReturnDate, SalesId) VALUES (@quan, (SELECT NOW()), @salesId)";
                                        cmd.Parameters.AddWithValue("@quan", returnQuantity);
                                        cmd.Parameters.AddWithValue("@salesId", txtD1.Text);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        btnReceivables_Click(this, null);
                                    }

                                }
                                else if (reader["Status"].ToString() == "Returned")
                                    MessageBox.Show("Item status is already returned.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else if (reader["Status"].ToString() == "Shipping")
                                    MessageBox.Show("Item status is currently shipping.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Only Whole Number.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtD4.Clear();
                        }
                    }

                    //COD - shipping
                    else if (updateCategory == "Receivables" && txtD5.SelectedIndex == 3)
                    {
                        conn = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(connectionString);
                        cmd.Connection = conn;
                        MySqlDataReader reader;
                        conn.Open();

                        cmd.CommandText = "SELECT * FROM sales WHERE SalesId = @salesId";
                        cmd.Parameters.AddWithValue("salesId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && (reader["Status"].ToString() != "Shipping" || reader["Status"].ToString() != "Paid"))
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE sales SET Status = @status WHERE SalesId = @salesId";
                                cmd.Parameters.AddWithValue("@status", "Shipping");
                                cmd.Parameters.AddWithValue("@salesId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                //Receivables
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                cmd.Parameters.AddWithValue("@status", "Shipping");
                                cmd.Parameters.AddWithValue("@salesId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnReceivables_Click(this, null);
                            }
                            else if (reader["Status"].ToString() == "Returned")
                                MessageBox.Show("Item was returned.\nCannot change status.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (reader["Status"].ToString() == "Shipping")
                                MessageBox.Show("Item status is already shipping.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (reader["Status"].ToString() == "Paid")
                                MessageBox.Show("Item status is paid, it was already delivered.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if (updateCategory == "Payables")
            {
                if (txtD1.Text == "")
                    MessageBox.Show("Please double a cell in the table to update item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtD5.SelectedIndex == -1)
                    MessageBox.Show("Please select an update operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //UPDATE PURCHASES PAYABLES - paid
                    if (updateCategory == "Payables" && txtD5.SelectedIndex == 0)
                    {
                        conn = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(connectionString);
                        cmd.Connection = conn;
                        MySqlDataReader reader;
                        conn.Open();

                        cmd.CommandText = "SELECT * FROM purchases WHERE PurchId = @purchId";
                        cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["Status"].ToString() == "Unpaid")
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE purchases SET Status = @status WHERE PurchId = @purchId";
                                cmd.Parameters.AddWithValue("@status", "Paid");
                                cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                //Payables
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE payables SET Status = @status WHERE PurchId = @purchId";
                                cmd.Parameters.AddWithValue("@status", "Paid");
                                cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnPayables_Click(this, null);
                            }
                            else if (reader["Status"].ToString() == "Returned")
                                MessageBox.Show("Item was returned.\nCannot change status.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show("Item status is already paid.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //UPDATE PURCHASES PAYABLES - unpaid
                    else if (updateCategory == "Payables" && txtD5.SelectedIndex == 1)
                    {
                        conn = new MySqlConnection(connectionString);
                        MySqlCommand cmd = new MySqlCommand(connectionString);
                        cmd.Connection = conn;
                        MySqlDataReader reader;
                        conn.Open();

                        cmd.CommandText = "SELECT * FROM purchases WHERE PurchId = @purchId";
                        cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["Status"].ToString() == "Paid")
                            {
                                reader.Close();
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE purchases SET Status = @status WHERE PurchId = @purchId";
                                cmd.Parameters.AddWithValue("@status", "Unpaid");
                                cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                //Payables
                                cmd.Parameters.Clear();
                                cmd.CommandText = "UPDATE payables SET Status = @status WHERE PurchId = @purchId";
                                cmd.Parameters.AddWithValue("@status", "Unpaid");
                                cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnPayables_Click(this, null);
                            }
                            else if (reader["Status"].ToString() == "Returned")
                                MessageBox.Show("Item was returned.\nCannot change status.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show("Item status is already unpaid.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //UPDATE PURCHASES STOCKS - return items
                    else if (updateCategory == "Payables" && txtD5.SelectedIndex == 2)
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

                        double price = 0, totalAmount = 0, newTotalAmount = 0, stockQuantity = 0;
                        int quantity = 0, newQuantity = 0, itemCode = 0;
                        int checkQuan;
                        if (int.TryParse(txtD4.Text, out checkQuan))
                        {
                            
                            int returnQuantity = int.Parse(txtD4.Text);
                            string status = "";

                            cmd.CommandText = "SELECT * FROM purchases WHERE PurchId = @purchId";
                            cmd.Parameters.AddWithValue("purchId", int.Parse(txtD1.Text));
                            using (reader = cmd.ExecuteReader())
                            {
                                if (reader.Read() && reader["Status"].ToString() != "Returned")
                                {
                                    price = double.Parse(reader["ItemPrice"].ToString());
                                    quantity = int.Parse(reader["Quantity"].ToString());
                                    status = reader["Status"].ToString();
                                    reader.Close();
                                    newQuantity = quantity - returnQuantity;
                                    if (newQuantity < 0)
                                    {
                                        MessageBox.Show("Return quantity exceeds purchased item quantity.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else if (newQuantity >= 0)
                                    {
                                        //Get Total Amount
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM transactions WHERE TransId = @transId";
                                        cmd2.Parameters.AddWithValue("transId", txtD3.Text);
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                totalAmount = double.Parse(reader2["TotalAmount"].ToString());
                                            }
                                            reader2.Close();
                                        }

                                        //Get Item Code
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM items WHERE ItemName = @name";
                                        cmd2.Parameters.AddWithValue("name", txtD2.Text);
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                itemCode = int.Parse(reader2["ItemCode"].ToString());
                                            }
                                            reader2.Close();
                                        }

                                        //Get Stock Quantity
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itCode AND UnitMeasurement = @itUnit";
                                        cmd2.Parameters.AddWithValue("@itCode", itemCode);
                                        cmd2.Parameters.AddWithValue("@itUnit", "1");
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                stockQuantity = double.Parse(reader2["Quantity"].ToString());
                                            }
                                            reader2.Close();
                                        }
                                        stockQuantity -= returnQuantity;
                                        //Stocks
                                        for (int x = 0; x < availFraction.Length; x++)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.CommandText = "UPDATE Stocks SET Quantity = @updateQuantity WHERE ItemCode = @updateICode AND UnitMeasurement = @updateUM";
                                            cmd.Parameters.AddWithValue("@updateQuantity", stockQuantity / availFractionValue[x]);
                                            cmd.Parameters.AddWithValue("@updateICode", itemCode);
                                            cmd.Parameters.AddWithValue("@updateUM", availFraction[x]);
                                            cmd.Prepare();
                                            cmd.ExecuteNonQuery();
                                        }

                                        //Transactions
                                        newTotalAmount = totalAmount - (price * returnQuantity);
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE transactions SET TotalAmount = @total WHERE TransId = @transId";
                                        cmd.Parameters.AddWithValue("@total", newTotalAmount);
                                        cmd.Parameters.AddWithValue("@transId", txtD3.Text);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        if (newQuantity == 0)
                                        {
                                            //Purchases
                                            cmd.Parameters.Clear();
                                            cmd.CommandText = "UPDATE purchases SET Status = @status, Quantity = @quantity WHERE PurchId = @purchId";
                                            cmd.Parameters.AddWithValue("@status", "Returned");
                                            cmd.Parameters.AddWithValue("@quantity", newQuantity);
                                            cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                                            cmd.Prepare();
                                            cmd.ExecuteNonQuery();

                                            if (status == "Payable")
                                            {
                                                //Payables
                                                cmd.Parameters.Clear();
                                                cmd.CommandText = "UPDATE payables SET Status = @status WHERE PurchId = @purchId";
                                                cmd.Parameters.AddWithValue("@status", "Returned");
                                                cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                                                cmd.Prepare();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            //Purchases
                                            cmd.Parameters.Clear();
                                            cmd.CommandText = "UPDATE purchases SET Quantity = @quantity WHERE PurchId = @purchId";
                                            cmd.Parameters.AddWithValue("@quantity", newQuantity);
                                            cmd.Parameters.AddWithValue("purchId", txtD1.Text);
                                            cmd.Prepare();
                                            cmd.ExecuteNonQuery();
                                        }

                                        //PurchasesReturns
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "INSERT INTO purchasesreturns (ReturnQuantity, ReturnDate, PurchId) VALUES (@quan, (SELECT NOW()), @purchId)";
                                        cmd.Parameters.AddWithValue("@quan", returnQuantity);
                                        cmd.Parameters.AddWithValue("@purchId", txtD1.Text);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        btnPayables_Click(this, null);
                                    }

                                }
                                else
                                    MessageBox.Show("Item status is already returned.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Only Whole Number.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtD4.Clear();
                        }
                    }
                }
            }
            else if (updateCategory == "Transactions")
            {
                if (txtD1.Text == "")
                    MessageBox.Show("Please double a cell in the table to update item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtD5.SelectedIndex == -1)
                    MessageBox.Show("Please select an update operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                //UPDATE TRANSACTION - paid
                if (updateCategory == "Transactions" && txtD5.SelectedIndex == 0)
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

                        int salesID = 0, purchID = 0;

                        cmd.CommandText = "SELECT * FROM transactions WHERE TransId = @transId";
                        cmd.Parameters.AddWithValue("transId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["TransType"].ToString() == "Sales")
                            {
                                reader.Close();
                                cmd2.Parameters.Clear();
                                cmd2.CommandText = "SELECT * FROM sales WHERE TransId = @transId";
                                cmd2.Parameters.AddWithValue("transId", txtD1.Text);
                                using (reader2 = cmd2.ExecuteReader())
                                {
                                    //if (reader2.Read() && reader2["Status"].ToString() == "Paid")
                                    //{
                                    //    MessageBox.Show("Item status is already paid.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //}
                                    while (reader2.Read() && (reader2["Status"].ToString() == "Unpaid" || reader2["Status"].ToString() == "Shipping"))
                                    {
                                        salesID = int.Parse(reader2["SalesId"].ToString());
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE sales SET Status = @status WHERE SalesId = @salesId";
                                        cmd.Parameters.AddWithValue("@status", "Paid");
                                        cmd.Parameters.AddWithValue("salesId", salesID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        //Receivables
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                        cmd.Parameters.AddWithValue("@status", "Paid");
                                        cmd.Parameters.AddWithValue("salesId", salesID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnTransactions_Click(this, null);
                            }
                            else
                            {
                                reader.Close();
                                cmd2.Parameters.Clear();
                                cmd2.CommandText = "SELECT * FROM purchases WHERE TransId = @transId";
                                cmd2.Parameters.AddWithValue("transId", txtD1.Text);
                                using (reader2 = cmd2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        purchID = int.Parse(reader2["PurchID"].ToString());
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE purchases SET Status = @status WHERE PurchId = @purchId";
                                        cmd.Parameters.AddWithValue("@status", "Paid");
                                        cmd.Parameters.AddWithValue("purchId", purchID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        //Payables
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE payables SET Status = @status WHERE PurchId = @purchId";
                                        cmd.Parameters.AddWithValue("@status", "Paid");
                                        cmd.Parameters.AddWithValue("purchId", purchID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnTransactions_Click(this, null);
                            }
                        }
                    }

                    //UPDATE TRANSACTION - unpaid
                    else if (updateCategory == "Transactions" && txtD5.SelectedIndex == 1)
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

                        int salesID = 0, purchID = 0;

                        cmd.CommandText = "SELECT * FROM transactions WHERE TransId = @transId";
                        cmd.Parameters.AddWithValue("transId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["TransType"].ToString() == "Sales")
                            {
                                reader.Close();
                                cmd2.Parameters.Clear();
                                cmd2.CommandText = "SELECT * FROM sales WHERE TransId = @transId";
                                cmd2.Parameters.AddWithValue("transId", txtD1.Text);
                                using (reader2 = cmd2.ExecuteReader())
                                {
                                    //if (reader2.Read() && reader2["Status"].ToString() == "Unpaid")
                                    //{
                                    //    MessageBox.Show("Item status is already unpaid.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //}
                                    while (reader2.Read())
                                    {
                                        salesID = int.Parse(reader2["SalesId"].ToString());
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE sales SET Status = @status WHERE SalesId = @salesId";
                                        cmd.Parameters.AddWithValue("@status", "Unpaid");
                                        cmd.Parameters.AddWithValue("salesId", salesID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        //Receivables
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                        cmd.Parameters.AddWithValue("@status", "Unpaid");
                                        cmd.Parameters.AddWithValue("salesId", salesID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnTransactions_Click(this, null);
                            }
                            else
                            {
                                reader.Close();
                                cmd2.Parameters.Clear();
                                cmd2.CommandText = "SELECT * FROM purchases WHERE TransId = @transId";
                                cmd2.Parameters.AddWithValue("transId", txtD1.Text);
                                using (reader2 = cmd2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        purchID = int.Parse(reader2["PurchID"].ToString());
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE purchases SET Status = @status WHERE PurchId = @purchId";
                                        cmd.Parameters.AddWithValue("@status", "Unpaid");
                                        cmd.Parameters.AddWithValue("purchId", purchID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        //Payables
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE payables SET Status = @status WHERE PurchId = @purchId";
                                        cmd.Parameters.AddWithValue("@status", "Unpaid");
                                        cmd.Parameters.AddWithValue("purchId", purchID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnTransactions_Click(this, null);
                            }
                        }
                    }

                    //UPDATE TRANSACTION - return all
                    else if (updateCategory == "Transactions" && txtD5.SelectedIndex == 2)
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

                        int salesID = 0, purchID = 0;

                        cmd.CommandText = "SELECT * FROM transactions WHERE TransId = @transId";
                        cmd.Parameters.AddWithValue("transId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["TransType"].ToString() == "Sales")
                            {
                                reader.Close();

                                double stockQuantity = 0;
                                int quantity = 0, itemCode = 0; ;
                                double unitMeasurementValue = 0;
                                string status = "";

                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT * FROM sales WHERE TransId = @transId";
                                cmd.Parameters.AddWithValue("transId", int.Parse(txtD1.Text));
                                using (reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read() && reader["Status"].ToString() != "Returned")
                                    {
                                        salesID = int.Parse(reader["SalesId"].ToString());
                                        quantity = int.Parse(reader["Quantity"].ToString());
                                        itemCode = int.Parse(reader["ItemCode"].ToString());
                                        status = reader["Status"].ToString();
                                        if (reader["UnitMeasurement"].ToString() == "1")
                                            unitMeasurementValue = 1;
                                        if (reader["UnitMeasurement"].ToString() == "1/2")
                                            unitMeasurementValue = 0.50;
                                        if (reader["UnitMeasurement"].ToString() == "1/4")
                                            unitMeasurementValue = 0.25;
                                        if (reader["UnitMeasurement"].ToString() == "1/8")
                                            unitMeasurementValue = 0.125;
                                        //Get Stock Quantity
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itCode AND UnitMeasurement = @itUnit";                        
                                        cmd2.Parameters.AddWithValue("itCode", itemCode);
                                        cmd2.Parameters.AddWithValue("itUnit", "1");
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                stockQuantity = double.Parse(reader2["Quantity"].ToString());
                                            }
                                            reader2.Close();
                                        }
                                        stockQuantity += (quantity * unitMeasurementValue);
                                        //Stocks
                                        for (int x = 0; x < availFraction.Length; x++)
                                        {
                                            cmd2.Parameters.Clear();
                                            cmd2.CommandText = "UPDATE Stocks SET Quantity = @updateQuantity WHERE ItemCode = @updateICode AND UnitMeasurement = @updateUM";
                                            cmd2.Parameters.AddWithValue("@updateQuantity", stockQuantity / availFractionValue[x]);
                                            cmd2.Parameters.AddWithValue("@updateICode", itemCode);
                                            cmd2.Parameters.AddWithValue("@updateUM", availFraction[x]);
                                            cmd2.Prepare();
                                            cmd2.ExecuteNonQuery();
                                        }

                                        //Transactions
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "UPDATE transactions SET TotalAmount = @total WHERE TransId = @transId";
                                        cmd2.Parameters.AddWithValue("@total", 0);
                                        cmd2.Parameters.AddWithValue("@transId", txtD1.Text);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();

                                        //Sales
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "UPDATE sales SET Status = @status, Quantity = @quantity WHERE SalesId = @salesId";
                                        cmd2.Parameters.AddWithValue("@status", "Returned");
                                        cmd2.Parameters.AddWithValue("@quantity", 0);
                                        cmd2.Parameters.AddWithValue("salesId", salesID);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();

                                        if (status == "Receivable" || status == "COD")
                                        {
                                            //Receivables
                                            cmd2.Parameters.Clear();
                                            cmd2.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                            cmd2.Parameters.AddWithValue("@status", "Returned");
                                            cmd2.Parameters.AddWithValue("salesId", salesID);
                                            cmd2.Prepare();
                                            cmd2.ExecuteNonQuery();
                                        }

                                        //SalesReturns
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "INSERT INTO salesreturns (ReturnQuantity, ReturnDate, SalesId) VALUES (@quan, (SELECT NOW()), @salesId)";
                                        cmd2.Parameters.AddWithValue("@quan", quantity);
                                        cmd2.Parameters.AddWithValue("@salesId", salesID);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();
                                    }
                                    reader.Close();
                                    MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnTransactions_Click(this, null);
                                }
                            }

                            else
                            {
                                reader.Close();

                                double stockQuantity = 0;
                                int quantity = 0, itemCode = 0; ;
                                string status = "";

                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT * FROM Purchases WHERE TransId = @transId";
                                cmd.Parameters.AddWithValue("transId", int.Parse(txtD1.Text));
                                using (reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        purchID = int.Parse(reader["PurchId"].ToString());
                                        quantity = int.Parse(reader["Quantity"].ToString());
                                        itemCode = int.Parse(reader["ItemCode"].ToString());
                                        status = reader["Status"].ToString();

                                        //Get Stock Quantity
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itCode";
                                        cmd2.Parameters.AddWithValue("itCode", itemCode);
                                        using (reader2 = cmd2.ExecuteReader())
                                        {
                                            while (reader2.Read())
                                            {
                                                stockQuantity = double.Parse(reader2["Quantity"].ToString());
                                            }
                                            reader2.Close();
                                        }

                                        //Stocks
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "UPDATE Stocks SET Quantity = @updateQuantity WHERE ItemCode = @updateICode";
                                        cmd2.Parameters.AddWithValue("@updateQuantity", stockQuantity - quantity);
                                        cmd2.Parameters.AddWithValue("@updateICode", itemCode);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();

                                        //Transactions
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "UPDATE transactions SET TotalAmount = @total WHERE TransId = @transId";
                                        cmd2.Parameters.AddWithValue("@total", 0);
                                        cmd2.Parameters.AddWithValue("@transId", txtD1.Text);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();

                                        //Purchases
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "UPDATE purchases SET Status = @status, Quantity = @quantity WHERE PurchId = @purchId";
                                        cmd2.Parameters.AddWithValue("@status", "Returned");
                                        cmd2.Parameters.AddWithValue("@quantity", 0);
                                        cmd2.Parameters.AddWithValue("purchId", purchID);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();

                                        if (status == "Payables")
                                        {
                                            //Payables
                                            cmd2.Parameters.Clear();
                                            cmd2.CommandText = "UPDATE payables SET Status = @status WHERE PurchId = @purchId";
                                            cmd2.Parameters.AddWithValue("@status", "Returned");
                                            cmd2.Parameters.AddWithValue("purchId", purchID);
                                            cmd2.Prepare();
                                            cmd2.ExecuteNonQuery();
                                        }

                                        //PurchasesReturns
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "INSERT INTO purchasesreturns (ReturnQuantity, ReturnDate, PurchId) VALUES (@quan, (SELECT NOW()), @purchId)";
                                        cmd2.Parameters.AddWithValue("@quan", quantity);
                                        cmd2.Parameters.AddWithValue("@purchId", purchID);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();
                                    }
                                    reader.Close();
                                    MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnTransactions_Click(this, null);
                                }

                            }

                        }
                    }

                    //UPDATE TRANSACTION - Shipping
                    else if (updateCategory == "Transactions" && txtD5.SelectedIndex == 3)
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

                        int salesID = 0;

                        cmd.CommandText = "SELECT * FROM transactions WHERE TransId = @transId";
                        cmd.Parameters.AddWithValue("transId", txtD1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader["TransType"].ToString() == "Sales")
                            {
                                reader.Close();
                                cmd2.Parameters.Clear();
                                cmd2.CommandText = "SELECT * FROM sales WHERE TransId = @transId";
                                cmd2.Parameters.AddWithValue("transId", txtD1.Text);
                                using (reader2 = cmd2.ExecuteReader())
                                {
                                    while (reader2.Read() && reader2["Status"].ToString() == "Unpaid")
                                    {
                                        salesID = int.Parse(reader2["SalesId"].ToString());
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE sales SET Status = @status WHERE SalesId = @salesId";
                                        cmd.Parameters.AddWithValue("@status", "Shipping");
                                        cmd.Parameters.AddWithValue("salesId", salesID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();

                                        //Receivables
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "UPDATE receivables SET Status = @status WHERE SalesId = @salesId";
                                        cmd.Parameters.AddWithValue("@status", "Shipping");
                                        cmd.Parameters.AddWithValue("salesId", salesID);
                                        cmd.Prepare();
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (reader2["Status"].ToString() == "Returned")
                                        MessageBox.Show("Item was returned.\nCannot change status.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    else if (reader2["Status"].ToString() == "Shipping")
                                        MessageBox.Show("Item status is already shipping.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    else if (reader2["Status"].ToString() == "Paid")
                                        MessageBox.Show("Item status is paid, it was already delivered.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                MessageBox.Show("Successfully updated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnTransactions_Click(this, null);
                            }
                            else
                            {
                                MessageBox.Show("Cannot change status to \"Shipping\" for purchases.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

        }

        private void Clear()
        {
            txtD5.SelectedIndex = -1;
            txtD1.Clear();
            txtD2.Clear();
            txtD3.Clear();
            txtD4.Clear();
            txtD1.Enabled = false;
            txtD2.Enabled = false;
            txtD3.Enabled = false;
            txtD4.Enabled = false;
            label1.Hide();
            cbxD6.SelectedIndex = -1;
        }

        private void tblPurchases_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = tblPurchases.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tblPurchases.Rows[index];
            txtD1.Text = selectedRow.Cells["PurchaseID"].Value.ToString();
            txtD2.Text = selectedRow.Cells["ItemCodeP"].Value.ToString();
            txtD3.Text = selectedRow.Cells["TransactionID"].Value.ToString();
            txtD1.Enabled = false;
            txtD2.Enabled = false;
            txtD3.Enabled = false;
        }

        private void tblSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = tblSales.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tblSales.Rows[index];
            txtD1.Text = selectedRow.Cells["SalesID"].Value.ToString();
            txtD2.Text = selectedRow.Cells["ItemCodes"].Value.ToString();
            txtD3.Text = selectedRow.Cells["TransactionIDS"].Value.ToString();
            txtD1.Enabled = false;
            txtD2.Enabled = false;
            txtD3.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void tblTransactions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = tblTransactions.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tblTransactions.Rows[index];
            txtD1.Text = selectedRow.Cells["TransID"].Value.ToString();
            txtD2.Text = selectedRow.Cells["TotalAmount"].Value.ToString();
            txtD3.Text = selectedRow.Cells["TransactionType"].Value.ToString();
            txtD1.Enabled = false;
            txtD2.Enabled = false;
            txtD3.Enabled = false;
        }

        private void btnUpdate_MouseEnter(object sender, EventArgs e)
        {
            btnUpdate.BackColor = Color.SeaGreen;
            btnUpdate.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnUpdate.ForeColor = Color.White;
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            btnUpdate.BackColor = Color.FromArgb(229, 245, 242);
            btnUpdate.FlatAppearance.BorderColor = Color.SeaGreen;
            btnUpdate.ForeColor = Color.Black;
        }

        private void btnClear_MouseEnter(object sender, EventArgs e)
        {
            btnClear.BackColor = Color.SeaGreen;
            btnClear.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnClear.ForeColor = Color.White;
        }

        private void btnClear_MouseLeave(object sender, EventArgs e)
        {
            btnClear.BackColor = Color.FromArgb(229, 245, 242);
            btnClear.FlatAppearance.BorderColor = Color.SeaGreen;
            btnClear.ForeColor = Color.Black;
        }

        private void txtD5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((updateCategory == "Receivables" || updateCategory == "Payables") && txtD5.SelectedIndex == 2)
            {
                txtD4.Visible = true;
                txtD4.Enabled = true;
                txtD4.Location = new Point(142, 189);
                lblD5.Visible = true;
                lblD5.Text = "Qty. Returned :";
                btnUpdate.Location = new Point(142, 224);
                btnClear.Location = new Point(262, 224);
            }
        }

        private void cbxD6_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxD6.SelectedIndex == 0 && updateCategory == "Receivables")
            {
                tblSales.Rows.Clear();
                tblSales.Show();
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
                cmd.CommandText = "SELECT * FROM Sales";
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader2["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            else if (cbxD6.SelectedIndex == 1 && updateCategory == "Receivables")
            {
                tblSales.Rows.Clear();
                tblSales.Show();
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
                cmd.CommandText = "SELECT * FROM Sales WHERE Status = @status";
                cmd.Parameters.AddWithValue("status", "Paid");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader2["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            else if (cbxD6.SelectedIndex == 2 && updateCategory == "Receivables")
            {
                tblSales.Rows.Clear();
                tblSales.Show();
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
                cmd.CommandText = "SELECT * FROM Sales WHERE Status = @status";
                cmd.Parameters.AddWithValue("status", "Unpaid");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader2["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            if (cbxD6.SelectedIndex == 3 && updateCategory == "Receivables")
            {
                tblSales.Rows.Clear();
                tblSales.Show();
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
                cmd.CommandText = "SELECT * FROM Sales WHERE Status = @status";
                cmd.Parameters.AddWithValue("status", "Returned");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader2["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            if (cbxD6.SelectedIndex == 4 && updateCategory == "Receivables")
            {
                tblSales.Rows.Clear();
                tblSales.Show();
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
                cmd.CommandText = "SELECT * FROM Sales WHERE PaymentType = @type";
                cmd.Parameters.AddWithValue("type", "COD");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader2["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            if (cbxD6.SelectedIndex == 5 && updateCategory == "Receivables")
            {
                tblSales.Rows.Clear();
                tblSales.Show();
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
                cmd.CommandText = "SELECT * FROM Sales WHERE Status = @status";
                cmd.Parameters.AddWithValue("status", "Shipping");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblSales.Rows.Add(reader["SalesId"], reader2["ItemName"], reader2["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["SalesInvoiceNumber"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            else if (cbxD6.SelectedIndex == 0 && updateCategory == "Payables")
            {
                tblPurchases.Rows.Clear();
                tblPurchases.Show();
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
                cmd.CommandText = "SELECT * FROM Purchases";
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblPurchases.Rows.Add(reader["PurchId"], reader2["ItemName"], reader2["ItemPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            else if (cbxD6.SelectedIndex == 1 && updateCategory == "Payables")
            {
                tblPurchases.Rows.Clear();
                tblPurchases.Show();
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
                cmd.CommandText = "SELECT * FROM Purchases WHERE Status = @status";
                cmd.Parameters.AddWithValue("status", "Paid");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblPurchases.Rows.Add(reader["PurchId"], reader2["ItemName"], reader2["ItemPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            else if (cbxD6.SelectedIndex == 2 && updateCategory == "Payables")
            {
                tblPurchases.Rows.Clear();
                tblPurchases.Show();
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
                cmd.CommandText = "SELECT * FROM Purchases WHERE Status = @status";
                cmd.Parameters.AddWithValue("status", "Unpaid");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblPurchases.Rows.Add(reader["PurchId"], reader2["ItemName"], reader2["ItemPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }

            else if (cbxD6.SelectedIndex == 3 && updateCategory == "Payables")
            {
                tblPurchases.Rows.Clear();
                tblPurchases.Show();
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
                cmd.CommandText = "SELECT * FROM Purchases WHERE Status = @status";
                cmd.Parameters.AddWithValue("status", "Returned");
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                        MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCParam.Value = int.Parse(reader["ItemCode"].ToString());
                        cmd2.Parameters.Add(itemCParam);
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                tblPurchases.Rows.Add(reader["PurchId"], reader2["ItemName"], reader2["ItemPrice"], reader["Quantity"], reader["TransId"], reader["PaymentType"], reader["Status"]);
                                reader2.Close();
                            }
                        }
                    }
                    reader.Close();
                }
            }
        }

        private void txtD3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), "[^0-9\b,., ]"))
            {
                MessageBox.Show("Please enter only numbers.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtD4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), "[^0-9\b,., ]"))
            {
                MessageBox.Show("Please enter only numbers.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtD3_TextChanged(object sender, EventArgs e)
        {
            if (txtD3.Text != "")
            {
                double checkamount;
                if (double.TryParse(txtD3.Text,out checkamount))
                { }
                else
                {
                    MessageBox.Show("Number of period (.) exceeded.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtD3.Clear();
                }
            } 
        }

        private void txtD4_TextChanged(object sender, EventArgs e)
        {
            if (txtD4.Text != "")
            {
                double checkamount;
                if (double.TryParse(txtD4.Text, out checkamount))
                { }
                else
                {
                    MessageBox.Show("Number of period (.) exceeded.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtD4.Clear();
                }
            }
        }
    }
}
