using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication8
{
    public partial class StartUpForm : Form
    {
        public StartUpForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            lblDH.MouseEnter += lblDH_MouseEnter;
            lblDH.MouseLeave += lblDH_MouseLeave;
            lblIS.MouseEnter += lblIS_MouseEnter;
            lblIS.MouseLeave += lblIS_MouseLeave;

            Timer MyTimer = new Timer();
            MyTimer.Interval = (1 * 60 * 20);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblContinue_Click(object sender, EventArgs e)
        {
            
        }
        private void lblDH_MouseEnter(object sender, EventArgs e)
        {
            lblDH.ForeColor = Color.Green;
        }

        private void lblDH_MouseLeave(object sender, EventArgs e)
        {
            lblDH.ForeColor = Color.Black;
        }
        private void lblIS_MouseEnter(object sender, EventArgs e)
        {
            lblIS.ForeColor = Color.Green;
        }

        private void lblIS_MouseLeave(object sender, EventArgs e)
        {
            lblIS.ForeColor = Color.Black;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
        }
    }
}
