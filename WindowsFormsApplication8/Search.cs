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
    public partial class Search : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        string searchCategory = "";

        public Search()
        {
            InitializeComponent();
            InitializeComboBox();
            StartPosition = FormStartPosition.CenterScreen;
            SetForm();
        }

        private void InitializeComboBox()
        {
            this.cbxSearchBy.SelectedIndexChanged += new System.EventHandler(ComboBox2_SelectedIndexChanged);
        }

        private void button1_Click(object sender, EventArgs e)
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

            //ITEMS - selected
            if (searchCategory == "Items")
            {
                if (cbxSearchBy.Text == "Item Code")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView1);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Show();
                        cmd.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itemcode";
                        cmd.Parameters.AddWithValue("@itemcode", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode = @itemcode";
                                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                                    itemCParam.Value = int.Parse(textBox1.Text);
                                    cmd2.Parameters.Add(itemCParam);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(reader2["ItemCode"], reader2["ItemName"], reader2["ItemPrice"], reader2["ItemSellPrice"], reader["Quantity"]);
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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Item Name")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView1);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Show();
                        int quantity;
                        cmd.CommandText = "SELECT * FROM items WHERE ItemName like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@itemname", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    quantity = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode like '%" + quantity + "%'";
                                    cmd2.Parameters.AddWithValue("@quantity", quantity);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"], reader2["Quantity"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Item Price")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView1);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Show();
                        int quantity;
                        cmd.CommandText = "SELECT * FROM items WHERE ItemPrice = @itemprice";
                        cmd.Parameters.AddWithValue("@itemprice", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    quantity = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode like '%" + quantity + "%'";
                                    cmd2.Parameters.AddWithValue("@quantity", quantity);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"], reader2["Quantity"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Item SellPrice")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView1);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Show();
                        int quantity;
                        cmd.CommandText = "SELECT * FROM items WHERE ItemSellPrice = @itemsellprice";
                        cmd.Parameters.AddWithValue("@itemsellprice", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    quantity = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode like '%" + quantity + "%'";
                                    cmd2.Parameters.AddWithValue("@quantity", quantity);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"], reader2["Quantity"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else
                    MessageBox.Show("Please select choice for search by.");
            }

            //PURCHASES - selected
            else if (searchCategory == "Purchases")
            {
                if (cbxSearchBy.Text == "Item Code")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM purchases WHERE ItemCode = @itemcode ";
                        cmd.Parameters.AddWithValue("@itemcode", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Item Name")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemcode;
                        cmd.CommandText = "SELECT * FROM items WHERE ItemName like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@itemname", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemcode = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM purchases WHERE ItemCode like '%" + itemcode + "%'";
                                    cmd2.Parameters.AddWithValue("@itemcode", itemcode);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);
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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Purchase ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM purchases WHERE PurchId = @purchid";
                        cmd.Parameters.AddWithValue("@purchid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Transaction ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM purchases WHERE TransId = @transid";
                        cmd.Parameters.AddWithValue("@transid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Supplier ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM purchases WHERE SupplierId = @suplid";
                        cmd.Parameters.AddWithValue("@suplid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Payment Type")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM purchases WHERE PaymentType like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@paytype", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Applicable Discount")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM purchases WHERE ApplicableDiscount = @discount";
                        cmd.Parameters.AddWithValue("@discount", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Status")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView2);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM purchases WHERE Status = @status";
                        cmd.Parameters.AddWithValue("@status", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else
                    MessageBox.Show("Please select choice for search by.");
            }

            //SALES - selected
            else if (searchCategory == "Sales")
            {
                if (cbxSearchBy.Text == "Item Code")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE ItemCode = @itemcode";
                        cmd.Parameters.AddWithValue("@itemcode", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Item Name")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemcode;
                        cmd.CommandText = "SELECT * FROM items WHERE ItemName like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@itemname", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemcode = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM sales WHERE ItemCode = @itemcode";
                                    cmd2.Parameters.AddWithValue("@itemcode", itemcode);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);
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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Sales ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE SalesId = @purchid";
                        cmd.Parameters.AddWithValue("@purchid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Transaction ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE TransId = @transid";
                        cmd.Parameters.AddWithValue("@transid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Customer ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE CustomerId = @custid";
                        cmd.Parameters.AddWithValue("@custid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Payment Type")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE PaymentType like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@payment", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Applicable Discount")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE ApplicableDiscount = @discount";
                        cmd.Parameters.AddWithValue("@discount", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Sales Invoice No.")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE SalesInvoiceNumber = @invoice";
                        cmd.Parameters.AddWithValue("@invoice", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Employee ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE EmployeeId = @empId";
                        cmd.Parameters.AddWithValue("@empId", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items " + "WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Status")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView3);
                        dataGridView3.Rows.Clear();
                        dataGridView3.Show();
                        int itemname;
                        cmd.CommandText = "SELECT * FROM sales WHERE Status = @status";
                        cmd.Parameters.AddWithValue("@status", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    cmd2.Parameters.Clear();
                                    itemname = int.Parse(reader["ItemCode"].ToString());
                                    cmd2.CommandText = "SELECT * FROM items WHERE ItemCode like '%" + itemname + "%'";
                                    cmd2.Parameters.AddWithValue("@itemname", itemname);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);

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
                        MessageBox.Show("Please enter value in the text box.");
                }

                else
                    MessageBox.Show("Please select choice for search by.");
            }

            //TRANSACTIONS - selected
            else if (searchCategory == "Transactions")
            {
                if (cbxSearchBy.Text == "Transaction Type")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView4);
                        dataGridView4.Rows.Clear();
                        dataGridView4.Show();
                        cmd.CommandText = "SELECT * FROM transactions WHERE TransType like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@transtype", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView4.Rows.Add(reader["TransId"], reader["TotalAmount"], reader["TransType"], reader["Date"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Transaction ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView4);
                        dataGridView4.Rows.Clear();
                        dataGridView4.Show();
                        cmd.CommandText = "SELECT * FROM transactions WHERE TransId @transid";
                        cmd.Parameters.AddWithValue("@transid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView4.Rows.Add(reader["TransId"], reader["TotalAmount"], reader["TransType"], reader["Date"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Date")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView4);
                        dataGridView4.Rows.Clear();
                        dataGridView4.Show();
                        cmd.CommandText = "SELECT * FROM transactions WHERE Date like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@date", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView4.Rows.Add(reader["TransId"], reader["TotalAmount"], reader["TransType"], reader["Date"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

            }


            //PAYABLES- selected
            else if (searchCategory == "Payables")
            {
                if (cbxSearchBy.Text == "Payable ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView5);
                        dataGridView5.Rows.Clear();
                        dataGridView5.Show();
                        cmd.CommandText = "SELECT * FROM payables WHERE PayableId = @payableId";
                        cmd.Parameters.AddWithValue("@payableId", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView5.Rows.Add(reader["PayableId"], reader["PurchId"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Purchase ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView5);
                        dataGridView5.Rows.Clear();
                        dataGridView5.Show();
                        cmd.CommandText = "SELECT * FROM payables WHERE PurchId = @purchid";
                        cmd.Parameters.AddWithValue("@purchid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView5.Rows.Add(reader["PayableId"], reader["PurchId"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Status")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView5);
                        dataGridView5.Rows.Clear();
                        dataGridView5.Show();
                        cmd.CommandText = "SELECT * FROM payables WHERE Status = @status";
                        cmd.Parameters.AddWithValue("@status", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView5.Rows.Add(reader["PayableId"], reader["PurchId"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

            }


            //RECEIVABLES- selected
            else if (searchCategory == "Receivables")
            {
                if (cbxSearchBy.Text == "Receivable ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView6);
                        dataGridView6.Rows.Clear();
                        dataGridView6.Show();
                        cmd.CommandText = "SELECT * FROM receivables WHERE ReceivableId = @receivableId";
                        cmd.Parameters.AddWithValue("@receivableId", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView6.Rows.Add(reader["ReceivableId"], reader["AdditionalFee"], reader["SalesId"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Sales ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView6);
                        dataGridView6.Rows.Clear();
                        dataGridView6.Show();
                        cmd.CommandText = "SELECT * FROM receivables WHERE SalesId = @salesid";
                        cmd.Parameters.AddWithValue("@salesid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView6.Rows.Add(reader["ReceivableId"], reader["AdditionalFee"], reader["SalesId"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Status")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView6);
                        dataGridView6.Rows.Clear();
                        dataGridView6.Show();
                        cmd.CommandText = "SELECT * FROM receivables WHERE Status = @status";
                        cmd.Parameters.AddWithValue("@status", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView6.Rows.Add(reader["ReceivableId"], reader["AdditionalFee"], reader["SalesId"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

            }
            //STOCKS - selected
            else if (searchCategory == "Stocks")
            {
                if (cbxSearchBy.Text == "Stock ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView7);
                        dataGridView7.Rows.Clear();
                        dataGridView7.Show();
                        cmd.CommandText = "SELECT * FROM stocks WHERE StockId = @stockid";
                        cmd.Parameters.AddWithValue("@stockid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView7.Rows.Add(reader["StockId"], reader["ItemCode"], reader["Quantity"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Item Code")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView7);
                        dataGridView7.Rows.Clear();
                        dataGridView7.Show();
                        cmd.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itemcode";
                        cmd.Parameters.AddWithValue("@itemcode", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView7.Rows.Add(reader["StockId"], reader["ItemCode"], reader["Quantity"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Quantity")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView7);
                        dataGridView7.Rows.Clear();
                        dataGridView7.Show();
                        cmd.CommandText = "SELECT * FROM stocks WHERE Quantity = @quantity";
                        cmd.Parameters.AddWithValue("@quantity", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView7.Rows.Add(reader["StockId"], reader["ItemCode"], reader["Quantity"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Status")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView7);
                        dataGridView7.Rows.Clear();
                        dataGridView7.Show();
                        cmd.CommandText = "SELECT * FROM stocks WHERE Status like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@status", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView7.Rows.Add(reader["StockId"], reader["ItemCode"], reader["Quantity"], reader["Status"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

            }
            //SUPPLIERS - selected
            else if (searchCategory == "Suppliers")
            {
                if (cbxSearchBy.Text == "Supplier ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView8);
                        dataGridView8.Rows.Clear();
                        dataGridView8.Show();
                        cmd.CommandText = "SELECT * FROM suppliers WHERE SupplierId = @supid";
                        cmd.Parameters.AddWithValue("@supid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView8.Rows.Add(reader["SupplierId"], reader["SupplierName"], reader["SupplierEmail"], reader["SupplierContactNumber"], reader["SupplierAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Supplier Name")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView8);
                        dataGridView8.Rows.Clear();
                        dataGridView8.Show();
                        cmd.CommandText = "SELECT * FROM suppliers WHERE SupplierName like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@supname", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView8.Rows.Add(reader["SupplierId"], reader["SupplierName"], reader["SupplierEmail"], reader["SupplierContactNumber"], reader["SupplierAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Supplier Email")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView8);
                        dataGridView8.Rows.Clear();
                        dataGridView8.Show();
                        cmd.CommandText = "SELECT * FROM suppliers WHERE SupplierEmail like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@email", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView8.Rows.Add(reader["SupplierId"], reader["SupplierName"], reader["SupplierEmail"], reader["SupplierContactNumber"], reader["SupplierAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Supplier Contact Number")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView8);
                        dataGridView8.Rows.Clear();
                        dataGridView8.Show();
                        cmd.CommandText = "SELECT * FROM suppliers WHERE SupplierContactNumber = @supnumber";
                        cmd.Parameters.AddWithValue("@supnumber", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView8.Rows.Add(reader["SupplierId"], reader["SupplierName"], reader["SupplierEmail"], reader["SupplierContactNumber"], reader["SupplierAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Supplier Address")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView8);
                        dataGridView8.Rows.Clear();
                        dataGridView8.Show();
                        cmd.CommandText = "SELECT * FROM suppliers WHERE SupplierAddress like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@supaddress", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView8.Rows.Add(reader["SupplierId"], reader["SupplierName"], reader["SupplierEmail"], reader["SupplierContactNumber"], reader["SupplierAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

            }

            //CUSTOMER - selected
            else if (searchCategory == "Customers")
            {
                if (cbxSearchBy.Text == "Customer ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView9);
                        dataGridView9.Rows.Clear();
                        dataGridView9.Show();
                        cmd.CommandText = "SELECT * FROM customers WHERE CustomerId = @cusid";
                        cmd.Parameters.AddWithValue("@cusid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView9.Rows.Add(reader["CustomerId"], reader["CustomerName"], reader["CustomerEmail"], reader["CustomerContactNumber"], reader["CustomerAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Customer Name")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView9);
                        dataGridView9.Rows.Clear();
                        dataGridView9.Show();
                        cmd.CommandText = "SELECT * FROM customers WHERE CustomerName like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@cusname", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView9.Rows.Add(reader["CustomerId"], reader["CustomerName"], reader["CustomerEmail"], reader["CustomerContactNumber"], reader["CustomerAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Customer Email")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView9);
                        dataGridView9.Rows.Clear();
                        dataGridView9.Show();
                        cmd.CommandText = "SELECT * FROM customers WHERE CustomerEmail like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@cusemail", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView9.Rows.Add(reader["CustomerId"], reader["CustomerName"], reader["CustomerEmail"], reader["CustomerContactNumber"], reader["CustomerAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Contact No.")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView9);
                        dataGridView9.Rows.Clear();
                        dataGridView9.Show();
                        cmd.CommandText = "SELECT * FROM customers WHERE CustomerContactNumber = @cusnumber";
                        cmd.Parameters.AddWithValue("@cusnumber", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView9.Rows.Add(reader["CustomerId"], reader["CustomerName"], reader["CustomerEmail"], reader["CustomerContactNumber"], reader["CustomerAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Customer Address")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView9);
                        dataGridView9.Rows.Clear();
                        dataGridView9.Show();
                        cmd.CommandText = "SELECT * FROM customers WHERE CustomerAddress like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@cusaddress", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView9.Rows.Add(reader["CustomerId"], reader["CustomerName"], reader["CustomerEmail"], reader["CustomerContactNumber"], reader["CustomerAddress"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
            }

            //RETURN SALES - selected
            else if (searchCategory == "Sales Return")
            {
                if (cbxSearchBy.Text == "Return ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView10);
                        dataGridView10.Rows.Clear();
                        dataGridView10.Show();
                        cmd.CommandText = "SELECT * FROM salesreturns WHERE SReturnId = @salRid";
                        cmd.Parameters.AddWithValue("@salRid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView10.Rows.Add(reader["SReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["SalesId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Quantity")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView10);
                        dataGridView10.Rows.Clear();
                        dataGridView10.Show();
                        cmd.CommandText = "SELECT * FROM salesreturns WHERE ReturnQuantity = @SRquantity";
                        cmd.Parameters.AddWithValue("@SRquantity", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView10.Rows.Add(reader["SReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["SalesId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Date")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView10);
                        dataGridView10.Rows.Clear();
                        dataGridView10.Show();
                        cmd.CommandText = "SELECT * FROM salesreturns WHERE ReturnDate like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@SRdate", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView11.Rows.Add(reader["SReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["SalesId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Sales ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView10);
                        dataGridView10.Rows.Clear();
                        dataGridView10.Show();
                        cmd.CommandText = "SELECT * FROM salesreturns WHERE SalesId = @SalesRID";
                        cmd.Parameters.AddWithValue("@SalesRID", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView10.Rows.Add(reader["SReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["SalesId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");

                }
            }


            //RETURN PURCHASES - selected
            else if (searchCategory == "Purchases Return")
            {
                if (cbxSearchBy.Text == "Return ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView11);
                        dataGridView11.Rows.Clear();
                        dataGridView11.Show();
                        cmd.CommandText = "SELECT * FROM purchasesreturns WHERE PReturnId = @purRid";
                        cmd.Parameters.AddWithValue("@purRid", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView11.Rows.Add(reader["PReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["PurchId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();
                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Quantity")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView11);
                        dataGridView11.Rows.Clear();
                        dataGridView11.Show();
                        cmd.CommandText = "SELECT * FROM purchasesreturns WHERE ReturnQuantity = @PRquantity";
                        cmd.Parameters.AddWithValue("@PRquantity", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView11.Rows.Add(reader["PReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["PurchId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }

                else if (cbxSearchBy.Text == "Date")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView11);
                        dataGridView11.Rows.Clear();
                        dataGridView11.Show();
                        cmd.CommandText = "SELECT * FROM purchasesreturns WHERE ReturnDate like '%" + textBox1.Text + "%'";
                        cmd.Parameters.AddWithValue("@PRdate", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView11.Rows.Add(reader["PReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["PurchId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");
                }
                else if (cbxSearchBy.Text == "Purchase ID")
                {
                    if (textBox1.Text != "")
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(dataGridView11);
                        dataGridView11.Rows.Clear();
                        dataGridView11.Show();
                        cmd.CommandText = "SELECT * FROM purchasesreturns WHERE PurchId = @PurchRID";
                        cmd.Parameters.AddWithValue("@PurchRID", textBox1.Text);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cmd.Parameters.Clear();
                                    dataGridView11.Rows.Add(reader["PReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["PurchId"]);
                                }
                            }
                            else
                                MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reader.Close();

                        }
                    }
                    else
                        MessageBox.Show("Please enter value in the text box.");

                }
            }

            //No category selected
            else if (cbxSearchBy.SelectedIndex == -1)
                MessageBox.Show("Please select option in Search by.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox1.Text == "Type here to search" || textBox1.Text == "")
                MessageBox.Show("Please enter word to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    
        //Hover - change color
        private void OnMouseEnterButton1(object sender, EventArgs e)
        {
            button1.BackColor = Color.SeaGreen;
            button1.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button1.ForeColor = Color.White;
        }
        private void OnMouseLeaveButton1(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(229, 245, 242);
            button1.FlatAppearance.BorderColor = Color.SeaGreen;
            button1.ForeColor = Color.Black;
        }

        public void SetForm()
        {
            this.cbxSearchBy.DropDownStyle = ComboBoxStyle.DropDownList;
            dataGridView1.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView3.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView4.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView4.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView5.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView5.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView6.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView6.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView7.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView7.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView8.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView8.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView9.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView9.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView10.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView10.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView11.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView11.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            button1.MouseEnter += OnMouseEnterButton1;
            button1.MouseLeave += OnMouseLeaveButton1;
            dataGridView1.Hide();
            dataGridView2.Hide();
            dataGridView3.Hide();
            dataGridView4.Hide();
            dataGridView5.Hide();
            dataGridView6.Hide();
            dataGridView7.Hide();
            dataGridView8.Hide();
            dataGridView9.Hide();
            dataGridView10.Hide();
            dataGridView11.Hide();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedItem = (string)cbxSearchBy.SelectedItem;

            if (selectedItem == "Date")
            {
                textBox1.Text = "yyyy-mm-dd";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            searchCategory = "Items";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Item Code");
            cbxSearchBy.Items.Add("Item Name");
            cbxSearchBy.Items.Add("Item Price");
            cbxSearchBy.Items.Add("Item SellPrice");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView1);
            dataGridView1.Rows.Clear();
            dataGridView1.Show();
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
                            dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"], reader2["Quantity"]);
                        }
                    }
                }
                reader.Close();
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            searchCategory = "Sales";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Item Code");
            cbxSearchBy.Items.Add("Sales ID");
            cbxSearchBy.Items.Add("Transaction ID");
            cbxSearchBy.Items.Add("Customer ID");
            cbxSearchBy.Items.Add("Payment Type");
            cbxSearchBy.Items.Add("Applicable Discount");
            cbxSearchBy.Items.Add("Sales Invoice No.");
            cbxSearchBy.Items.Add("Employee ID");
            cbxSearchBy.Items.Add("Status");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView3);
            dataGridView3.Rows.Clear();
            dataGridView3.Show();
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
                                dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], "ToShip");
                            else
                                dataGridView3.Rows.Add(reader["SalesId"], reader["ItemCode"], reader2["ItemName"], reader["ItemSellPrice"], reader["Quantity"], reader["TransId"], reader["CustomerId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["SalesInvoiceNumber"], reader["EmployeeId"], reader["Status"]);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }
        }

        private void btnPurchases_Click(object sender, EventArgs e)
        {
            searchCategory = "Purchases";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Item Code");
            cbxSearchBy.Items.Add("Purchase ID");
            cbxSearchBy.Items.Add("Transaction ID");
            cbxSearchBy.Items.Add("Supplier ID");
            cbxSearchBy.Items.Add("Payment Type");
            cbxSearchBy.Items.Add("Applicable Discount");
            cbxSearchBy.Items.Add("Status");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView2);
            dataGridView2.Rows.Clear();
            dataGridView2.Show();
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
                            dataGridView2.Rows.Add(reader["PurchId"], reader["ItemCode"], reader2["ItemName"], reader["ItemPrice"], reader["Quantity"], reader["TransId"], reader["SupplierId"], reader["PaymentType"], reader["ApplicableDiscount"], reader["Status"]);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            searchCategory = "Transactions";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Transaction ID");
            cbxSearchBy.Items.Add("Transaction Type");
            cbxSearchBy.Items.Add("Date");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView4);
            dataGridView4.Rows.Clear();
            dataGridView4.Show();
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
                    dataGridView4.Rows.Add(reader["TransId"], reader["TotalAmount"], reader["TransType"], reader["Date"]);
                }
                reader.Close();
            }
        }

        private void btnPayables_Click(object sender, EventArgs e)
        {
            searchCategory = "Payables";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Payable ID");
            cbxSearchBy.Items.Add("Purchase ID");
            cbxSearchBy.Items.Add("Status");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView5);
            dataGridView5.Rows.Clear();
            dataGridView5.Show();
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            MySqlDataReader reader;
            cmd.CommandText = "SELECT * FROM Payables";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView5.Rows.Add(reader["PayableId"], reader["PurchId"], reader["Status"]);
                }
                reader.Close();
            }
        }

        private void btnReceivables_Click(object sender, EventArgs e)
        {
            searchCategory = "Receivables";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Receivable ID");
            cbxSearchBy.Items.Add("Sales ID");
            cbxSearchBy.Items.Add("Status");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView6);
            dataGridView6.Rows.Clear();
            dataGridView6.Show();
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            MySqlDataReader reader;
            cmd.CommandText = "SELECT * FROM Receivables";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if(int.Parse(reader["AdditionalFee"].ToString()) != 0)
                        dataGridView6.Rows.Add(reader["ReceivableId"], reader["AdditionalFee"], reader["SalesId"], "ToShip");
                    else
                        dataGridView6.Rows.Add(reader["ReceivableId"], reader["AdditionalFee"], reader["SalesId"], reader["Status"]);
                }
                reader.Close();
            }
        }

        private void btnReturnSales_Click(object sender, EventArgs e)
        {
            searchCategory = "Sales Return";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Return ID");
            cbxSearchBy.Items.Add("Quantity");
            cbxSearchBy.Items.Add("Date");
            cbxSearchBy.Items.Add("Sales ID");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView10);
            dataGridView10.Rows.Clear();
            dataGridView10.Show();
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            MySqlDataReader reader;
            cmd.CommandText = "SELECT * FROM SalesReturns";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView10.Rows.Add(reader["SReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["SalesId"]);
                }
                reader.Close();
            }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            searchCategory = "Customers";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Customer ID");
            cbxSearchBy.Items.Add("Customer Name");
            cbxSearchBy.Items.Add("Customer Email");
            cbxSearchBy.Items.Add("Contact No.");
            cbxSearchBy.Items.Add("Customer Address");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView9);
            dataGridView9.Rows.Clear();
            dataGridView9.Show();
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            MySqlDataReader reader;
            cmd.CommandText = "SELECT * FROM Customers";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView9.Rows.Add(reader["CustomerId"], reader["CustomerName"], reader["CustomerEmail"], reader["CustomerContactNumber"], reader["CustomerAddress"]);
                }
                reader.Close();
            }
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            searchCategory = "Suppliers";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Supplier ID");
            cbxSearchBy.Items.Add("Supplier Name");
            cbxSearchBy.Items.Add("Supplier Email");
            cbxSearchBy.Items.Add("Contact No.");
            cbxSearchBy.Items.Add("Supplier Address");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView8);
            dataGridView8.Rows.Clear();
            dataGridView8.Show();
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            MySqlDataReader reader;
            cmd.CommandText = "SELECT * FROM Suppliers";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView8.Rows.Add(reader["SupplierId"], reader["SupplierName"], reader["SupplierEmail"], reader["SupplierContactNumber"], reader["SupplierAddress"]);
                }
                reader.Close();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = Color.Black;
        }

        private void btnReturnPurchases_Click(object sender, EventArgs e)
        {
            searchCategory = "Purchases Return";
            textBox1.Clear();
            cbxSearchBy.Items.Clear();
            cbxSearchBy.Items.Add("Return ID");
            cbxSearchBy.Items.Add("Quantity");
            cbxSearchBy.Items.Add("Date");
            cbxSearchBy.Items.Add("Purchase ID");

            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView11);
            dataGridView11.Rows.Clear();
            dataGridView11.Show();
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            MySqlDataReader reader;
            cmd.CommandText = "SELECT * FROM PurchasesReturns";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView11.Rows.Add(reader["PReturnId"], reader["ReturnQuantity"], reader["ReturnDate"], reader["PurchId"]);
                }
                reader.Close();
            }
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

        private void btnPurchases_MouseEnter(object sender, EventArgs e)
        {
            btnPurchases.BackColor = Color.SeaGreen;
            btnPurchases.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnPurchases.ForeColor = Color.White;
        }

        private void btnPurchases_MouseLeave(object sender, EventArgs e)
        {
            btnPurchases.BackColor = Color.FromArgb(229, 245, 242);
            btnPurchases.FlatAppearance.BorderColor = Color.SeaGreen;
            btnPurchases.ForeColor = Color.Black;
        }

        private void btnSales_MouseEnter(object sender, EventArgs e)
        {
            btnSales.BackColor = Color.SeaGreen;
            btnSales.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnSales.ForeColor = Color.White;
        }

        private void btnSales_MouseLeave(object sender, EventArgs e)
        {
            btnSales.BackColor = Color.FromArgb(229, 245, 242);
            btnSales.FlatAppearance.BorderColor = Color.SeaGreen;
            btnSales.ForeColor = Color.Black;
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

        private void btnReturnSales_MouseEnter(object sender, EventArgs e)
        {
            btnReturnSales.BackColor = Color.SeaGreen;
            btnReturnSales.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnReturnSales.ForeColor = Color.White;
        }

        private void btnReturnSales_MouseLeave(object sender, EventArgs e)
        {
            btnReturnSales.BackColor = Color.FromArgb(229, 245, 242);
            btnReturnSales.FlatAppearance.BorderColor = Color.SeaGreen;
            btnReturnSales.ForeColor = Color.Black;
        }

        private void btnReturnPurchases_MouseEnter(object sender, EventArgs e)
        {
            btnReturnPurchases.BackColor = Color.SeaGreen;
            btnReturnPurchases.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnReturnPurchases.ForeColor = Color.White;
        }

        private void btnReturnPurchases_MouseLeave(object sender, EventArgs e)
        {
            btnReturnPurchases.BackColor = Color.FromArgb(229, 245, 242);
            btnReturnPurchases.FlatAppearance.BorderColor = Color.SeaGreen;
            btnReturnPurchases.ForeColor = Color.Black;
        }

        private void btnCustomers_MouseEnter(object sender, EventArgs e)
        {
            btnCustomers.BackColor = Color.SeaGreen;
            btnCustomers.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnCustomers.ForeColor = Color.White;
        }

        private void btnCustomers_MouseLeave(object sender, EventArgs e)
        {
            btnCustomers.BackColor = Color.FromArgb(229, 245, 242);
            btnCustomers.FlatAppearance.BorderColor = Color.SeaGreen;
            btnCustomers.ForeColor = Color.Black;
        }

        private void btnSuppliers_MouseEnter(object sender, EventArgs e)
        {
            btnSuppliers.BackColor = Color.SeaGreen;
            btnSuppliers.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnSuppliers.ForeColor = Color.White;
        }

        private void btnSuppliers_MouseLeave(object sender, EventArgs e)
        {
            btnSuppliers.BackColor = Color.FromArgb(229, 245, 242);
            btnSuppliers.FlatAppearance.BorderColor = Color.SeaGreen;
            btnSuppliers.ForeColor = Color.Black;
        }
    }
}
