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
    public partial class AdminForm : Form
    {
        MySqlConnection conn;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        int activeTab = 0;
        string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        public AdminForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            panelControl.MouseDown += panel1_MouseDown;
            panelControl.MouseUp += panel1_MouseUp;
            panelControl.MouseMove += panel1_MouseMove;
            Dashboard ds = new Dashboard();
            ds.TopLevel = false;
            ds.AutoScroll = true;
            this.panel1.Controls.Add(ds);
            ds.Show();
            button1.MouseEnter += OnMouseEnterButton1;
            button1.MouseLeave += OnMouseLeaveButton1;
            button2.MouseEnter += OnMouseEnterButton2;
            button2.MouseLeave += OnMouseLeaveButton2;
            button3.MouseEnter += OnMouseEnterButton3;
            button3.MouseLeave += OnMouseLeaveButton3;
            button5.MouseEnter += OnMouseEnterButton5;
            button5.MouseLeave += OnMouseLeaveButton5;
            button6.MouseEnter += OnMouseEnterButton6;
            button6.MouseLeave += OnMouseLeaveButton6;
            button7.MouseEnter += OnMouseEnterButton7;
            button7.MouseLeave += OnMouseLeaveButton7;
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

        private void button1_Click(object sender, EventArgs e)
        {
            activeTab = 1;
            CheckActiveTab();
            panel1.Controls.Clear();
            RegistrationForm reg = new RegistrationForm();
            reg.TopLevel = false;
            reg.AutoScroll = true;
            this.panel1.Controls.Add(reg);
            reg.Show();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("MMMMMMMMMMMMM") + " " + DateTime.Now.Day + ", " + DateTime.Now.Year;
            Timer tmr = new Timer();
            tmr.Interval = 1000; //ticks every 1 second
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Start();

            conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Accounts WHERE Username = @username AND Password = @password ";
            MySqlParameter userParam = new MySqlParameter("@username", MySqlDbType.Text);
            MySqlParameter passParam = new MySqlParameter("@password", MySqlDbType.Text);
            userParam.Value = Globals.username;
            passParam.Value = Globals.password;
            cmd.Parameters.Add(userParam);
            cmd.Parameters.Add(passParam);
            using (reader = cmd.ExecuteReader())
            {
                if(reader.Read())
                {
                    cmd.CommandText = "SELECT * FROM AdminProfile WHERE AccountId = @id ";
                    MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Text);
                    idParam.Value = reader["AccountId"];
                    cmd.Parameters.Add(idParam);
                    conn.Close();
                    conn.Open();
                    using (MySqlDataReader reader2 = cmd.ExecuteReader())
                    {
                        if(reader2.Read())
                        {
                            nameLbl.Text = reader2["AdminFName"].ToString();
                        }
                        else
                        {
                            nameLbl.Text = "Failed";
                        }
                    }
                }
                else
                {
                    nameLbl.Text = "Failed";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            this.Hide();
            log.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            activeTab = 2;
            CheckActiveTab();
            panel1.Controls.Clear();
            AddItem ad = new AddItem();
            ad.TopLevel = false;
            ad.AutoScroll = true;
            this.panel1.Controls.Add(ad);
            ad.Show();
        }

        //Hover - change color
        private void OnMouseEnterButton1(object sender, EventArgs e)
        {
            button1.BackColor = Color.SeaGreen;
            button1.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button1.ForeColor = Color.FromArgb(143, 255, 170);
            activeAccounts.BackColor = Color.LightGreen;
        }
        private void OnMouseLeaveButton1(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(45, 45, 45);
            button1.FlatAppearance.BorderColor = Color.SeaGreen;
            button1.ForeColor = Color.White;
            activeAccounts.BackColor = Color.SeaGreen;
        }
        private void OnMouseEnterButton2(object sender, EventArgs e)
        {
            button2.BackColor = Color.SeaGreen;
            button2.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button2.ForeColor = Color.FromArgb(143, 255, 170);
            activeAddItem.BackColor = Color.LightGreen;
        }
        private void OnMouseLeaveButton2(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(45, 45, 45);
            button2.FlatAppearance.BorderColor = Color.SeaGreen;
            button2.ForeColor = Color.White;
            activeAddItem.BackColor = Color.SeaGreen;
        }
        private void OnMouseEnterButton3(object sender, EventArgs e)
        {
            button3.BackColor = Color.SeaGreen;
            button3.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button3.ForeColor = Color.FromArgb(143, 255, 170);
            activeUpdate.BackColor = Color.LightGreen;
        }
        private void OnMouseLeaveButton3(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(45, 45, 45);
            button3.FlatAppearance.BorderColor = Color.SeaGreen;
            button3.ForeColor = Color.White;
            activeUpdate.BackColor = Color.SeaGreen;
        }
        private void OnMouseEnterButton5(object sender, EventArgs e)
        {
            button5.BackColor = Color.SeaGreen;
            button5.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button5.ForeColor = Color.FromArgb(143, 255, 170);
            activeSearch.BackColor = Color.LightGreen;
        }
        private void OnMouseLeaveButton5(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(45, 45, 45);
            button5.FlatAppearance.BorderColor = Color.SeaGreen;
            button5.ForeColor = Color.White;
            activeSearch.BackColor = Color.SeaGreen;
        }
        private void OnMouseEnterButton6(object sender, EventArgs e)
        {
            button6.BackColor = Color.SeaGreen;
            button6.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button6.ForeColor = Color.FromArgb(143, 255, 170);
        }
        private void OnMouseLeaveButton6(object sender, EventArgs e)
        {
            button6.BackColor = Color.FromArgb(45, 45, 45);
            button6.FlatAppearance.BorderColor = Color.SeaGreen;
            button6.ForeColor = Color.White;
        }
        private void OnMouseEnterButton7(object sender, EventArgs e)
        {
            button7.BackColor = Color.SeaGreen;
            button7.FlatAppearance.BorderColor = Color.MediumAquamarine;
            button7.ForeColor = Color.FromArgb(143, 255, 170);
            activeReports.BackColor = Color.LightGreen;
        }
        private void OnMouseLeaveButton7(object sender, EventArgs e)
        {
            button7.BackColor = Color.FromArgb(45, 45, 45);
            button7.FlatAppearance.BorderColor = Color.SeaGreen;
            button7.ForeColor = Color.White;
            activeReports.BackColor = Color.SeaGreen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            ViewForm view = new ViewForm();
            view.TopLevel = false;
            view.AutoScroll = true;
            this.panel1.Controls.Add(view);
            view.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            activeTab = 3;
            CheckActiveTab();
            panel1.Controls.Clear();
            Update up = new Update();
            up.TopLevel = false;
            up.AutoScroll = true;
            this.panel1.Controls.Add(up);
            up.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            activeTab = 5;
            CheckActiveTab();
            panel1.Controls.Clear();
            Search sr = new Search();
            sr.TopLevel = false;
            sr.AutoScroll = true;
            this.panel1.Controls.Add(sr);
            sr.Show();
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Dashboard ds = new Dashboard();
            ds.TopLevel = false;
            ds.AutoScroll = true;
            this.panel1.Controls.Add(ds);
            ds.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            activeTab = 4;
            CheckActiveTab();
            panel1.Controls.Clear();
            ReportForm ds = new ReportForm();
            ds.TopLevel = false;
            ds.AutoScroll = true;
            this.panel1.Controls.Add(ds);
            ds.Show();
        }

        private void CheckActiveTab()
        {
            activeDashboard.Visible = false;
            activeAccounts.Visible = false;
            activeAddItem.Visible = false;
            activeUpdate.Visible = false;
            activeReports.Visible = false;
            activeSearch.Visible = false;

            if (activeTab == 0)
            {
                activeDashboard.Visible = true;
            }
            else if (activeTab == 1)
            {
                activeAccounts.Visible = true;
            }
            else if (activeTab == 2)
            {
                activeAddItem.Visible = true;
            }
            else if (activeTab == 3)
            {
                activeUpdate.Visible = true;
            }
            else if (activeTab == 4)
            {
                activeReports.Visible = true;
            }
            else if (activeTab == 5)
            {
                activeSearch.Visible = true;
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            activeTab = 0;
            CheckActiveTab();
            panel1.Controls.Clear();
            Dashboard ds = new Dashboard();
            ds.TopLevel = false;
            ds.AutoScroll = true;
            this.panel1.Controls.Add(ds);
            ds.Show();
        }

        private void btnDashboard_MouseEnter(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.SeaGreen;
            btnDashboard.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnDashboard.ForeColor = Color.FromArgb(143, 255, 170);
            activeDashboard.BackColor = Color.LightGreen;
        }

        private void btnDashboard_MouseLeave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(45, 45, 45);
            btnDashboard.FlatAppearance.BorderColor = Color.SeaGreen;
            btnDashboard.ForeColor = Color.White;
            activeDashboard.BackColor = Color.SeaGreen;
        }

        private void panelControl_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
