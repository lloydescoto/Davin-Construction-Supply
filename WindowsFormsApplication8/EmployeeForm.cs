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
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApplication8
{
    public partial class EmployeeForm : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        string caption = "Invalid Input Detection";
        int activeTab = 0;
        ArrayList items = new ArrayList();
        ArrayList quantity = new ArrayList();
        DateTime dn = DateTime.Now;
        int itemNumber = 1;
        double total = 0;
        int discount = 0;
        string[] availFraction = { "1", "1/2", "1/4", "1/8" };
        double[] availFractionValue = { 1, 0.50, 0.25, 0.125 };
        double currentQuantity = 0;
        int currentItemCode = 0;
        int currentItemIndex = 0;
        double unitMeasurement;
        int[] itemQueueItemCode;
        double[] itemQueueQuantity;

        public EmployeeForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            dataGridView1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            panelControl.MouseDown += panel1_MouseDown;
            panelControl.MouseUp += panel1_MouseUp;
            panelControl.MouseMove += panel1_MouseMove;
            button1.MouseEnter += OnMouseEnterButton1;
            button1.MouseLeave += OnMouseLeaveButton1;
            button2.MouseEnter += OnMouseEnterButton2;
            button2.MouseLeave += OnMouseLeaveButton2;
            button3.MouseEnter += OnMouseEnterButton3;
            button3.MouseLeave += OnMouseLeaveButton3;
            button4.MouseEnter += OnMouseEnterButton4;
            button4.MouseLeave += OnMouseLeaveButton4;
            button7.MouseEnter += OnMouseEnterButton7;
            button7.MouseLeave += OnMouseLeaveButton7;
            cbxUnit.SelectedIndex = 0;
        }


        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            LblLocalTime.Text = DateTime.Now.ToString("HH:mm:ss");
            LblUTCTime.Text = DateTime.UtcNow.ToString("HH:mm:ss");
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("MMMMMMMMMMMMM") + " " + DateTime.Now.Day + ", " + DateTime.Now.Year;
            Timer tmr = new Timer();
            tmr.Interval = 1000; //ticks every 1 second
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Start();

            conn = new MySqlConnection(connectionString);
            conn2 = new MySqlConnection(connectionString);
            conn.Open();
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            MySqlCommand cmd2 = new MySqlCommand();
            MySqlDataReader reader2;
            cmd.Connection = conn;
            cmd2.Connection = conn2;

            dataGridView1.Rows.Clear();
            cmd.CommandText = "SELECT * FROM Items";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cmd2.Parameters.Clear();
                    int item = int.Parse(reader["ItemCode"].ToString());
                    cmd2.CommandText = "SELECT * FROM stocks WHERE ItemCode = @item";
                    cmd2.Parameters.AddWithValue("@item", item);
                    cmd2.Prepare();
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            dataGridView2.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemSellPrice"], reader2["Quantity"], reader["ItemUnit"]);
                        }
                    }
                }
                reader.Close();
            }

            cmd.CommandText = "SELECT * FROM Accounts WHERE Username = @username AND Password = @password ";
            MySqlParameter userParam = new MySqlParameter("@username", MySqlDbType.Text);
            MySqlParameter passParam = new MySqlParameter("@password", MySqlDbType.Text);
            userParam.Value = Globals.username;
            passParam.Value = Globals.password;
            cmd.Parameters.Add(userParam);
            cmd.Parameters.Add(passParam);
            using (reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    cmd.CommandText = "SELECT * FROM EmployeeProfile WHERE AccountId = @id ";
                    MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Text);
                    idParam.Value = reader["AccountId"];
                    cmd.Parameters.Add(idParam);
                    conn.Close();
                    conn.Open();
                    using (reader2 = cmd.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            fnameLbl.Text = reader2["EmployeeFName"].ToString();
                            Globals.employeeid = int.Parse(reader2["EmployeeId"].ToString());
                        }
                        else
                        {
                            fnameLbl.Text = "Failed";
                        }
                    }
                }
                else
                {
                    fnameLbl.Text = "Failed";
                }

            }
            reader.Close();
            int x = 0;
            cmd.CommandText = "SELECT COUNT(*) FROM Items";
            itemQueueItemCode = new int[int.Parse(cmd.ExecuteScalar().ToString())];
            itemQueueQuantity = new double[int.Parse(cmd.ExecuteScalar().ToString())];
            cmd.CommandText = "SELECT * FROM Items";
            using (reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    itemQueueItemCode[x] = int.Parse(reader["ItemCode"].ToString());
                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @itemcode AND UnitMeasurement = @unit";
                    MySqlParameter itemCodeParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                    MySqlParameter unitParam = new MySqlParameter("@unit", MySqlDbType.Int32);
                    itemCodeParam.Value = itemQueueItemCode[x];
                    unitParam.Value = 1;
                    cmd2.Parameters.Add(itemCodeParam);
                    cmd2.Parameters.Add(unitParam);
                    using (reader2 = cmd2.ExecuteReader())
                    {
                        if(reader2.Read())
                        {
                            itemQueueQuantity[x] = double.Parse(reader2["Quantity"].ToString());
                        }
                    }
                    x++;
                }
            }
            conn.Close();
            dateLbl.Text = dn.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                string message = "Item Code must be provided. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    txtItemCode.Clear();
                }
            }
            else
            {
                if (hasSpecialChar(txtItemCode.Text) || hasLetter(txtItemCode.Text))
                {
                    string message = "Item Code must not have Special Character and Letters. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        txtItemCode.Clear();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        string message = "Quantity must be provided. Try Again!";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                        if (result == DialogResult.OK)
                        {
                            txtQuantity.Clear();
                        }
                    }
                    else
                    {
                        if (hasSpecialChar(txtQuantity.Text) || hasLetter(txtQuantity.Text) || int.Parse(txtQuantity.Text) == 0) 
                        {
                            string message = "Quantity must not have Special Character, Letters and Zero. Try Again!";
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                            if (result == DialogResult.OK)
                            {
                                txtQuantity.Clear();
                            }
                        }
                        else
                        {
                            conn = new MySqlConnection(connectionString);
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            MySqlCommand cmd2 = new MySqlCommand();
                            cmd.Connection = conn;
                            MySqlDataReader reader;

                            int itemcode = int.Parse(txtItemCode.Text);
                            List<int> itemlist = new List<int>();
                            cmd.CommandText = "SELECT * FROM Items";
                            using (reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    itemlist.Add(int.Parse(reader["ItemCode"].ToString()));
                                }
                            }

                            if (itemlist.Contains(itemcode))
                            {
                                //conn = new MySqlConnection(connectionString);
                                //conn.Open();
                                //MySqlCommand cmd = new MySqlCommand();
                                //MySqlDataReader reader;
                                //cmd.Connection = conn;
                                //cmd.CommandText = "SELECT * FROM Items WHERE ItemCode = @itemcode";
                                cmd.CommandText = "SELECT * FROM stocks WHERE ItemCode = @itemcode";
                                MySqlParameter itemParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                                itemParam.Value = int.Parse(txtItemCode.Text);
                                cmd.Parameters.Add(itemParam);
                                using (reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        if (cbxUnit.Text == "1")
                                            unitMeasurement = 1;
                                        if (cbxUnit.Text == "1/2")
                                            unitMeasurement = 0.50;
                                        if (cbxUnit.Text == "1/4")
                                            unitMeasurement = 0.25;
                                        if (cbxUnit.Text == "1/8")
                                            unitMeasurement = 0.125;
                                        for (int x = 0; x < itemQueueItemCode.Length; x++)
                                        {
                                            if (int.Parse(txtItemCode.Text) == itemQueueItemCode[x])
                                            {
                                                currentItemCode = itemQueueItemCode[x];
                                                currentQuantity = itemQueueQuantity[x] / unitMeasurement;
                                                currentItemIndex = x;
                                            }
                                        }
                                        if (currentQuantity >= int.Parse(txtQuantity.Text))
                                        {
                                            itemQueueQuantity[currentItemIndex] -= (int.Parse(txtQuantity.Text) * unitMeasurement);
                                            unitMeasurement = 0;
                                            reader.Close();
                                            cmd.CommandText = "SELECT * FROM Items WHERE ItemCode = @ic";
                                            MySqlParameter icParam = new MySqlParameter("@ic", MySqlDbType.Int32);
                                            icParam.Value = itemParam.Value;
                                            cmd.Parameters.Add(icParam);

                                            reader = cmd.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                if (int.Parse(reader["ItemCode"].ToString()) == int.Parse(txtItemCode.Text))
                                                {
                                                    items.Add(reader["ItemCode"]);
                                                    quantity.Add(int.Parse(txtQuantity.Text));
                                                    dataGridView1.Rows.Add(itemNumber, reader["ItemName"], reader["ItemSellPrice"], reader["ItemUnit"], cbxUnit.Text, txtQuantity.Text, (double.Parse(reader["ItemSellPrice"].ToString()) * double.Parse(txtQuantity.Text)).ToString("F2"));
                                                    itemNumber++;
                                                    total += double.Parse(reader["ItemSellPrice"].ToString()) * double.Parse(txtQuantity.Text);
                                                    moneyLbl.Text = total.ToString("F2");
                                                    txtTotalQuantity.Text = "" + ((int.Parse(txtTotalQuantity.Text) + int.Parse(txtQuantity.Text)));
                                                    double subtotal = int.Parse(txtQuantity.Text) * double.Parse(reader["ItemSellPrice"].ToString());
                                                    txtSubtotal.Text = (double.Parse(txtSubtotal.Text) + subtotal).ToString("F2");
                                                    txtItemCode.Text = "";
                                                    txtQuantity.Text = "";
                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Inputted quantity exceeds remaining stocks.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                            else
                                MessageBox.Show("Inputted Item Code does not match any existing item.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            /*MySqlParameter itemParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
            itemParam.Value = int.Parse(textBox1.Text);
            cmd.Parameters.Add(itemParam);
            using (reader = cmd.ExecuteReader())
            {
                if(reader.Read())
                {
                    if(int.Parse(reader["ItemCode"].ToString()) == int.Parse(textBox1.Text))
                    {
                        cmd.CommandText = "SELECT * FROM stocks WHERE ItemCode = @ic";
                        MySqlParameter icParam = new MySqlParameter("@ic", MySqlDbType.Int32);
                        icParam.Value = int.Parse(textBox1.Text);
                        cmd.Parameters.Add(icParam);
                        reader.Close();
                        reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                if (int.Parse(reader["Quantity"].ToString()) <= int.Parse(textBox2.Text))
                                {
                                    items.Add(reader["ItemCode"]);
                                    quantity.Add(int.Parse(textBox2.Text));
                                    dataGridView1.Rows.Add(itemNumber, reader["ItemName"], reader["ItemPrice"], textBox2.Text);
                                    itemNumber++;
                                    total += double.Parse(reader["ItemPrice"].ToString()) * double.Parse(textBox2.Text);
                                    moneyLbl.Text = total.ToString();
                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Test");
                                }
                            }
                        
                    }
                    else
                    {
                        //EmployeeForm ef = new EmployeeForm();
                        //ef.ShowDialog();
                    }
                }
                else
                {
                    //EmployeeForm ef = new EmployeeForm();
                    //ef.ShowDialog();
                }
            }*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtItemCode.Clear();
            txtQuantity.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            txtDiscount.Clear();
            dataGridView1.Rows.Clear();
            items.Clear();
            quantity.Clear();
            itemNumber = 1;
            total = 0;
            txtTotalQuantity.Text = "0";
            txtSubtotal.Text = "00.00";
            moneyLbl.Text = "00.00";
            discountLbl.Text = "00.00";
            txtSubtotal.Text = "00.00";
            txtTotalQuantity.Text = "0";
            comboBox2.SelectedItem = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            cmd.Connection = conn2;
            double measurement = 0;
            int itemIndex = 0;
            itemNumber--;
            txtTotalQuantity.Text = "0";
            txtSubtotal.Text = "00.00";
            if (dataGridView1.Rows.Count > 0)
            {
                int index = dataGridView1.SelectedCells[0].RowIndex;
                cmd.CommandText = "SELECT * FROM Items WHERE ItemName = @itemname";
                MySqlParameter itemNameParam = new MySqlParameter("@itemname", MySqlDbType.Text);
                itemNameParam.Value = dataGridView1.Rows[index].Cells["ItemNa"].Value.ToString();
                cmd.Parameters.Add(itemNameParam);
                using (reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                        for(int x = 0; x < itemQueueItemCode.Length; x++)
                        {
                            if (Globals.itemcode == itemQueueItemCode[x])
                                itemIndex = x;
                        }
                        reader.Close();
                        cmd.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @itemcode AND UnitMeasurement ";
                        MySqlParameter itemCodeParam = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                        itemCodeParam.Value = Globals.itemcode;
                        cmd.Parameters.Add(itemCodeParam);
                        using (reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                if (dataGridView1.Rows[index].Cells["ItemMeasurement"].Value.ToString() == "1")
                                    measurement = 1;
                                if (dataGridView1.Rows[index].Cells["ItemMeasurement"].Value.ToString() == "1/2")
                                    measurement = 0.50;
                                if (dataGridView1.Rows[index].Cells["ItemMeasurement"].Value.ToString() == "1/4")
                                    measurement = 0.25;
                                if (dataGridView1.Rows[index].Cells["ItemMeasurement"].Value.ToString() == "1/8")
                                    measurement = 0.125;
                                itemQueueQuantity[itemIndex] += double.Parse(dataGridView1.Rows[index].Cells["ItemQuan"].Value.ToString()) * measurement;
                            }
                        }
                    }
                }
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
                    double subtotal = int.Parse(dataGridView1.Rows[i].Cells["ItemQuan"].Value.ToString()) * double.Parse(dataGridView1.Rows[i].Cells["ItemPrice"].Value.ToString());
                    txtSubtotal.Text = (double.Parse(txtSubtotal.Text) + subtotal).ToString("F2");
                }
                
                //moneyLbl.Text = (double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)).ToString("F2");
                if (txtDiscount.Text != "")
                {
                    discountLbl.Text = (double.Parse(txtSubtotal.Text) * (double.Parse(txtDiscount.Text) / 100)).ToString("F2");
                    moneyLbl.Text = (double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)).ToString("F2");
                }
                else
                {
                    moneyLbl.Text = double.Parse(txtSubtotal.Text).ToString("F2");
                }
            }
            else
            {
                MessageBox.Show("No items are on queue.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double salesMeasurement = 0;
            if (dataGridView1.Rows.Count != 0)
            {
                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    string message = "Customer Name must be provided. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        textBox4.Clear();
                    }
                }
                else
                {
                    if (hasNumber(textBox4.Text) || hasSpecialChar(textBox4.Text))
                    {
                        string message = "Customer Name must not have Special Character and Numbers. Try Again!";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                        if (result == DialogResult.OK)
                        {
                            textBox4.Clear();
                        }
                    }
                    else
                    {
                        if (hasSpecialChar(txtDiscount.Text) || hasLetter(txtDiscount.Text))
                        {
                            string message = "Applicable Discount must not have Special Character and Letter. Try Again!";
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                            if (result == DialogResult.OK)
                            {
                                txtDiscount.Clear();
                            }
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
                                    comboBox2.ResetText();
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtDiscount.Text) || int.Parse(txtDiscount.Text) == 0)
                                {
                                    discount = 0;
                                    txtDiscount.Text = discount.ToString();
                                }
                                else
                                {
                                    discount = int.Parse(txtDiscount.Text);
                                }
                                string customerName = textBox4.Text;
                                string customerContact = textBox6.Text;
                                string customerEmail = textBox7.Text;
                                string customerAddress = textBox8.Text;
                                int checkCount = 0;
                                conn = new MySqlConnection(connectionString);
                                conn.Open();
                                MySqlCommand cmd2 = new MySqlCommand();
                                cmd2.Connection = conn;
                                //Checking Customer Contact
                                cmd2.CommandText = "SELECT* FROM Customers WHERE CustomerName = @checkContact";
                                MySqlParameter checkContactParam = new MySqlParameter("@checkContact", MySqlDbType.Text);
                                checkContactParam.Value = textBox4.Text;
                                cmd2.Parameters.Add(checkContactParam);
                                using (MySqlDataReader reader = cmd2.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        if (reader["CustomerContactNumber"].ToString() == textBox6.Text)
                                        {
                                            textBox6.Text = reader["CustomerContactNumber"].ToString();
                                        }
                                        else
                                        {
                                            string message = "Customer Contact is not the same in the record. Update it?";
                                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                                            DialogResult result;

                                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                                reader.Close();
                                                cmd2.Parameters.Clear();
                                                cmd2.CommandText = "UPDATE Customers SET CustomerContactNumber = @inputContact WHERE CustomerName = @readerCusName";
                                                MySqlParameter inputContactParam = new MySqlParameter("@inputContact", MySqlDbType.Text);
                                                MySqlParameter readerCusNameParam = new MySqlParameter("@readerCusName", MySqlDbType.Text);
                                                inputContactParam.Value = textBox6.Text;
                                                readerCusNameParam.Value = textBox4.Text;
                                                cmd2.Parameters.Add(inputContactParam);
                                                cmd2.Parameters.Add(readerCusNameParam);
                                                cmd2.Prepare();
                                                cmd2.ExecuteNonQuery();
                                            }
                                            if (result == DialogResult.No)
                                            {
                                                textBox6.Text = reader["CustomerContactNumber"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        reader.Close();
                                        customerName = textBox4.Text;
                                        customerContact = textBox6.Text;
                                        customerEmail = textBox7.Text;
                                        customerAddress = textBox8.Text;
                                    }

                                }
                                //Checking Customer Email
                                cmd2.CommandText = "SELECT * FROM Customers WHERE CustomerName = @checkEmail";
                                MySqlParameter checkEmailParam = new MySqlParameter("@checkEmail", MySqlDbType.Text);
                                checkEmailParam.Value = textBox4.Text;
                                cmd2.Parameters.Add(checkEmailParam);
                                using (MySqlDataReader reader = cmd2.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        if (reader["CustomerEmail"].ToString() == textBox7.Text)
                                        {
                                            textBox7.Text = reader["CustomerEmail"].ToString();
                                        }
                                        else
                                        {
                                            string message = "Customer Email is not the same in the record. Update it?";
                                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                                            DialogResult result;

                                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                                reader.Close();
                                                cmd2.Parameters.Clear();
                                                cmd2.CommandText = "UPDATE Customers SET CustomerEmail = @inputEmail WHERE CustomerName = @readerCusName";
                                                MySqlParameter inputEmailParam = new MySqlParameter("@inputEmail", MySqlDbType.Text);
                                                MySqlParameter readerCusNameParam = new MySqlParameter("@readerCusName", MySqlDbType.Text);
                                                inputEmailParam.Value = textBox7.Text;
                                                readerCusNameParam.Value = textBox4.Text;
                                                cmd2.Parameters.Add(inputEmailParam);
                                                cmd2.Parameters.Add(readerCusNameParam);
                                                cmd2.Prepare();
                                                cmd2.ExecuteNonQuery();
                                            }
                                            if (result == DialogResult.No)
                                            {
                                                textBox7.Text = reader["CustomerEmail"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        reader.Close();
                                        customerName = textBox4.Text;
                                        customerContact = textBox6.Text;
                                        customerEmail = textBox7.Text;
                                        customerAddress = textBox8.Text;
                                    }

                                }
                                //Checking Customer Address
                                cmd2.CommandText = "SELECT * FROM Customers WHERE CustomerName = @checkAddress";
                                MySqlParameter checkAddressParam = new MySqlParameter("@checkAddress", MySqlDbType.Text);
                                checkAddressParam.Value = textBox4.Text;
                                cmd2.Parameters.Add(checkAddressParam);
                                using (MySqlDataReader reader = cmd2.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        if (reader["CustomerAddress"].ToString() == textBox8.Text)
                                        {
                                            textBox8.Text = reader["CustomerAddress"].ToString();
                                        }
                                        else
                                        {
                                            string message = "Customer Address is not the same in the record. Update it?";
                                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                                            DialogResult result;

                                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                                reader.Close();
                                                cmd2.Parameters.Clear();
                                                cmd2.CommandText = "UPDATE Customers SET CustomerAddress = @inputAddress WHERE CustomerName = @readerCusName";
                                                MySqlParameter inputAddressParam = new MySqlParameter("@inputAddress", MySqlDbType.Text);
                                                MySqlParameter readerCusNameParam = new MySqlParameter("@readerCusName", MySqlDbType.Text);
                                                inputAddressParam.Value = textBox8.Text;
                                                readerCusNameParam.Value = textBox4.Text;
                                                cmd2.Parameters.Add(inputAddressParam);
                                                cmd2.Parameters.Add(readerCusNameParam);
                                                cmd2.Prepare();
                                                cmd2.ExecuteNonQuery();
                                            }
                                            if (result == DialogResult.No)
                                            {
                                                textBox8.Text = reader["CustomerAddress"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        reader.Close();
                                        customerName = textBox4.Text;
                                        customerContact = textBox6.Text;
                                        customerEmail = textBox7.Text;
                                        customerAddress = textBox8.Text;
                                    }

                                }
                                //Check Customer Name
                                cmd2.CommandText = "SELECT * FROM Customers WHERE CustomerName = @checkName";
                                MySqlParameter checkNameParam = new MySqlParameter("@checkName", MySqlDbType.Text);
                                checkNameParam.Value = textBox4.Text;
                                cmd2.Parameters.Add(checkNameParam);
                                using (MySqlDataReader reader = cmd2.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        Globals.customerid = int.Parse(reader["CustomerId"].ToString());
                                        reader.Close();
                                    }
                                    else
                                    {
                                        reader.Close();
                                        cmd2.CommandText = "INSERT INTO Customers(CustomerName,CustomerEmail,CustomerContactNumber,CustomerAddress) VALUES (@InputCusName,@InputCusEmail,@InputCusContact,@InputCusAddress)";
                                        MySqlParameter inputCusNameParam = new MySqlParameter("@InputCusName", MySqlDbType.Text);
                                        MySqlParameter inputCusEmailParam = new MySqlParameter("@InputCusEmail", MySqlDbType.Text);
                                        MySqlParameter inputCusContactParam = new MySqlParameter("@InputCusContact", MySqlDbType.Text);
                                        MySqlParameter inputCusAddressParam = new MySqlParameter("@InputCusAddress", MySqlDbType.Text);
                                        inputCusNameParam.Value = textBox4.Text;
                                        inputCusEmailParam.Value = textBox7.Text;
                                        inputCusContactParam.Value = textBox6.Text;
                                        inputCusAddressParam.Value = textBox8.Text;
                                        cmd2.Parameters.Add(inputCusNameParam);
                                        cmd2.Parameters.Add(inputCusEmailParam);
                                        cmd2.Parameters.Add(inputCusContactParam);
                                        cmd2.Parameters.Add(inputCusAddressParam);
                                        cmd2.Prepare();
                                        cmd2.ExecuteNonQuery();
                                    }
                                }

                                //Get Customer Id
                                cmd2.CommandText = "SELECT * FROM Customers WHERE CustomerName = @getCustomerId";
                                MySqlParameter getCusParam = new MySqlParameter("@getCustomerId", MySqlDbType.Text);
                                getCusParam.Value = textBox4.Text;
                                cmd2.Parameters.Add(getCusParam);
                                using (MySqlDataReader reader = cmd2.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        Globals.customerid = int.Parse(reader["CustomerId"].ToString());
                                        reader.Close();
                                    }
                                    else
                                    {
                                        reader.Close();
                                        Globals.result = "Failed to get Customer Id";
                                        MessageBox.Show("Failed to get Customer Id", "Result");
                                    }
                                }

                                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                                {
                                    MySqlCommand cmd = new MySqlCommand();
                                    MySqlDataReader reader;
                                    cmd.Connection = conn;
                                    cmd.CommandText = "SELECT * FROM Items WHERE ItemName = @itemname";
                                    MySqlParameter itemParam = new MySqlParameter("@itemname", MySqlDbType.Text);
                                    itemParam.Value = dataGridView1.Rows[x].Cells["ItemNa"].Value.ToString();
                                    cmd.Parameters.Add(itemParam);
                                    using (reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                            reader.Close();
                                            cmd.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @quanIC AND UnitMeasurement = @quanUM";
                                            MySqlParameter quanICParam = new MySqlParameter("@quanIC", MySqlDbType.Int32);
                                            MySqlParameter quanUMParam = new MySqlParameter("@quanUM", MySqlDbType.Text);
                                            quanICParam.Value = Globals.itemcode;
                                            quanUMParam.Value = dataGridView1.Rows[x].Cells["ItemMeasurement"].Value.ToString();
                                            cmd.Parameters.Add(quanICParam);
                                            cmd.Parameters.Add(quanUMParam);
                                            using (reader = cmd.ExecuteReader())
                                            {
                                                if (reader.Read())
                                                {
                                                    if (double.Parse(reader["Quantity"].ToString()) >= double.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString()))
                                                    {
                                                        Globals.quantity = double.Parse(reader["Quantity"].ToString()) - double.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString());
                                                        reader.Close();
                                                        if (Globals.quantity >= 0)
                                                        {
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                    }
                                                }
                                                else
                                                {
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //EmployeeForm ef = new EmployeeForm();
                                            //ef.ShowDialog();
                                        }
                                    }
                                }
                                if (checkCount == items.Count)
                                {
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = conn;
                                    MySqlDataReader reader;
                                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "SELECT * FROM Items WHERE ItemName = @upIN";
                                        MySqlParameter upINParam = new MySqlParameter("@upIN", MySqlDbType.Text);
                                        upINParam.Value = dataGridView1.Rows[x].Cells["ItemNa"].Value.ToString();
                                        cmd.Parameters.Add(upINParam);
                                        using (reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                                reader.Close();
                                                cmd.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @stockIC AND UnitMeasurement = @stockUnit";
                                                MySqlParameter stockICParam = new MySqlParameter("@stockIC", MySqlDbType.Int32);
                                                MySqlParameter stockUnitParam = new MySqlParameter("@stockUnit", MySqlDbType.Text);
                                                stockICParam.Value = Globals.itemcode;
                                                stockUnitParam.Value = "1";
                                                cmd.Parameters.Add(stockICParam);
                                                cmd.Parameters.Add(stockUnitParam);
                                                using (reader = cmd.ExecuteReader())
                                                {
                                                    if (reader.Read())
                                                    {
                                                        if (dataGridView1.Rows[x].Cells["ItemMeasurement"].Value.ToString() == "1")
                                                            salesMeasurement = 1;
                                                        if (dataGridView1.Rows[x].Cells["ItemMeasurement"].Value.ToString() == "1/2")
                                                            salesMeasurement = 0.50;
                                                        if (dataGridView1.Rows[x].Cells["ItemMeasurement"].Value.ToString() == "1/4")
                                                            salesMeasurement = 0.25;
                                                        if (dataGridView1.Rows[x].Cells["ItemMeasurement"].Value.ToString() == "1/8")
                                                            salesMeasurement = 0.125;

                                                        if (double.Parse(reader["Quantity"].ToString()) >= (double.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString()) * salesMeasurement))
                                                        {
                                                            Globals.quantity = double.Parse(reader["Quantity"].ToString()) - (int.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString()) * salesMeasurement);
                                                            reader.Close();
                                                            if (Globals.quantity >= 0)
                                                            {
                                                                if (Globals.quantity == 0)
                                                                {
                                                                    for (int y = 0; y < availFraction.Length; y++)
                                                                    {
                                                                        string stockStatus = "Out-Of-Stock";
                                                                        cmd.Parameters.Clear();
                                                                        cmd.CommandText = "UPDATE Stocks SET Quantity = @stockQuantity, Status = @stockStatus WHERE ItemCode = @upQuanIC AND UnitMeasurement = @upQuanUM";
                                                                        MySqlParameter stockQuanParam = new MySqlParameter("@stockQuantity", MySqlDbType.Double);
                                                                        MySqlParameter stockStatusParam = new MySqlParameter("@stockStatus", MySqlDbType.String);
                                                                        MySqlParameter upQuanParam = new MySqlParameter("@upQuanIC", MySqlDbType.Int32);
                                                                        MySqlParameter upQuanUMParam = new MySqlParameter("@upQuanUM", MySqlDbType.Text);
                                                                        stockQuanParam.Value = Globals.quantity / availFractionValue[y];
                                                                        stockStatusParam.Value = stockStatus;
                                                                        upQuanParam.Value = Globals.itemcode;
                                                                        upQuanUMParam.Value = availFraction[y];
                                                                        cmd.Parameters.Add(stockQuanParam);
                                                                        cmd.Parameters.Add(stockStatusParam);
                                                                        cmd.Parameters.Add(upQuanParam);
                                                                        cmd.Parameters.Add(upQuanUMParam);
                                                                        cmd.Prepare();
                                                                        if ((int)cmd.ExecuteNonQuery() != 0)
                                                                        {
                                                                        }
                                                                        else
                                                                        {
                                                                            //EmployeeForm ef = new EmployeeForm();
                                                                            //ef.ShowDialog();
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    for (int y = 0; y < availFraction.Length; y++)
                                                                    {
                                                                        cmd.Parameters.Clear();
                                                                        cmd.CommandText = "UPDATE Stocks SET Quantity = @stockQuantity WHERE ItemCode = @upQuanIC AND UnitMeasurement = @upQuanUM";
                                                                        MySqlParameter stockQuanParam = new MySqlParameter("@stockQuantity", MySqlDbType.Double);
                                                                        MySqlParameter upQuanParam = new MySqlParameter("@upQuanIC", MySqlDbType.Int32);
                                                                        MySqlParameter upQuanUMParam = new MySqlParameter("@upQuanUM", MySqlDbType.Text);
                                                                        stockQuanParam.Value = Globals.quantity / availFractionValue[y];
                                                                        upQuanParam.Value = Globals.itemcode;
                                                                        upQuanUMParam.Value = availFraction[y];
                                                                        cmd.Parameters.Add(stockQuanParam);
                                                                        cmd.Parameters.Add(upQuanParam);
                                                                        cmd.Parameters.Add(upQuanUMParam);
                                                                        cmd.Prepare();
                                                                        if ((int)cmd.ExecuteNonQuery() != 0)
                                                                        {
                                                                        }
                                                                        else
                                                                        {
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                            }
                                                        }
                                                        else
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                    }
                                                }
                                            }
                                            else
                                            {
                                            }
                                        }
                                        reader.Close();
                                    }
                                    string type = "Sales";
                                    cmd.CommandText = "INSERT INTO Transactions(TotalAmount,TransType,Date) VALUES(@total,@type,(SELECT NOW()))";
                                    MySqlParameter totalParam = new MySqlParameter("@total", MySqlDbType.Double);
                                    MySqlParameter typeParam = new MySqlParameter("@type", MySqlDbType.Text);
                                    MySqlParameter dateParam = new MySqlParameter("date", MySqlDbType.DateTime);
                                    totalParam.Value = double.Parse(moneyLbl.Text);
                                    typeParam.Value = type;
                                    dateParam.Value = dn.ToString("yyyy-MM-dd HH:mm:ss");
                                    cmd.Parameters.Add(totalParam);
                                    cmd.Parameters.Add(typeParam);
                                    cmd.Parameters.Add(dateParam);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    cmd.CommandText = "SELECT MAX(TransId) FROM Transactions";
                                    bool itsTrue = true;
                                    Globals.transid = int.Parse(cmd.ExecuteScalar().ToString());
                                    cmd.CommandText = "SELECT COUNT(*) FROM Sales";
                                    if (int.Parse(cmd.ExecuteScalar().ToString()) == 0)
                                    {
                                        Globals.salesinvoicenum = 1;
                                    }
                                    else
                                    {
                                        cmd.CommandText = "SELECT MAX(SalesInvoiceNumber) FROM Sales";
                                        Globals.salesinvoicenum = int.Parse(cmd.ExecuteScalar().ToString()) + 1;
                                    }
                                    for (int x = 0; x < checkCount; x++)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandText = "SELECT * FROM Items WHERE ItemName = @itemname";
                                        MySqlParameter itemNParam = new MySqlParameter("@itemname", MySqlDbType.Text);
                                        itemNParam.Value = dataGridView1.Rows[x].Cells["ItemNa"].Value.ToString();
                                        cmd.Parameters.Add(itemNParam);
                                        using (reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                                Globals.itemprice = double.Parse(reader["ItemPrice"].ToString());
                                                reader.Close();

                                                string salStatus = "Paid";
                                                if (comboBox2.Text == "Cash" || comboBox2.Text == "Check")
                                                {
                                                    salStatus = "Paid";
                                                }
                                                else
                                                {
                                                    salStatus = "Unpaid";
                                                }
                                                cmd.Parameters.Clear();
                                                cmd.CommandText = "INSERT INTO Sales(ItemCode,ItemSellPrice,Quantity,TransId,CustomerId,PaymentType,ApplicableDiscount,SalesInvoiceNumber,UnitMeasurement,EmployeeId,Status) VALUES(@salIC,@salIP,@salQuan,@salTID,@salCusId,@salPayType,@salAppDis,@salInvoiceNum,@salUnit,@salEmpId,@salStatus)";
                                                MySqlParameter salICParam = new MySqlParameter("@salIC", MySqlDbType.Int32);
                                                MySqlParameter salIPParam = new MySqlParameter("@salIP", MySqlDbType.Int32);
                                                MySqlParameter salQuanParam = new MySqlParameter("@salQuan", MySqlDbType.Int32);
                                                MySqlParameter salTIDParam = new MySqlParameter("@salTID", MySqlDbType.Int32);
                                                MySqlParameter salCusIdParam = new MySqlParameter("@salCusId", MySqlDbType.Int32);
                                                MySqlParameter salPayTypeParam = new MySqlParameter("@salPayType", MySqlDbType.Text);
                                                MySqlParameter salAppDisParam = new MySqlParameter("@salAppDis", MySqlDbType.Int32);
                                                MySqlParameter salInvoiceNumParam = new MySqlParameter("@salInvoiceNum", MySqlDbType.Int32);
                                                MySqlParameter salUnitParam = new MySqlParameter("@salUnit", MySqlDbType.Text);
                                                MySqlParameter salEmpIdParam = new MySqlParameter("@salEmpId", MySqlDbType.Int32);
                                                MySqlParameter salStatusParam = new MySqlParameter("@salStatus", MySqlDbType.String);
                                                salICParam.Value = Globals.itemcode;
                                                salIPParam.Value = Globals.itemprice;
                                                salQuanParam.Value = int.Parse(dataGridView1.Rows[x].Cells["ItemQuan"].Value.ToString());
                                                salTIDParam.Value = Globals.transid;
                                                salCusIdParam.Value = Globals.customerid;
                                                salPayTypeParam.Value = comboBox2.Text;
                                                salAppDisParam.Value = discount;
                                                salInvoiceNumParam.Value = Globals.salesinvoicenum;
                                                salUnitParam.Value = dataGridView1.Rows[x].Cells["ItemMeasurement"].Value.ToString();
                                                salEmpIdParam.Value = Globals.employeeid;
                                                salStatusParam.Value = salStatus;
                                                cmd.Parameters.Add(salICParam);
                                                cmd.Parameters.Add(salIPParam);
                                                cmd.Parameters.Add(salQuanParam);
                                                cmd.Parameters.Add(salTIDParam);
                                                cmd.Parameters.Add(salCusIdParam);
                                                cmd.Parameters.Add(salPayTypeParam);
                                                cmd.Parameters.Add(salAppDisParam);
                                                cmd.Parameters.Add(salInvoiceNumParam);
                                                cmd.Parameters.Add(salUnitParam);
                                                cmd.Parameters.Add(salEmpIdParam);
                                                cmd.Parameters.Add(salStatusParam);
                                                cmd.Prepare();
                                                if ((int)cmd.ExecuteNonQuery() != 0)
                                                {
                                                    if (comboBox2.Text == "Receivable" || comboBox2.Text == "COD")
                                                    {
                                                        cmd.CommandText = "SELECT * FROM Sales WHERE TransId = @reTransId AND SalesInvoiceNumber = @reSalesInv";
                                                        MySqlParameter reTransIdParam = new MySqlParameter("@reTransId", MySqlDbType.Int32);
                                                        MySqlParameter reSalesInvParam = new MySqlParameter("@reSalesInv", MySqlDbType.Int32);
                                                        reTransIdParam.Value = Globals.transid;
                                                        reSalesInvParam.Value = Globals.salesinvoicenum;
                                                        cmd.Parameters.Add(reTransIdParam);
                                                        cmd.Parameters.Add(reSalesInvParam);
                                                        using (reader = cmd.ExecuteReader())
                                                        {
                                                            if (reader.Read())
                                                            {
                                                                int addFeeValue = 0;
                                                                if (comboBox2.Text == "COD")
                                                                    addFeeValue = int.Parse(txtShipFee.Text);
                                                                else
                                                                    addFeeValue = 0;
                                                                Globals.salesid = int.Parse(reader["SalesId"].ToString());
                                                                string receiStatus = "Unpaid";
                                                                reader.Close();
                                                                cmd.CommandText = "INSERT INTO Receivables(AdditionalFee,SalesId,Status) VALUES (@addfee,@receiSalesId,@receiStatus)";
                                                                MySqlParameter receiAddFeeParam = new MySqlParameter("@addfee", MySqlDbType.Int32);
                                                                MySqlParameter receiSalesIdParam = new MySqlParameter("@receiSalesId", MySqlDbType.Int32);
                                                                MySqlParameter receiStatusParam = new MySqlParameter("@receiStatus", MySqlDbType.String);
                                                                receiAddFeeParam.Value = addFeeValue;
                                                                receiSalesIdParam.Value = Globals.salesid;
                                                                receiStatusParam.Value = receiStatus;
                                                                cmd.Parameters.Add(receiAddFeeParam);
                                                                cmd.Parameters.Add(receiSalesIdParam);
                                                                cmd.Parameters.Add(receiStatusParam);
                                                                cmd.Prepare();
                                                                if ((int)cmd.ExecuteNonQuery() != 0)
                                                                {
                                                                    Globals.result = "Success";
                                                                    //DialogResult result = MessageBox.Show("Success.", "Result");
                                                                    //if (result == DialogResult.OK)
                                                                    //{

                                                                    itsTrue = true;

                                                                    //}
                                                                    //EmployeeForm ef = new EmployeeForm();
                                                                    //ef.Show();
                                                                }
                                                                else
                                                                {
                                                                    itsTrue = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //EmployeeForm ef = new EmployeeForm();
                                                    //ef.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                //EmployeeForm ef = new EmployeeForm();
                                                //ef.ShowDialog();
                                            }
                                        }
                                    }
                                    if (itsTrue)
                                    {
                                        MessageBox.Show("Success.", "Result");
                                        dataGridView1.Rows.Clear();
                                        items.Clear();
                                        quantity.Clear();
                                        textBox4.Clear();
                                        textBox6.Clear();
                                        textBox7.Clear();
                                        textBox8.Clear();
                                        txtDiscount.ReadOnly = false;
                                        txtDiscount.Clear();
                                        itemNumber = 1;
                                        total = 0;
                                        moneyLbl.Text = "00.00";
                                        txtSubtotal.Text = "00.00";
                                        txtTotalQuantity.Text = "0";
                                        comboBox2.SelectedItem = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Transaction Failed.", "Result");
                                        dataGridView1.Rows.Clear();
                                        items.Clear();
                                        quantity.Clear();
                                        textBox4.Clear();
                                        textBox6.Clear();
                                        textBox7.Clear();
                                        textBox8.Clear();
                                        txtDiscount.ReadOnly = false;
                                        txtDiscount.Clear();
                                        itemNumber = 1;
                                        total = 0;
                                        moneyLbl.Text = "00.00";
                                        txtSubtotal.Text = "00.00";
                                        txtTotalQuantity.Text = "0";
                                        comboBox2.SelectedItem = null;
                                    }
                                }
                                else
                                {
                                    //EmployeeForm ef = new EmployeeForm();
                                    //ef.ShowDialog();
                                }
                            }
                        }
                        if (Globals.result == "Success")
                        {
                            dataGridView1.Rows.Clear();
                            items.Clear();
                            quantity.Clear();
                            textBox4.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                            textBox8.Clear();
                            txtDiscount.ReadOnly = false;
                            txtDiscount.Clear();
                            itemNumber = 1;
                            total = 0;
                            moneyLbl.Text = "00.00";
                            Globals.result = "";
                            txtSubtotal.Text = "00.00";
                            txtTotalQuantity.Text = "0";
                            txtShipFee.Enabled = false;
                            comboBox2.SelectedIndex = -1;
                            comboBox2.SelectedItem = null;
                        }
                    }
                }
            }
            else
            {
                string message = "Item Queue is empty. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    txtItemCode.Clear();
                    txtQuantity.Clear();
                    textBox4.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    txtDiscount.Clear();
                    dataGridView1.Rows.Clear();
                    items.Clear();
                    quantity.Clear();
                    itemNumber = 1;
                    total = 0;
                    txtTotalQuantity.Text = "0";
                    txtSubtotal.Text = "00.00";
                    moneyLbl.Text = "00.00";
                    discountLbl.Text = "00.00";
                    txtSubtotal.Text = "00.00";
                    txtTotalQuantity.Text = "0";
                    comboBox2.SelectedItem = null;
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
        private void OnMouseEnterButton7(object sender, EventArgs e)
        {
            button7.BackColor = Color.SeaGreen;
            button7.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button7.ForeColor = Color.White;
        }
        private void OnMouseLeaveButton7(object sender, EventArgs e)
        {
            button7.BackColor = Color.FromArgb(229, 245, 242);
            button7.FlatAppearance.BorderColor = Color.SeaGreen;
            button7.ForeColor = Color.Black;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            this.Hide();
            log.ShowDialog();
            this.Show();
        }

        public bool IsValidEmail(string s)
        {
            return new EmailAddressAttribute().IsValid(s);
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

        private void button5_Click(object sender, EventArgs e)
        {
            ItemSearch sr = new ItemSearch();
            sr.ShowDialog();
            this.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            CustomerSearch cs = new CustomerSearch();
            cs.ShowDialog();
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtDiscount.Text))
            {
                string message = "Applicable Discount must be provided. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    txtDiscount.Clear();
                }
            }
            else
            {
                if(hasLetter(txtDiscount.Text) || hasSpecialChar(txtDiscount.Text))
                {
                    string message = "Applicable Discount must not have Special Character and Letters. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        txtDiscount.Clear();
                    }
                }
                else
                {
                    if(int.Parse(txtDiscount.Text) == 0)
                    {
                        string message = "No Discount Applied. Try Again!";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                        if (result == DialogResult.OK)
                        {
                            txtDiscount.Clear();
                        }
                    }
                    else
                    {
                        double discount = total * (double.Parse(txtDiscount.Text) / 100);
                        total -= discount;
                        moneyLbl.Text = total.ToString();
                        txtDiscount.ReadOnly = true;
                    }
                }
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtSearch.ForeColor = Color.Black;
        }

        private void Search()
        {
            conn = new MySqlConnection(connectionString);
            conn2 = new MySqlConnection(connectionString);
            conn.Open();
            conn2.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd.Connection = conn;
            cmd2.Connection = conn2;
            MySqlDataReader reader, reader2;
            dataGridView2.Rows.Clear();
            int quantity;
            cmd.CommandText = "SELECT * FROM items WHERE ItemName like '%" + txtSearch.Text + "%'";
            cmd.Parameters.AddWithValue("@itemname", txtSearch.Text);
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
                                dataGridView2.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemSellPrice"], reader2["Quantity"], reader["ItemUnit"]);

                            }
                        }
                    }
                }
                else
                    //MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexpass = dataGridView2.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView2.Rows[indexpass];
            txtItemCode.Text = selectedRow.Cells["ItemCode"].Value.ToString();

            //if (dataGridView2.SelectedCells.Count > 0)
            //{
            //    int indexpass = dataGridView2.SelectedCells[0].RowIndex;
            //    DataGridViewRow selectedRow = dataGridView2.Rows[indexpass];
            //    try
            //    {
            //        textBox1.Text = selectedRow.Cells["ItemCode"].Value.ToString();
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Selected cell is EMPTY!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            this.Hide();
            log.ShowDialog();
            this.Show();
        }

        private void btnSalesInvoice_MouseEnter(object sender, EventArgs e)
        {
            btnSalesInvoice.BackColor = Color.SeaGreen;
            btnSalesInvoice.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnSalesInvoice.ForeColor = Color.FromArgb(143, 255, 170);
            activeSales.BackColor = Color.LightGreen;
        }

        private void btnSalesInvoice_MouseLeave(object sender, EventArgs e)
        {
            btnSalesInvoice.BackColor = Color.FromArgb(45, 45, 45);
            btnSalesInvoice.FlatAppearance.BorderColor = Color.SeaGreen;
            btnSalesInvoice.ForeColor = Color.White;
            activeSales.BackColor = Color.SeaGreen;
        }

        private void btnPrint_MouseEnter(object sender, EventArgs e)
        {
            btnPrint.BackColor = Color.SeaGreen;
            btnPrint.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnPrint.ForeColor = Color.FromArgb(143, 255, 170);
        }

        private void btnPrint_MouseLeave(object sender, EventArgs e)
        {
            btnPrint.BackColor = Color.FromArgb(45, 45, 45);
            btnPrint.FlatAppearance.BorderColor = Color.SeaGreen;
            btnPrint.ForeColor = Color.White;
        }


        private void btnLogout_MouseEnter(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.SeaGreen;
            btnLogout.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnLogout.ForeColor = Color.FromArgb(143, 255, 170);
        }

        private void btnLogout_MouseLeave(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.FromArgb(45, 45, 45);
            btnLogout.FlatAppearance.BorderColor = Color.SeaGreen;
            btnLogout.ForeColor = Color.White;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            Search();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            activeTab = 1;
            CheckActiveTab();
            MessageBox.Show("Sorry this part is still under development.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSalesInvoice_Click(object sender, EventArgs e)
        {
            activeTab = 0;
            CheckActiveTab();
            panelMain.Controls.Clear();
            panelMain.Controls.Add(panel1);
            panelMain.Controls.Add(panel2);
            panelMain.Controls.Add(panel3);
            panelMain.Controls.Add(panel4);
            panelMain.Controls.Add(panel8);
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            double amount = 0;
            try
            {
                if (txtDiscount.Text != "")
                {
                    if (txtShipFee.Text != "")
                    {
                        discountLbl.Text = (double.Parse(txtSubtotal.Text) * (double.Parse(txtDiscount.Text) / 100)).ToString("F2");
                        amount = (double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)) + double.Parse(txtShipFee.Text);
                        moneyLbl.Text = amount.ToString("F2");
                    }
                    else
                    {
                        discountLbl.Text = (double.Parse(txtSubtotal.Text) * (double.Parse(txtDiscount.Text) / 100)).ToString("F2");
                        moneyLbl.Text = (double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)).ToString("F2");
                    }
                }
                else
                {
                    if (txtShipFee.Text != "")
                    {
                        amount = (double.Parse(txtSubtotal.Text) + double.Parse(txtShipFee.Text));
                        moneyLbl.Text = amount.ToString("F2");
                        discountLbl.Text = "00.00";
                    }
                    else
                    {
                        moneyLbl.Text = double.Parse(txtSubtotal.Text).ToString("F2");
                        discountLbl.Text = "00.00";
                    }
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
                    txtDiscount.Clear();
                }
            }
        }

        private void CheckActiveTab()
        {
            activeSales.Visible = false;


            if (activeTab == 0)
            {
                activeSales.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 2)
            {
                txtShipFee.Enabled = true;
            }
            else
            {
                txtShipFee.Text = "";
                txtShipFee.Enabled = false;
            }
        }

        private void txtShipFee_TextChanged(object sender, EventArgs e)
        {
            double amount = 0;
            try
            {
                if (txtShipFee.Text != "")
                {
                    if (txtDiscount.Text != "")
                    {
                        discountLbl.Text = (double.Parse(txtSubtotal.Text) * (double.Parse(txtDiscount.Text) / 100)).ToString("F2");
                        amount = (double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)) + double.Parse(txtShipFee.Text);
                        moneyLbl.Text = amount.ToString("F2");
                    }
                    else
                    {
                        amount = (double.Parse(txtSubtotal.Text) + double.Parse(txtShipFee.Text));
                        moneyLbl.Text = amount.ToString("F2");
                    }
                }
                else
                {
                    txtShipFee.Text = "";
                    if (txtDiscount.Text != "")
                    {
                        discountLbl.Text = (double.Parse(txtSubtotal.Text) * (double.Parse(txtDiscount.Text) / 100)).ToString("F2");
                        moneyLbl.Text = ((double.Parse(txtSubtotal.Text) - double.Parse(discountLbl.Text)).ToString("F2"));
                    }
                    else
                    {
                        moneyLbl.Text = double.Parse(txtSubtotal.Text).ToString("F2");
                    }
                }
            }
            catch (System.Exception)
            {
                string message = "Shipping Fee must not have Special Character and Letters. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    txtShipFee.Clear();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
