using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication8
{
    
    public partial class RegistrationForm : Form
    {
        
        MySqlConnection conn, conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        string caption = "Invalid Input Detected";
        public RegistrationForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            button1.MouseEnter += OnMouseEnterButton1;
            button1.MouseLeave += OnMouseLeaveButton1;
            button2.MouseEnter += OnMouseEnterButton2;
            button2.MouseLeave += OnMouseLeaveButton2;
            this.positionBox.DropDownStyle = ComboBoxStyle.DropDownList;
            passBox.MouseHover += new EventHandler(textBox1_MouseHover);
            passBox.MouseLeave += new EventHandler(textBox1_MouseLeave);
            tblAccounts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in tblAccounts.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        ToolTip toolTip1 = new ToolTip();
        void textBox1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(passBox);
        }
        void textBox1_MouseHover(object sender, EventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {

                toolTip1.ToolTipTitle = "Caps Lock Is On";
                toolTip1.ToolTipIcon = ToolTipIcon.Warning;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(passBox, "Having Caps Lock on may cause you to enter your password incorrectly.\n\nYou should press Caps Lock to turn it off before entering your password.");
                toolTip1.Show("Having Caps Lock on may cause you to enter your password incorrectly.\n\nYou should press Caps Lock to turn it off before entering your password.", passBox, 5, passBox.Height - 5);
            }
        }
        private void RegistrationForm_Load(object sender, EventArgs e)
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
            tblAccounts.Rows.Clear();
            cmd.CommandText = "SELECT * FROM Accounts";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int accountId = int.Parse(reader["AccountId"].ToString());
                    if (reader["AccountType"].ToString().Equals("Admin"))
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM adminprofile WHERE AccountId = @accountid";
                        MySqlParameter idParam = new MySqlParameter("@accountid", MySqlDbType.Int32);
                        idParam.Value = accountId;
                        cmd2.Parameters.Add(idParam);
                        cmd2.Prepare();
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                tblAccounts.Rows.Add(reader["AccountId"], reader["Username"], reader2["AdminFName"], reader2["AdminLName"], reader["AccountType"], reader["AccountStatus"]);
                            }
                        }
                    }
                    else if (reader["AccountType"].ToString().Equals("Employee"))
                    {
                        cmd2.Parameters.Clear();
                        cmd2.CommandText = "SELECT * FROM employeeprofile WHERE AccountId = @accountid";
                        MySqlParameter idParam = new MySqlParameter("@accountid", MySqlDbType.Int32);
                        idParam.Value = accountId;
                        cmd2.Parameters.Add(idParam);
                        cmd2.Prepare();
                        using (reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                tblAccounts.Rows.Add(reader["AccountId"], reader["Username"], reader2["EmployeeFName"], reader2["EmployeeLName"], reader["AccountType"], reader["AccountStatus"]);
                            }
                        }
                    }
                }
                reader.Close();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        public void Register()
        {
            Globals.status = "Active";
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts WHERE Username = @inUser";
            MySqlParameter inUserParam = new MySqlParameter("@inUser", MySqlDbType.Text);
            inUserParam.Value = userBox.Text;
            cmd.Parameters.Add(inUserParam);
            using (reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string message = "Username already exists. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        userBox.Clear();
                    }
                }
                else
                {
                    reader.Close();
                    cmd.CommandText = "INSERT INTO Accounts(Username,Password,AccountType,AccountStatus) VALUES(@username,@password,@type,@status)";
                    MySqlParameter userParam = new MySqlParameter("@username", MySqlDbType.Text);
                    MySqlParameter passParam = new MySqlParameter("@password", MySqlDbType.Text);
                    MySqlParameter typeParam = new MySqlParameter("@type", MySqlDbType.Text);
                    MySqlParameter statParam = new MySqlParameter("@status", MySqlDbType.Text);
                    userParam.Value = userBox.Text;
                    passParam.Value = passBox.Text;
                    typeParam.Value = positionBox.Text;
                    statParam.Value = Globals.status;
                    cmd.Parameters.Add(userParam);
                    cmd.Parameters.Add(passParam);
                    cmd.Parameters.Add(typeParam);
                    cmd.Parameters.Add(statParam);
                    cmd.Prepare();
                    if ((int)cmd.ExecuteNonQuery() != 0)
                    {
                        cmd.CommandText = "SELECT * FROM Accounts WHERE Username = @username2";
                        MySqlParameter user2Param = new MySqlParameter("@username2", MySqlDbType.Text);
                        user2Param.Value = userBox.Text;
                        cmd.Parameters.Add(user2Param);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader["AccountStatus"].Equals("Active"))
                                {
                                    if (reader["AccountType"].Equals("Admin"))
                                    {
                                        Globals.id = (int)reader["AccountId"];
                                        reader.Close();
                                        cmd.CommandText = "INSERT INTO AdminProfile(AdminFName,AdminLName,AdminAddress,AdminContactNumber,AccountId) VALUES(@fname,@lname,@address,@contact,@id)";
                                        MySqlParameter fnameParam = new MySqlParameter("@fname", MySqlDbType.Text);
                                        MySqlParameter lnameParam = new MySqlParameter("@lname", MySqlDbType.Text);
                                        MySqlParameter addressParam = new MySqlParameter("@address", MySqlDbType.Text);
                                        MySqlParameter contactParam = new MySqlParameter("@contact", MySqlDbType.Text);
                                        MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int32);
                                        fnameParam.Value = firstBox.Text;
                                        lnameParam.Value = lastBox.Text;
                                        addressParam.Value = addressBox.Text;
                                        contactParam.Value = contactBox.Text;
                                        idParam.Value = Globals.id;
                                        cmd.Parameters.Add(fnameParam);
                                        cmd.Parameters.Add(lnameParam);
                                        cmd.Parameters.Add(addressParam);
                                        cmd.Parameters.Add(contactParam);
                                        cmd.Parameters.Add(idParam);
                                        cmd.Prepare();
                                        if (cmd.ExecuteNonQuery() != 0)
                                        {
                                            Globals.result = "Success";
                                            MessageBox.Show("Registration Successful.", "Result");
                                            userBox.Clear();
                                            passBox.Clear();
                                            firstBox.Clear();
                                            lastBox.Clear();
                                            addressBox.Clear();
                                            contactBox.Clear();
                                            positionBox.SelectedItem = null;
                                        }
                                        else
                                        {
                                            Globals.result = "Failed";
                                            MessageBox.Show("Registration Failed.", "Result");
                                            userBox.Clear();
                                            passBox.Clear();
                                            firstBox.Clear();
                                            lastBox.Clear();
                                            addressBox.Clear();
                                            contactBox.Clear();
                                        }
                                    }
                                    else
                                    {
                                        Globals.id = (int)reader["AccountId"];
                                        reader.Close();
                                        cmd.CommandText = "INSERT INTO EmployeeProfile(EmployeeFName,EmployeeLName,EmployeeAddress,EmployeeContactNumber,AccountId) VALUES(@fname,@lname,@address,@contact,@id)";
                                        MySqlParameter fnameParam = new MySqlParameter("@fname", MySqlDbType.Text);
                                        MySqlParameter lnameParam = new MySqlParameter("@lname", MySqlDbType.Text);
                                        MySqlParameter addressParam = new MySqlParameter("@address", MySqlDbType.Text);
                                        MySqlParameter contactParam = new MySqlParameter("@contact", MySqlDbType.Text);
                                        MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int32);
                                        fnameParam.Value = firstBox.Text;
                                        lnameParam.Value = lastBox.Text;
                                        addressParam.Value = addressBox.Text;
                                        contactParam.Value = contactBox.Text;
                                        idParam.Value = Globals.id;
                                        cmd.Parameters.Add(fnameParam);
                                        cmd.Parameters.Add(lnameParam);
                                        cmd.Parameters.Add(addressParam);
                                        cmd.Parameters.Add(contactParam);
                                        cmd.Parameters.Add(idParam);
                                        cmd.Prepare();
                                        if (cmd.ExecuteNonQuery() != 0)
                                        {
                                            Globals.result = "Success";
                                            MessageBox.Show("Registration Successful.", "Result");
                                            userBox.Clear();
                                            passBox.Clear();
                                            firstBox.Clear();
                                            lastBox.Clear();
                                            addressBox.Clear();
                                            contactBox.Clear();
                                            positionBox.SelectedItem = null;
                                        }
                                        else
                                        {
                                            Globals.result = "Failed";
                                            MessageBox.Show("Registration Failed.", "Result");
                                            userBox.Clear();
                                            passBox.Clear();
                                            firstBox.Clear();
                                            lastBox.Clear();
                                            addressBox.Clear();
                                            contactBox.Clear();
                                        }
                                    }
                                }
                                else
                                {
                                    Globals.result = "Account is not activate";
                                    MessageBox.Show("Account is not active.", "Result");
                                    userBox.Clear();
                                    passBox.Clear();
                                    firstBox.Clear();
                                    lastBox.Clear();
                                    addressBox.Clear();
                                    contactBox.Clear();
                                }
                            }
                            else
                            {
                                Globals.result = "Register Failed";
                                MessageBox.Show("Registration Failed.", "Result");
                                userBox.Clear();
                                passBox.Clear();
                                firstBox.Clear();
                                lastBox.Clear();
                                addressBox.Clear();
                                contactBox.Clear();
                            }
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = userBox.Text;
            string password = passBox.Text;
            string first = firstBox.Text;
            string last = lastBox.Text;
            string contact = contactBox.Text;
            string position = positionBox.Text;
            string address = addressBox.Text;
            if (!string.IsNullOrEmpty(username))
            {
                if (hasSpecialChar(username))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        if (hasSpecialChar(password))
                        {
                            if (!string.IsNullOrEmpty(first))
                            {
                                if (hasSpecialChar(first) && hasNumber(first))
                                {
                                    if (!string.IsNullOrEmpty(last))
                                    {
                                        if (hasSpecialChar(last) && hasNumber(last))
                                        {
                                            if (!string.IsNullOrEmpty(contact))
                                            {
                                                if (hasLetter(contact) && hasSpecialChar(contact))
                                                {
                                                    if (!string.IsNullOrEmpty(address))
                                                    {
                                                        if (!string.IsNullOrEmpty(position))
                                                        {
                                                            if (position.Equals("Employee") || position.Equals("Admin"))
                                                            {
                                                                Register();
                                                            }
                                                            else
                                                            {
                                                                string message = "Invalid Position. Try Again!";
                                                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                                DialogResult result;

                                                                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                                if (result == DialogResult.OK)
                                                                {
                                                                    Clear();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string message = "Position must be provided. Try Again!";
                                                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                            DialogResult result;

                                                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                            if (result == DialogResult.OK)
                                                            {
                                                                Clear();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string message = "Address must be provided. Try Again!";
                                                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                        DialogResult result;

                                                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                        if (result == DialogResult.OK)
                                                        {
                                                            Clear();
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string message = "Contact must not have Special Character and Letters. Try Again!";
                                                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                    DialogResult result;

                                                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                    if (result == DialogResult.OK)
                                                    {
                                                        Clear();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string message = "Contact must be provided. Try Again!";
                                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                                DialogResult result;

                                                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                                if (result == DialogResult.OK)
                                                {
                                                    Clear();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string message = "Last Name must not have Special Character and Numbers. Try Again!";
                                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                                            DialogResult result;

                                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                            if (result == DialogResult.OK)
                                            {
                                                Clear();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string message = "Last Name must be provided. Try Again!";
                                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                                        DialogResult result;

                                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                        if (result == DialogResult.OK)
                                        {
                                            Clear();
                                        }
                                    }
                                }
                                else
                                {
                                    string message = "First Name must not have Special Character and Numbers. Try Again!";
                                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                                    DialogResult result;

                                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                    if (result == DialogResult.OK)
                                    {
                                        Clear();
                                    }
                                }
                            }
                            else
                            {
                                string message = "First Name must be provided. Try Again!";
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                DialogResult result;

                                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                                if (result == DialogResult.OK)
                                {
                                    Clear();
                                }
                            }
                        }
                        else
                        {
                            string message = "Password must not have Special Character. Try Again!";
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                            if (result == DialogResult.OK)
                            {
                                Clear();
                            }
                        }
                    }
                    else
                    {
                        string message = "Password must be provided. Try Again!";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                        if (result == DialogResult.OK)
                        {
                            Clear();
                        }
                    }
                }
                else
                {
                    string message = "Username must not have Special Character. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        Clear();
                    }
                }
            }
            else
            {
                string message = "Username must be provided. Try Again!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    Clear();
                }
            }                      
        }

        public void Clear()
        {
            userBox.Clear();
            passBox.Clear();
            firstBox.Clear();
            lastBox.Clear();
            addressBox.Clear();
            contactBox.Clear();
            positionBox.SelectedItem = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        public bool hasSpecialChar(string s)
        {
            int length = s.Length;
            int count = 0;
            char[] test = s.ToCharArray();
            for(int x = 0; x < test.Length; x++)
            {
                if (!Char.IsLetterOrDigit(test[x]))
                    count--;
                else
                    count++;
            }
            if (count == length)
                return true;
            else
                return false;
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
                return true;
            else
                return false;
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
                return true;
            else
                return false;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtAccountIdUpdate.Text == "" || txtUsernameUpdate.Text == "")
                MessageBox.Show("No AccountId and Username is yet selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string accountId = txtAccountIdUpdate.Text, username = txtUsernameUpdate.Text, status = "Deactivated";
                conn = new MySqlConnection(connectionString);
                conn2 = new MySqlConnection(connectionString);
                conn.Open();
                conn2.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd.Connection = conn;
                cmd2.Connection = conn2;
                MySqlDataReader reader;
                cmd.CommandText = "SELECT * FROM accounts WHERE AccountId = @id and Username = @user";
                cmd.Parameters.AddWithValue("@id", accountId);
                cmd.Parameters.AddWithValue("@user", username);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["AccountStatus"].ToString() == "Active")
                        {
                            cmd2.CommandText = "UPDATE accounts SET AccountStatus = @stat Where AccountId = @id and Username = @user";
                            cmd2.Parameters.AddWithValue("@stat", status);
                            cmd2.Parameters.AddWithValue("@id", accountId);
                            cmd2.Parameters.AddWithValue("@user", username);
                            cmd2.ExecuteNonQuery();
                            conn2.Close();
                            MessageBox.Show("Account is successfully deactivated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAccountIdUpdate.Clear();
                            txtUsernameUpdate.Clear();
                            RegistrationForm_Load(this, null);
                        }
                        else
                            MessageBox.Show("Account is already deactivated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    reader.Close();
                }
            }
        }

        private void btnActive_MouseEnter(object sender, EventArgs e)
        {
            btnActive.BackColor = Color.SeaGreen;
            btnActive.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnActive.ForeColor = Color.White;
        }

        private void btnActive_MouseLeave(object sender, EventArgs e)
        {
            btnActive.BackColor = Color.FromArgb(229, 245, 242);
            btnActive.FlatAppearance.BorderColor = Color.SeaGreen;
            btnActive.ForeColor = Color.Black;
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            if (txtAccountIdUpdate.Text == "" || txtUsernameUpdate.Text == "")
                MessageBox.Show("No AccountId and Username is yet selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string accountId = txtAccountIdUpdate.Text, username = txtUsernameUpdate.Text, status = "Active";
                conn = new MySqlConnection(connectionString);
                conn2 = new MySqlConnection(connectionString);
                conn.Open();
                conn2.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd.Connection = conn;
                cmd2.Connection = conn2;
                MySqlDataReader reader;
                cmd.CommandText = "SELECT * FROM accounts WHERE AccountId = @id and Username = @user";
                cmd.Parameters.AddWithValue("@id", accountId);
                cmd.Parameters.AddWithValue("@user", username);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["AccountStatus"].ToString() == "Deactivate")
                        {
                            cmd2.CommandText = "UPDATE accounts SET AccountStatus = @stat Where AccountId = @id and Username = @user";
                            cmd2.Parameters.AddWithValue("@stat", status);
                            cmd2.Parameters.AddWithValue("@id", accountId);
                            cmd2.Parameters.AddWithValue("@user", username);
                            cmd2.ExecuteNonQuery();
                            conn2.Close();
                            MessageBox.Show("Account is successfully activated.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAccountIdUpdate.Clear();
                            txtUsernameUpdate.Clear();
                            RegistrationForm_Load(this, null);
                        }
                        else
                            MessageBox.Show("Account is already active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    reader.Close();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = tblAccounts.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = tblAccounts.Rows[index];
            txtAccountIdUpdate.Text = selectedRow.Cells["AccountId"].Value.ToString();
            txtUsernameUpdate.Text = selectedRow.Cells["USername"].Value.ToString();
        }
    }
}
