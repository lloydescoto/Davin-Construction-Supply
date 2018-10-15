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
    public partial class LoginForm : Form
    {
        MySqlConnection conn;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";      
        public LoginForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            button1.MouseEnter += OnMouseEnterButton1;
            button1.MouseLeave += OnMouseLeaveButton1;
            button2.MouseEnter += OnMouseEnterButton2;
            button2.MouseLeave += OnMouseLeaveButton2;
            passTxt.MouseHover += new EventHandler(textBox1_MouseHover);
            passTxt.MouseLeave += new EventHandler(textBox1_MouseLeave);
            userTxt.Text = "emplloyd";
            passTxt.Text = "12345";
        }
        ToolTip toolTip1 = new ToolTip();
        void textBox1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(passTxt);
        }
        void textBox1_MouseHover(object sender, EventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {

                toolTip1.ToolTipTitle = "Caps Lock Is On";
                toolTip1.ToolTipIcon = ToolTipIcon.Warning;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(passTxt, "Having Caps Lock on may cause you to enter your password incorrectly.\n\nYou should press Caps Lock to turn it off before entering your password.");
                toolTip1.Show("Having Caps Lock on may cause you to enter your password incorrectly.\n\nYou should press Caps Lock to turn it off before entering your password.", passTxt, 5, passTxt.Height - 5);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts WHERE Username = @username";
            MySqlParameter userParam = new MySqlParameter("@username", MySqlDbType.Text);

            //Validation
            if (userTxt.Text == "" || passTxt.Text == "")
            {
                DialogResult result = MessageBox.Show("Please fill out all data fields.", "Error!");
                if (result == DialogResult.OK)
                {
                    userTxt.Clear();
                    passTxt.Clear();
                }
            }
            else
            {
                userParam.Value = userTxt.Text;
                cmd.Parameters.Add(userParam);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["Username"].Equals(userTxt.Text))
                        {
                            if (passTxt.Text.Equals(reader["Password"].ToString()))
                            {
                                if (reader["AccountStatus"].Equals("Active"))
                                {
                                    StartUpForm start = new StartUpForm();
                                    this.Hide();
                                    start.ShowDialog();
                                    this.Show();
                                    if (reader["AccountType"].Equals("Admin"))
                                    {
                                        AdminForm af = new AdminForm();
                                        Globals.username = reader["Username"].ToString();
                                        Globals.password = reader["Password"].ToString();
                                        this.Hide();
                                        af.ShowDialog();
                                        this.Close();
                                    }
                                    else
                                    {
                                        EmployeeForm ef = new EmployeeForm();
                                        Globals.username = reader["Username"].ToString();
                                        Globals.password = reader["Password"].ToString();
                                        this.Hide();
                                        ef.ShowDialog();
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Account is deactivated", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Password does not match", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username does not exists", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account does not exists", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void OnMouseEnterButton1(object sender, EventArgs e)
        {
            button1.BackColor = Color.SeaGreen;
            button1.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button1.ForeColor = Color.White;
        }
        private void OnMouseLeaveButton1(object sender, EventArgs e)
        {
            button1.BackColor = Color.MintCream;
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
            button2.BackColor = Color.MintCream;
            button2.FlatAppearance.BorderColor = Color.SeaGreen;
            button2.ForeColor = Color.Black;
        }

        private void passTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs ());
            }
        }

        private void userTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }

        }

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            passTxt.PasswordChar = '\0';
        }

        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            passTxt.PasswordChar = '•';
        }

        private void userTxt_Enter(object sender, EventArgs e)
        {
            userTxt.Clear();
            userTxt.ForeColor = Color.Black;
        }

        private void passTxt_Enter(object sender, EventArgs e)
        {
            passTxt.Clear();
            passTxt.ForeColor = Color.Black;
            passTxt.PasswordChar = '•';
        }
    }
}
