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
    public partial class ItemSearch : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        public string itemcode;
        public ItemSearch()
        {
            InitializeComponent();
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseUp += panel1_MouseUp;
            panel1.MouseMove += panel1_MouseMove;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int indexpass = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[indexpass];
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].Name == "EmployeeForm")
                    {
                        EmployeeForm employeeForm = (EmployeeForm)Application.OpenForms[i];
                        employeeForm.txtItemCode.Text = selectedRow.Cells["ListItemCode"].Value.ToString();
                        this.Close();
                    }
                    else if(Application.OpenForms[i].Name == "AddItem")
                    {
                        AddItem addItemForm = (AddItem)Application.OpenForms[i];
                        addItemForm.textBox1.Text = selectedRow.Cells["ListItemName"].Value.ToString();
                        addItemForm.textBox2.Text = selectedRow.Cells["ListItemPrice"].Value.ToString();
                        addItemForm.textBox9.Text = selectedRow.Cells["ListItemSellPrice"].Value.ToString();
                        addItemForm.comboBoxUnit.Text = selectedRow.Cells["Unit"].Value.ToString();
                        addItemForm.textBox1.ReadOnly = true;
                        addItemForm.textBox2.ReadOnly = true;
                        addItemForm.textBox9.ReadOnly = true;
                        this.Close();
                    }
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.SeaGreen;
            button1.FlatAppearance.BorderColor = Color.White;
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.SeaGreen;
            button1.FlatAppearance.BorderColor = Color.White;
            button1.ForeColor = Color.White;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.SeaGreen;
            button2.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button2.ForeColor = Color.White;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.MintCream;
            button2.FlatAppearance.BorderColor = Color.SeaGreen;
            button2.ForeColor = Color.Black;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtSearch.ForeColor = Color.Black;
        }

        private void ItemSearch_Load_1(object sender, EventArgs e)
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
                            dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"], reader2["Quantity"], reader["ItemUnit"]);
                        }
                    }
                }
                reader.Close();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            Search();
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
            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemPrice"], reader["ItemSellPrice"], reader2["Quantity"]);

                            }
                        }
                    }
                }
                else
                    //MessageBox.Show("Value did not match any record.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reader.Close();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button2.PerformClick();
        }
    }
}
