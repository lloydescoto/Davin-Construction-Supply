using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication8
{
    public partial class AddItem : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        string caption = "Invalid Input Detected";
        double total = 0;
        int successItems = 0;
        int itemNumber = 1;
        double discountValue;
        string supplierName;
        string supplierContact;
        string supplierEmail;
        string supplierAddress;
        int supplierContactInt;
        ArrayList items = new ArrayList();
        ArrayList prices = new ArrayList();
        ArrayList quantity = new ArrayList();
        ArrayList sellprices = new ArrayList();
        ArrayList supplier = new ArrayList();
        ArrayList suppliercontact = new ArrayList();
        ArrayList supplieremail = new ArrayList();
        ArrayList supplieraddress = new ArrayList(); 
        ArrayList paymenttype = new ArrayList();
        ArrayList discount = new ArrayList();
        ArrayList unit = new ArrayList();
        DateTime dn = DateTime.Now;
        string[] availFraction = { "1", "1/2", "1/4", "1/8" };
        double[] availFractionValue = { 1, 0.50, 0.25, 0.125 };
        public AddItem()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            dataGridView1.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            button1.MouseEnter += OnMouseEnterButton1;
            button1.MouseLeave += OnMouseLeaveButton1;
            button2.MouseEnter += OnMouseEnterButton2;
            button2.MouseLeave += OnMouseLeaveButton2;
            button3.MouseEnter += OnMouseEnterButton3;
            button3.MouseLeave += OnMouseLeaveButton3;
            button4.MouseEnter += OnMouseEnterButton4;
            button4.MouseLeave += OnMouseLeaveButton4;
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            dateLbl.Text = dn.ToString("yyyy-MM-dd");
            conn = new MySqlConnection(connectionString);
            conn2 = new MySqlConnection(connectionString);
            conn.Open();
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            cmd2.Connection = conn2;
            MySqlDataReader reader;
            //MySqlDataReader reader2;
            cmd.CommandText = "SELECT * FROM Items";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //dataGridView2.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"],reader["ItemSellPrice"]);
                }
                reader.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox11.Clear();
            comboBox2.Text = "";
            dataGridView1.Rows.Clear();
            itemNumber = 1;
            total = 0;
            moneyLbl.Text = "00.00";
            txtSubtotal.Text = "00.00";
            txtTotalQuantity.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double priceDouble;
            double sellPriceDouble;
            bool verifyDouble = Double.TryParse(textBox2.Text, out priceDouble);
            bool verifySellDouble = Double.TryParse(textBox2.Text, out sellPriceDouble);
            if (hasSpecialChar(textBox1.Text) || hasNumber(textBox1.Text))
            {
                string message = "Item Name must not have Special Character and Number. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    textBox1.Clear();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    string message = "Item Name must be provided. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Clear();
                    }
                }
                else
                {
                    //if (hasSpecialChar(textBox2.Text) || hasLetter(textBox2.Text))
                    if (!verifyDouble || double.Parse(textBox2.Text) <= 0)
                    {
                        string message = "Item Price must not have Special Character and Letter. Try Again!";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                        if (result == DialogResult.OK)
                        {
                            textBox2.Clear();
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(textBox2.Text))
                        {
                            string message = "Item Price must be provided. Try Again!";
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                            if (result == DialogResult.OK)
                            {
                                textBox2.Clear();
                            }
                        }
                        else
                        {
                            if (hasSpecialChar(textBox3.Text) || hasLetter(textBox3.Text))
                            {
                                string message = "Quantity must not have Special Character and Letter. Try Again!";
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                DialogResult result;

                                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                if (result == DialogResult.OK)
                                {
                                    textBox3.Clear();
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(textBox3.Text) || textBox3.Text.Equals((0).ToString()))
                                {
                                    string message = "Quantity must be provided. Try Again!";
                                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                                    DialogResult result;

                                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                    if (result == DialogResult.OK)
                                    {
                                        textBox3.Clear();
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(textBox9.Text))
                                    {
                                        string message = "Selling Price must be provided. Try Again!";
                                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                                        DialogResult result;

                                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                        if (result == DialogResult.OK)
                                        {
                                            textBox9.Clear();
                                        }
                                    }
                                    else
                                    {
                                        if (!verifySellDouble || double.Parse(textBox9.Text) <= 0)
                                        {
                                            string message = "Selling Price must not have Special Character and Letter. Try Again!";
                                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                                            DialogResult result;

                                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                            if (result == DialogResult.OK)
                                            {
                                                textBox9.Clear();
                                            }
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(textBox5.Text))
                                            {
                                                string message = "Supplier Name must be provided. Try Again!";
                                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                DialogResult result;

                                                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                if (result == DialogResult.OK)
                                                {
                                                    textBox5.Clear();
                                                }
                                            }
                                            else
                                            {
                                                if (hasSpecialChar(textBox11.Text) || hasLetter(textBox11.Text))
                                                {
                                                    string message = "Applicable Discount must not have Special Character and Letter. Try Again!";
                                                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                    DialogResult result;

                                                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                    if (result == DialogResult.OK)
                                                    {
                                                        textBox11.Clear();
                                                    }
                                                }
                                                else
                                                {
                                                    if (hasLetter(textBox6.Text))
                                                    {
                                                        string message = "Supplier Contact must not have Special Character and Letter. Try Again!";
                                                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                        DialogResult result;

                                                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                        if (result == DialogResult.OK)
                                                        {
                                                            textBox6.Clear();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(textBox11.Text) || int.Parse(textBox11.Text) == 0)
                                                        {
                                                            discountValue = 0;
                                                            textBox11.Text = discountValue.ToString();
                                                        }
                                                        else
                                                        {
                                                            discountValue = double.Parse(textBox11.Text) / 100;
                                                        }
                                                        MySqlCommand cmd = new MySqlCommand();
                                                        MySqlDataReader reader;
                                                        cmd.Connection = conn;
                                                        //Check Supplier Contact
                                                        cmd.CommandText = "SELECT * FROM Suppliers WHERE SupplierName = @checkSuppContact";
                                                        MySqlParameter checkContactParam = new MySqlParameter("@checkSuppContact", MySqlDbType.String);
                                                        checkContactParam.Value = textBox5.Text.ToString();
                                                        cmd.Parameters.Add(checkContactParam);
                                                        using (reader = cmd.ExecuteReader())
                                                        {
                                                            if (reader.Read())
                                                            {
                                                                if (reader["SupplierContactNumber"].ToString() == textBox6.Text)
                                                                {
                                                                    textBox6.Text = reader["SupplierContactNumber"].ToString();
                                                                    reader.Close();
                                                                }
                                                                else
                                                                {
                                                                    string message = "Supplier Contact is not the same in the record. Update it?";
                                                                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                                                                    DialogResult result;

                                                                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
                                                                    if (result == DialogResult.Yes)
                                                                    {
                                                                        reader.Close();
                                                                        cmd.Parameters.Clear();
                                                                        cmd.CommandText = "UPDATE Suppliers SET SupplierContactNumber = @inputContact WHERE SupplierName = @readerSuppName";
                                                                        MySqlParameter inputContactParam = new MySqlParameter("@inputContact", MySqlDbType.String);
                                                                        MySqlParameter readerSuppNameParam = new MySqlParameter("@readerSuppName", MySqlDbType.String);
                                                                        inputContactParam.Value = textBox6.Text;
                                                                        readerSuppNameParam.Value = textBox5.Text;
                                                                        cmd.Parameters.Add(inputContactParam);
                                                                        cmd.Parameters.Add(readerSuppNameParam);
                                                                        cmd.Prepare();
                                                                        cmd.ExecuteNonQuery();
                                                                        supplierContact = textBox6.Text;
                                                                    }
                                                                    if (result == DialogResult.No)
                                                                    {
                                                                        textBox6.Text = reader["SupplierContactNumber"].ToString();
                                                                        reader.Close();
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                supplierName = textBox5.Text;
                                                                supplierContact = textBox6.Text;
                                                                supplierEmail = textBox7.Text;
                                                                supplierAddress = textBox8.Text;
                                                            }
                                                        }
                                                        //Check Supplier Email
                                                        cmd.CommandText = "SELECT * FROM Suppliers WHERE SupplierName = @checkSuppEmail";
                                                        MySqlParameter checkEmailParam = new MySqlParameter("@checkSuppEmail", MySqlDbType.String);
                                                        checkEmailParam.Value = textBox5.Text;
                                                        cmd.Parameters.Add(checkEmailParam);
                                                        using (reader = cmd.ExecuteReader())
                                                        {
                                                            if (reader.Read())
                                                            {
                                                                if (reader["SupplierEmail"].ToString() == textBox7.Text)
                                                                {
                                                                    textBox7.Text = reader["SupplierEmail"].ToString();
                                                                    reader.Close();
                                                                }
                                                                else
                                                                {
                                                                    string message = "Supplier Email is not the same in the record. Update it?";
                                                                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                                                                    DialogResult result;

                                                                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
                                                                    if (result == DialogResult.Yes)
                                                                    {
                                                                        reader.Close();
                                                                        cmd.Parameters.Clear();
                                                                        cmd.CommandText = "UPDATE Suppliers SET SupplierEmail = @inputEmail WHERE SupplierName = @readerSuppName";
                                                                        MySqlParameter inputEmailParam = new MySqlParameter("@inputEmail", MySqlDbType.String);
                                                                        MySqlParameter readerSuppNameParam = new MySqlParameter("@readerSuppName", MySqlDbType.String);
                                                                        inputEmailParam.Value = textBox7.Text;
                                                                        readerSuppNameParam.Value = textBox5.Text;
                                                                        cmd.Parameters.Add(inputEmailParam);
                                                                        cmd.Parameters.Add(readerSuppNameParam);
                                                                        cmd.Prepare();
                                                                        cmd.ExecuteNonQuery();
                                                                        supplierEmail = textBox7.Text;
                                                                    }
                                                                    if (result == DialogResult.No)
                                                                    {
                                                                        textBox7.Text = reader["SupplierEmail"].ToString();
                                                                        reader.Close();
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                supplierName = textBox5.Text;
                                                                supplierContact = textBox6.Text;
                                                                supplierEmail = textBox7.Text;
                                                                supplierAddress = textBox8.Text;
                                                            }
                                                        }
                                                        //Check Supplier Address
                                                        cmd.CommandText = "SELECT * FROM Suppliers WHERE SupplierName = @checkSuppAddress";
                                                        MySqlParameter checkAddressParam = new MySqlParameter("@checkSuppAddress", MySqlDbType.String);
                                                        checkAddressParam.Value = textBox5.Text;
                                                        cmd.Parameters.Add(checkAddressParam);
                                                        using (reader = cmd.ExecuteReader())
                                                        {
                                                            if (reader.Read())
                                                            {
                                                                if (reader["SupplierAddress"].ToString() == textBox8.Text)
                                                                {
                                                                    textBox8.Text = reader["SupplierAddress"].ToString();
                                                                    reader.Close();
                                                                }
                                                                else
                                                                {
                                                                    string message = "Supplier Address is not the same in the record. Update it?";
                                                                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                                                                    DialogResult result;

                                                                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
                                                                    if (result == DialogResult.Yes)
                                                                    {
                                                                        reader.Close();
                                                                        cmd.Parameters.Clear();
                                                                        cmd.CommandText = "UPDATE Suppliers SET SupplierAddress = @inputAddress WHERE SupplierName = @readerSuppName";
                                                                        MySqlParameter inputAddressParam = new MySqlParameter("@inputAddress", MySqlDbType.String);
                                                                        MySqlParameter readerSuppNameParam = new MySqlParameter("@readerSuppName", MySqlDbType.String);
                                                                        inputAddressParam.Value = textBox8.Text;
                                                                        readerSuppNameParam.Value = textBox5.Text;
                                                                        cmd.Parameters.Add(inputAddressParam);
                                                                        cmd.Parameters.Add(readerSuppNameParam);
                                                                        cmd.Prepare();
                                                                        cmd.ExecuteNonQuery();
                                                                        supplierAddress = textBox8.Text;
                                                                    }
                                                                    if (result == DialogResult.No)
                                                                    {
                                                                        textBox8.Text = reader["SupplierAddress"].ToString();
                                                                        reader.Close();
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                supplierName = textBox5.Text;
                                                                supplierContact = textBox6.Text;
                                                                supplierEmail = textBox7.Text;
                                                                supplierAddress = textBox8.Text;
                                                            }
                                                        }
                                                        txtTotalQuantity.Text = "" + (int.Parse(txtTotalQuantity.Text) + int.Parse(textBox3.Text));
                                                        double subtotal = int.Parse(textBox3.Text) * double.Parse(textBox2.Text);
                                                        txtSubtotal.Text = (double.Parse(txtSubtotal.Text) + subtotal).ToString("F2");
                                                        dataGridView1.Rows.Add(itemNumber, textBox1.Text, textBox2.Text, textBox9.Text, textBox3.Text, comboBoxUnit.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, comboBox2.Text, textBox11.Text);
                                                        itemNumber++;
                                                        if (discountValue > 0)
                                                        {
                                                            total += double.Parse(textBox2.Text) * double.Parse(textBox3.Text);
                                                            total -= (double.Parse(textBox2.Text) * double.Parse(textBox3.Text)) * discountValue;
                                                            double discount = (double.Parse(textBox2.Text) * double.Parse(textBox3.Text)) * (double.Parse(textBox11.Text) / 100);
                                                            discountLbl.Text = (double.Parse(discountLbl.Text) + discount).ToString("F2");
                                                            moneyLbl.Text = total.ToString("F2");
                                                        }
                                                        else
                                                        {
                                                            total += double.Parse(textBox2.Text) * double.Parse(textBox3.Text);
                                                            moneyLbl.Text = total.ToString("F2");
                                                        }
                                                        textBox1.Text = "";
                                                        textBox2.Text = "";
                                                        textBox3.Text = "";
                                                        textBox5.ReadOnly = true;
                                                        textBox6.ReadOnly = true;
                                                        textBox7.ReadOnly = true;
                                                        textBox8.ReadOnly = true;
                                                        textBox9.Text = "";
                                                        textBox11.Text = "";
                                                        comboBoxUnit.SelectedIndex = -1;

                                                        textBox9.ReadOnly = false;
                                                        textBox2.ReadOnly = false;
                                                        textBox1.ReadOnly = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTotalQuantity.Text = "0";
            txtSubtotal.Text = "00.00";
            discountLbl.Text = "00.00";
            moneyLbl.Text = "00.00";
            if (dataGridView1.Rows.Count > 0)
            {
                int index = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows.RemoveAt(index);

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[i];
                    int itemNo = int.Parse(selectedRow.Cells["ItemNo"].Value.ToString());

                    if (i != itemNo - 1)
                    {
                        selectedRow.Cells["ItemNo"].Value = i + 1;
                    }
                    txtTotalQuantity.Text = "" + ((int.Parse(txtTotalQuantity.Text) + int.Parse(dataGridView1.Rows[i].Cells["ItemQuan"].Value.ToString())));
                    double subtotal = int.Parse(dataGridView1.Rows[i].Cells["ItemQuan"].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells["UnitPrice"].Value.ToString());
                    txtSubtotal.Text = (double.Parse(txtSubtotal.Text) + subtotal).ToString("F2");
                }
                discountLbl.Text = (double.Parse(txtSubtotal.Text) * (double.Parse(textBox11.Text) / 100)).ToString("F2");
                moneyLbl.Text = (double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)).ToString("F2");
            }
            else
                MessageBox.Show("No items are on queue.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connectionString);
            conn.Open();
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No items are on queue", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox11.Clear();
                comboBox2.Text = "";
                dataGridView1.Rows.Clear();
                itemNumber = 1;
                total = 0;
                moneyLbl.Text = "00.00";
                txtSubtotal.Text = "00.00";
                txtTotalQuantity.Text = "0";
                comboBox2.SelectedItem = null;
            }
            else
            {
                if (string.IsNullOrEmpty(comboBox2.Text))
                {
                    string message = "Payment Type must be provided. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtSINo.Text))
                    {
                        string message = "Invoice Number must be provided. Try Again!";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                        if (result == DialogResult.OK)
                        {
                        }
                    }
                    else
                    {
                        for (int x = 0; x < dataGridView1.Rows.Count; x++)
                        {
                            MySqlCommand cmd = new MySqlCommand();
                            MySqlDataReader reader;
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT * FROM Items WHERE ItemName = @item";
                            MySqlParameter itemParam = new MySqlParameter("@item", MySqlDbType.Text);
                            itemParam.Value = dataGridView1.Rows[x].Cells["ItemName"].Value.ToString();
                            cmd.Parameters.Add(itemParam);
                            using (reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                    reader.Close();
                                    for (int w = 0; w < availFraction.Length; w++)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @itemcode AND UnitMeasurement = @UnitMeasurement";
                                        MySqlParameter itemCodeParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                                        MySqlParameter unitMeasurementParam = new MySqlParameter("@UnitMeasurement", MySqlDbType.Text);
                                        itemCodeParam.Value = Globals.itemcode;
                                        unitMeasurementParam.Value = availFraction[w];
                                        cmd.Parameters.Add(itemCodeParam);
                                        cmd.Parameters.Add(unitMeasurementParam);
                                        using (reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                Globals.quantity = double.Parse(reader["Quantity"].ToString());
                                                reader.Close();
                                                cmd.Parameters.Clear();
                                                cmd.CommandText = "UPDATE Stocks SET Quantity = @updateQuantity WHERE ItemCode = @updateICode AND UnitMeasurement = @updateUM";
                                                MySqlParameter upQuanParam = new MySqlParameter("@updateQuantity", MySqlDbType.Int32);
                                                MySqlParameter upICodeParam = new MySqlParameter("@updateICode", MySqlDbType.Int32);
                                                MySqlParameter upUMParam = new MySqlParameter("@updateUM", MySqlDbType.Text);
                                                upQuanParam.Value = (int.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString()) / availFractionValue[w]) + Globals.quantity;
                                                upICodeParam.Value = Globals.itemcode;
                                                upUMParam.Value = availFraction[w];
                                                cmd.Parameters.Add(upQuanParam);
                                                cmd.Parameters.Add(upICodeParam);
                                                cmd.Parameters.Add(upUMParam);
                                                cmd.Prepare();
                                                cmd.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                Globals.result = "Failed to Insert";
                                                MessageBox.Show("Failed to Insert", "Result");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    reader.Close();
                                    cmd.CommandText = "INSERT INTO Items(ItemName,ItemPrice,ItemSellPrice,ItemUnit) VALUES(@itemName,@price,@sellPrice,@unit)";
                                    MySqlParameter itemNameParam = new MySqlParameter("@itemName", MySqlDbType.Text);
                                    MySqlParameter priceParam = new MySqlParameter("@price", MySqlDbType.Decimal);
                                    MySqlParameter sellPriceParam = new MySqlParameter("@sellPrice", MySqlDbType.Decimal);
                                    MySqlParameter unitParam = new MySqlParameter("@unit", MySqlDbType.Text);
                                    itemNameParam.Value = dataGridView1.Rows[x].Cells["ItemName"].Value.ToString();
                                    priceParam.Value = double.Parse(dataGridView1.Rows[x].Cells["UnitPrice"].Value.ToString());
                                    sellPriceParam.Value = double.Parse(dataGridView1.Rows[x].Cells["SellPrice"].Value.ToString());
                                    unitParam.Value = dataGridView1.Rows[x].Cells["ItemUnit"].Value.ToString();
                                    cmd.Parameters.Add(itemNameParam);
                                    cmd.Parameters.Add(priceParam);
                                    cmd.Parameters.Add(sellPriceParam);
                                    cmd.Parameters.Add(unitParam);
                                    cmd.Prepare();
                                    if ((int)cmd.ExecuteNonQuery() != 0)
                                    {
                                        cmd.CommandText = "SELECT * FROM Items WHERE ItemName = @itemRead";
                                        MySqlParameter itemReadParam = new MySqlParameter("@itemRead", MySqlDbType.Text);
                                        itemReadParam.Value = dataGridView1.Rows[x].Cells["ItemName"].Value.ToString();
                                        cmd.Parameters.Add(itemReadParam);
                                        using (reader = cmd.ExecuteReader())
                                        {
                                            string stockStatus = "Available";
                                            if (reader.Read())
                                            {
                                                Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                                reader.Close();
                                                for (int w = 0; w < availFraction.Length; w++)
                                                {
                                                    cmd.Parameters.Clear();
                                                    cmd.CommandText = "INSERT INTO Stocks(Quantity,UnitMeasurement,Status,ItemCode) VALUES (@quantity,@measurement,@status,@itemcode)";
                                                    MySqlParameter quanParam = new MySqlParameter("@quantity", MySqlDbType.Int32);
                                                    MySqlParameter measureParam = new MySqlParameter("@measurement", MySqlDbType.Text);
                                                    MySqlParameter statParam = new MySqlParameter("@status", MySqlDbType.Text);
                                                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                                                    quanParam.Value = int.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString()) / availFractionValue[w];
                                                    measureParam.Value = availFraction[w];
                                                    statParam.Value = stockStatus;
                                                    itemCParam.Value = Globals.itemcode;
                                                    cmd.Parameters.Add(quanParam);
                                                    cmd.Parameters.Add(measureParam);
                                                    cmd.Parameters.Add(statParam);
                                                    cmd.Parameters.Add(itemCParam);
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            else
                                            {
                                                Globals.result = "Failed to Insert";
                                                MessageBox.Show("Failed to Insert", "Result");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Globals.result = "Failed to Insert";
                                        MessageBox.Show("Failed to Insert", "Result");
                                    }
                                }
                            }
                        }
                        string type = "Purchases";
                        MySqlCommand cmd2 = new MySqlCommand();
                        cmd2.Connection = conn;
                        cmd2.CommandText = "INSERT INTO Transactions(TotalAmount,TransType,Date) VALUES (@total,@type,(SELECT NOW()))";
                        MySqlParameter totalParam = new MySqlParameter("@total", MySqlDbType.Double);
                        MySqlParameter typeParam = new MySqlParameter("@type", MySqlDbType.Text);
                        MySqlParameter dateParam = new MySqlParameter("@date", MySqlDbType.DateTime);
                        totalParam.Value = total;
                        typeParam.Value = type;
                        dateParam.Value = dn.ToString("yyyy-MM-dd HH:mm:ss");
                        cmd2.Parameters.Add(totalParam);
                        cmd2.Parameters.Add(typeParam);
                        cmd2.Parameters.Add(dateParam);
                        cmd2.Prepare();
                        cmd2.ExecuteNonQuery();
                        cmd2.CommandText = "SELECT MAX(TransId) FROM Transactions";
                        Globals.transid = int.Parse(cmd2.ExecuteScalar().ToString());
                        for (int x = 0; x < dataGridView1.Rows.Count; x++)
                        {
                            MySqlCommand cmd = new MySqlCommand();
                            MySqlDataReader reader;
                            cmd.Connection = conn;
                            cmd.Parameters.Clear();
                            cmd.CommandText = "SELECT * FROM Suppliers WHERE SupplierName = @suppName";
                            MySqlParameter suppNameParam = new MySqlParameter("@suppName", MySqlDbType.String);
                            suppNameParam.Value = textBox5.Text;
                            cmd.Parameters.Add(suppNameParam);
                            using (reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Globals.supplierid = int.Parse(reader["SupplierId"].ToString());
                                    reader.Close();
                                }
                                else
                                {
                                    reader.Close();
                                    cmd.CommandText = "INSERT INTO Suppliers(SupplierName,SupplierEmail,SupplierContactNumber,SupplierAddress) VALUES (@inSuppName,@inSuppEmail,@inSuppContact,@inSuppAddress)";
                                    MySqlParameter inputSuppName = new MySqlParameter("@inSuppName", MySqlDbType.String);
                                    MySqlParameter inputSuppEmail = new MySqlParameter("@inSuppEmail", MySqlDbType.String);
                                    MySqlParameter inputSuppContact = new MySqlParameter("@inSuppContact", MySqlDbType.String);
                                    MySqlParameter inputSuppAddress = new MySqlParameter("@inSuppAddress", MySqlDbType.String);
                                    inputSuppName.Value = dataGridView1.Rows[x].Cells["ItemSupp"].Value.ToString();
                                    inputSuppEmail.Value = dataGridView1.Rows[x].Cells["ItemSuppMail"].Value.ToString();
                                    inputSuppContact.Value = dataGridView1.Rows[x].Cells["ItemSuppCon"].Value.ToString();
                                    inputSuppAddress.Value = dataGridView1.Rows[x].Cells["ItemSuppAddr"].Value.ToString();
                                    cmd.Parameters.Add(inputSuppName);
                                    cmd.Parameters.Add(inputSuppEmail);
                                    cmd.Parameters.Add(inputSuppContact);
                                    cmd.Parameters.Add(inputSuppAddress);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            reader.Close();
                            cmd.CommandText = "SELECT * FROM Suppliers WHERE SupplierName = @getSuppId";
                            MySqlParameter getSuppIdParam = new MySqlParameter("@getSuppId", MySqlDbType.String);
                            getSuppIdParam.Value = textBox5.Text;
                            cmd.Parameters.Add(getSuppIdParam);
                            using (reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Globals.supplierid = int.Parse(reader["SupplierId"].ToString());
                                }
                                else
                                {
                                    Globals.result = "Failed to get Supplier Id";
                                    MessageBox.Show("Failed to get Supplier Id", "Result");
                                }
                            }
                            reader.Close();
                            cmd.CommandText = "SELECT * FROM Items WHERE ItemName = @item";
                            MySqlParameter itemParam = new MySqlParameter("@item", MySqlDbType.Text);
                            itemParam.Value = dataGridView1.Rows[x].Cells["ItemName"].Value.ToString();
                            cmd.Parameters.Add(itemParam);
                            using (reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                    Globals.itemprice = double.Parse(reader["ItemPrice"].ToString());
                                    reader.Close();
                                    cmd.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @itemcode";
                                    MySqlParameter itemCParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                                    itemCParam.Value = Globals.itemcode;
                                    cmd.Parameters.Add(itemCParam);
                                    using (reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            Globals.quantity = double.Parse(reader["Quantity"].ToString());
                                            string purchDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                                            reader.Close();
                                            cmd.CommandText = "INSERT INTO Purchases(ItemCode,ItemPrice,Quantity,TransId,SupplierId,PaymentType,InvoiceNo,ApplicableDiscount,Status,PurchaseDate) VALUES (@purIC,@purIP,@purQ,@purTI,@purSuppId,@purPayType,@purInvoice,@purAppDiscount,@purStatus,@purDate)";
                                            MySqlParameter purICParam = new MySqlParameter("@purIC", MySqlDbType.Int32);
                                            MySqlParameter purIPParam = new MySqlParameter("@purIP", MySqlDbType.Int32);
                                            MySqlParameter purQParam = new MySqlParameter("@purQ", MySqlDbType.Int32);
                                            MySqlParameter purTIparam = new MySqlParameter("@purTI", MySqlDbType.Int32);
                                            MySqlParameter purSuppIParam = new MySqlParameter("@purSuppId", MySqlDbType.Int32);
                                            MySqlParameter purPayTypeParam = new MySqlParameter("@purPayType", MySqlDbType.Text);
                                            MySqlParameter purInvoiceParam = new MySqlParameter("purInvoice", MySqlDbType.Text);
                                            MySqlParameter purAppDiscountParam = new MySqlParameter("@purAppDiscount", MySqlDbType.Text);
                                            MySqlParameter purStatusParam = new MySqlParameter("@purStatus", MySqlDbType.Text);
                                            MySqlParameter purDateParam = new MySqlParameter("@purDate", MySqlDbType.Date);
                                            purICParam.Value = Globals.itemcode;
                                            purIPParam.Value = Globals.itemprice;
                                            purQParam.Value = int.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString());
                                            purTIparam.Value = Globals.transid;
                                            purSuppIParam.Value = Globals.supplierid;
                                            purPayTypeParam.Value = comboBox2.Text;
                                            purInvoiceParam.Value = long.Parse(txtSINo.Text);
                                            purAppDiscountParam.Value = textBox11.Text;
                                            purDateParam.Value = purchDate;
                                            if (comboBox2.Text == "Payable")
                                            {
                                                purStatusParam.Value = "Unpaid";
                                            }
                                            else
                                            {
                                                purStatusParam.Value = "Paid";
                                            }
                                            cmd.Parameters.Add(purICParam);
                                            cmd.Parameters.Add(purIPParam);
                                            cmd.Parameters.Add(purQParam);
                                            cmd.Parameters.Add(purTIparam);
                                            cmd.Parameters.Add(purSuppIParam);
                                            cmd.Parameters.Add(purPayTypeParam);
                                            cmd.Parameters.Add(purInvoiceParam);
                                            cmd.Parameters.Add(purAppDiscountParam);
                                            cmd.Parameters.Add(purStatusParam);
                                            cmd.Parameters.Add(purDateParam);
                                            cmd.Prepare();
                                            if ((int)cmd.ExecuteNonQuery() != 0)
                                            {
                                                successItems++;
                                                Globals.result = "Success";
                                            }
                                            else
                                            {
                                                Globals.result = "Failed to Insert";
                                                MessageBox.Show("Failed to Insert", "Result");
                                            }
                                        }
                                        else
                                        {
                                            Globals.result = "Failed to Transact";
                                            MessageBox.Show("Failed to Transact", "Result");
                                        }
                                    }
                                }
                                else
                                {
                                    Globals.result = "Failed to Transact";
                                    MessageBox.Show("Failed to Transact", "Result");
                                }
                            }

                        }
                        string payInputSuccess = "Success";
                        for (int y = 0; y < items.Count; y++)
                        {
                            if (dataGridView1.Rows[y].Cells["ItemPayType"].Value.ToString().Equals("Payable"))
                            {
                                cmd2.Parameters.Clear();
                                cmd2.CommandText = "SELECT * FROM Purchases WHERE TransId = @purTransId AND PaymentType = @purPay";
                                MySqlParameter purTransIdParam = new MySqlParameter("@purTransId", MySqlDbType.Int32);
                                MySqlParameter purPayParam = new MySqlParameter("@purPay", MySqlDbType.Text);
                                purTransIdParam.Value = Globals.transid;
                                purPayParam.Value = "Payable";
                                cmd2.Parameters.Add(purTransIdParam);
                                cmd2.Parameters.Add(purPayParam);
                                using (MySqlDataReader reader = cmd2.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        Globals.purchid = int.Parse(reader["PurchId"].ToString());
                                        reader.Close();
                                        cmd2.Parameters.Clear();
                                        cmd2.CommandText = "INSERT INTO Payables(PurchId,Status) VALUES (@payPurchId,@payStatus)";
                                        MySqlParameter payPurchParam = new MySqlParameter("@payPurchId", MySqlDbType.Int32);
                                        MySqlParameter payStatusParam = new MySqlParameter("@payStatus", MySqlDbType.Text);
                                        payPurchParam.Value = Globals.purchid;
                                        payStatusParam.Value = "Unpaid";
                                        cmd2.Parameters.Add(payPurchParam);
                                        cmd2.Parameters.Add(payStatusParam);
                                        cmd2.Prepare();
                                        if ((int)cmd2.ExecuteNonQuery() != 0)
                                            payInputSuccess = "Success";
                                        else
                                            payInputSuccess = "Failed";
                                    }
                                }
                            }
                        }
                        if (payInputSuccess == "Success")
                        {
                            Globals.result = "Success";
                        }
                        else
                        {
                            Globals.result = "Failed";
                        }
                        if (Globals.result == "Success")
                        {
                            DialogResult result = MessageBox.Show("Success.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox5.ReadOnly = false;
                            textBox6.ReadOnly = false;
                            textBox7.ReadOnly = false;
                            textBox8.ReadOnly = false;
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                            textBox8.Clear();
                            textBox9.Clear();
                            textBox11.Clear();
                            comboBox2.Text = "";
                            comboBoxUnit.ResetText();
                            dataGridView1.Rows.Clear();
                            //dataGridView2.Rows.Clear();
                            MySqlDataReader reader;
                            cmd2.CommandText = "SELECT * FROM Items";
                            using (reader = cmd2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //dataGridView2.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"]);
                                }
                            }
                            reader.Close();
                            itemNumber = 1;
                            total = 0;
                            moneyLbl.Text = "00.00";
                            Globals.result = "";
                            txtTotalQuantity.Text = "0";
                            txtSubtotal.Text = "00.00";
                            comboBox2.SelectedItem = null;
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("Failed.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox5.ReadOnly = false;
                            textBox6.ReadOnly = false;
                            textBox7.ReadOnly = false;
                            textBox8.ReadOnly = false;
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                            textBox8.Clear();
                            textBox9.Clear();
                            textBox11.Clear();
                            comboBoxUnit.ResetText();
                            comboBox2.Text = "";
                            dataGridView1.Rows.Clear();
                            MySqlDataReader reader;
                            cmd2.CommandText = "SELECT * FROM Items";
                            using (reader = cmd2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //dataGridView2.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"]);
                                }
                            }
                            reader.Close();
                            itemNumber = 1;
                            total = 0;
                            moneyLbl.Text = "00.00";
                            Globals.result = "";
                            txtTotalQuantity.Text = "0";
                            txtSubtotal.Text = "00.00";
                            comboBox2.SelectedItem = null;
                        }
                        //}
                        //}
                    }
                }
            }
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
        private void OnMouseEnterButton2(object sender, EventArgs e)
        {
            button2.BackColor = Color.SeaGreen;
            button2.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button2.ForeColor = Color.White;
        }
        private void OnMouseLeaveButton2(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(229, 245, 242);
            button2.FlatAppearance.BorderColor = Color.SeaGreen;
            button2.ForeColor = Color.Black;
        }
        private void OnMouseEnterButton3(object sender, EventArgs e)
        {
            button3.BackColor = Color.SeaGreen;
            button3.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button3.ForeColor = Color.White;
        }
        private void OnMouseLeaveButton3(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(229, 245, 242);
            button3.FlatAppearance.BorderColor = Color.SeaGreen;
            button3.ForeColor = Color.Black;
        }
        private void OnMouseEnterButton4(object sender, EventArgs e)
        {
            button4.BackColor = Color.SeaGreen;
            button4.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button4.ForeColor = Color.White;
        }
        private void OnMouseLeaveButton4(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(229, 245, 242);
            button4.FlatAppearance.BorderColor = Color.SeaGreen;
            button4.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public bool hasSpecialChar(string s)
        {
            int length = s.Length;
            int count = 0;
            foreach (char x in s)
            {
                if (!Char.IsLetterOrDigit(x))
                    count--;
                else
                    count++;
            }
            if (count == length)
                return false;
            else
                return true;
        }
        public bool hasNumber(string s)
        {
            int length = s.Length;
            int count = 0;
            foreach (char x in s)
            {
                if (Char.IsDigit(x))
                    count--;
                else
                    count++;
            }
            if (count == length)
                return false;
            else
                return true;
        }
        public bool hasLetter(string s)
        {
            int length = s.Length;
            int count = 0;
            foreach (char x in s)
            {
                if (Char.IsLetter(x))
                    count--;
                else
                    count++;
            }
            if (count == length)
                return false;
            else
                return true;
        }

        public bool acceptUnit(string s)
        {
            bool result = true;
            int slashCount = 0;
            string[] delimeter = { " " };
            string[] unit;
            if(s.Contains(" "))
            {
                unit = s.Split(delimeter, StringSplitOptions.None);
                if(unit.Length > 2)
                {
                    result = false;
                }
                else
                {
                    foreach(char w in unit[0])
                    {
                        if(w == '/')
                        {
                            slashCount++;
                        }
                    }
                    if(slashCount > 1)
                    {
                        result = false;
                    }
                    foreach(char w in unit[1])
                    {
                        if(!char.IsLetter(w))
                        {
                            result = false;
                        }
                    }

                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            //panel4.Controls.Clear();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            SupplierSearch ss = new SupplierSearch();
            ss.ShowDialog();
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.BackColor = Color.SeaGreen;
            button6.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button6.ForeColor = Color.White;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.FromArgb(229, 245, 242);
            button6.FlatAppearance.BorderColor = Color.SeaGreen;
            button6.ForeColor = Color.Black;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ItemSearch sr = new ItemSearch();
            sr.ShowDialog();
            this.Show();
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            button7.BackColor = Color.SeaGreen;
            button7.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button7.ForeColor = Color.White;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.BackColor = Color.FromArgb(229, 245, 242);
            button7.FlatAppearance.BorderColor = Color.SeaGreen;
            button7.ForeColor = Color.Black;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox9.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox1.ReadOnly = false;

            textBox9.ResetText();
            textBox2.ResetText();
            textBox1.ResetText();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexpass = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[indexpass];
            int itemIndex = int.Parse(selectedRow.Cells["Column1"].Value.ToString());
            dataGridView1.Rows.Clear();
            itemIndex = itemIndex - 1;

            total -= double.Parse(prices[itemIndex].ToString()) * double.Parse(quantity[itemIndex].ToString());
            items.RemoveAt(itemIndex);
            prices.RemoveAt(itemIndex);
            quantity.RemoveAt(itemIndex);
            for (int x = 0; x < items.Count; x++)
            {
                dataGridView1.Rows.Add(x + 1, items[x], prices[x], quantity[x]);
            }
            itemNumber--;
            moneyLbl.Text = total.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

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

        private void txtSINo_TextChanged(object sender, EventArgs e)
        {
            try
            {

                long invoice = long.Parse(txtSINo.Text);
            }
            catch (System.Exception)
            {
                string message = "Invoice No, must not have Special Character and Letters and Whole Number Only. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    txtSINo.Clear();
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox11.Text != "")
                {
                    discountLbl.Text = (double.Parse(txtSubtotal.Text) * (double.Parse(textBox11.Text) / 100)).ToString("F2");
                    moneyLbl.Text = (double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)).ToString("F2");
                }
                else
                {
                    moneyLbl.Text = double.Parse(txtSubtotal.Text).ToString("F2");
                    discountLbl.Text = "00.00";
                }
            }
            catch(System.Exception)
            {
                string message = "Discount must not have Special Character and Letters. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    textBox11.Clear();
                }
            }
        }
    }
}
