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
    public partial class Dashboard : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        public Dashboard()
        {
            InitializeComponent();
        }

        double totalPurchases = 0, totalSales = 0, topCustomerPay;
        int countTopCus;
        string mostBought, mostPurchased, topCustomer;

        private void Dashboard_Load(object sender, EventArgs e)
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
                            totalPurchases += double.Parse(reader2["ItemPrice"].ToString()) * int.Parse(reader["Quantity"].ToString());
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }

            cmd.Parameters.Clear();
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
                            totalSales += double.Parse(reader2["ItemSellPrice"].ToString()) * int.Parse(reader["Quantity"].ToString());
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }

            //FREQUENT
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT ItemCode, COUNT(ItemCode) AS Most_Frequent FROM Sales GROUP BY ItemCode ORDER BY Most_Frequent DESC LIMIT 1";
            using (reader = cmd.ExecuteReader())
            {
                if(reader.Read())
                {
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
                            mostBought = (reader2["ItemName"].ToString());
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT ItemCode, COUNT(ItemCode) AS Most_Frequent FROM Purchases GROUP BY ItemCode ORDER BY Most_Frequent DESC LIMIT 1";
            using (reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
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
                            mostPurchased = (reader2["ItemName"].ToString());
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT CustomerId, COUNT(CustomerId) AS Most_Frequent FROM Sales GROUP BY CustomerId ORDER BY Most_Frequent DESC LIMIT 1";
            using (reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Globals.customerid = int.Parse(reader["CustomerId"].ToString());
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM customers WHERE CustomerId = @customerid";
                    MySqlParameter itemCParam = new MySqlParameter("@customerid", MySqlDbType.Int32);
                    itemCParam.Value = Globals.customerid;
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            topCustomer = (reader2["CustomerName"].ToString());
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT COUNT(CustomerId) AS Most_Frequent FROM Sales GROUP BY CustomerId ORDER BY Most_Frequent DESC LIMIT 1";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    countTopCus = int.Parse(reader[0].ToString());
                }
                reader.Close();
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT CustomerId, COUNT(CustomerId) AS Most_Frequent FROM Sales GROUP BY CustomerId ORDER BY Most_Frequent DESC LIMIT 1";
            using (reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Globals.customerid = int.Parse(reader["CustomerId"].ToString());
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM sales WHERE CustomerId = @customerid";
                    MySqlParameter itemCParam = new MySqlParameter("@customerid", MySqlDbType.Int32);
                    itemCParam.Value = Globals.customerid;
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            topCustomerPay += double.Parse(reader2["ItemSellPrice"].ToString()) * int.Parse(reader2["Quantity"].ToString());
                        }
                        reader2.Close();
                    }
                }
                reader.Close();
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT * FROM Stocks WHERE Quantity <= @quan AND UnitMeasurement = @unit";
            cmd.Parameters.AddWithValue("@quan", 20);
            cmd.Parameters.AddWithValue("@unit", "1");
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int itemcode = int.Parse(reader["ItemCode"].ToString());
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    itemCParam.Value = itemcode;
                    cmd2.Parameters.Add(itemCParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            tblLowStock.Rows.Add(reader["ItemCode"], reader2["ItemName"], reader["Quantity"]);
                            reader2.Close();
                        }
                    }
                }
                reader.Close();
            }

            int totalOrderShip = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT * FROM Sales WHERE PaymentType = @type AND Status = @status";
            cmd.Parameters.AddWithValue("@type", "COD");
            cmd.Parameters.AddWithValue("@status", "Unpaid");
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    totalOrderShip += 1;
                }
                reader.Close();
            }

            int totalShipped = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT * FROM Sales WHERE PaymentType = @type AND Status = @status";
            cmd.Parameters.AddWithValue("@type", "COD");
            cmd.Parameters.AddWithValue("@status", "Paid");
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    totalShipped += 1;
                }
                reader.Close();
            }

            int totalShipping = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT * FROM Sales WHERE PaymentType = @type AND Status = @status";
            cmd.Parameters.AddWithValue("@type", "COD");
            cmd.Parameters.AddWithValue("@status", "Shipping");
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    totalShipping += 1;
                }
                reader.Close();
            }

            int allItems = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT * FROM Items";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allItems += 1;
                }
                reader.Close();
            }

            double allStocks = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT * FROM Stocks WHERE UnitMeasurement = @unit";
            cmd.Parameters.AddWithValue("@unit", "1");
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allStocks += double.Parse(reader["Quantity"].ToString());
                }
                reader.Close();
            }

            int allToShip = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT * FROM Sales WHERE PaymentType = @type AND Status = @status";
            cmd.Parameters.AddWithValue("@type", "COD");
            cmd.Parameters.AddWithValue("@status", "Unpaid");
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allToShip += int.Parse(reader["Quantity"].ToString());
                }
                reader.Close();
            }

            label3.Text = "₱ " + totalPurchases.ToString("F2");
            label9.Text = mostPurchased;
            label4.Text = "₱ " + totalSales.ToString("F2");
            label13.Text = mostBought;
            label17.Text = topCustomer;
            label20.Text = countTopCus.ToString();
            label24.Text = "₱ " + topCustomerPay.ToString("F2");
            lblToShipped.Text = totalOrderShip.ToString();
            lblShipping.Text = totalShipping.ToString();
            lblPaymentReceived.Text = totalShipped.ToString();
            lblAllItems.Text = allItems.ToString();
            lblQuantityOnHand.Text = Math.Ceiling(allStocks).ToString();
            lblQuantityToShip.Text = allToShip.ToString();
        }
    }
}
