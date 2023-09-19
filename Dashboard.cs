using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XufiScheduler
{
    public partial class Dashboard : Form
    {
        public LoginForm loginForm;
        public Dashboard()
        {
            InitializeComponent();
            tableLayoutPanel1.Visible = true;
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newApptClick(object sender, EventArgs e)
        {
            var appwin = new ApptAdd();
            appwin.ShowDialog();
        }

        private void modifyApptClick(object sender, EventArgs e)
        {
            var appwin = new ApptModify();
            appwin.ShowDialog();

        }

        private void addCustomerClick(object sender, EventArgs e)
        {
            var appwin = new CustomerAdd();
            appwin.ShowDialog();
        }

        private void modifyCustomerClick(object sender, EventArgs e)
        {
            var appwin = new CustomerModify();
            appwin.ShowDialog();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void monthViewClick(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel2.Visible = false;
            /*TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Location = new Point(3, 3);
            tableLayoutPanel.Name = "TableLayoutPanel1";
            tableLayoutPanel.Size = new Size(1161, 693);
            tableLayoutPanel.TabIndex = 0;
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.ColumnCount = 7;
            Label sunLabel = new Label();
            Label monLabel = new Label();
            Label tuesLabel = new Label();
            Label wedLabel = new Label();
            Label thurLabel = new Label();
            Label friLabel = new Label();
            Label satLabel = new Label();
            tableLayoutPanel.Controls.Add(sunLabel, 0, 0);
            tableLayoutPanel.Controls.Add(monLabel, 0, 1);
            tableLayoutPanel.Controls.Add(tuesLabel, 0, 2);
            tableLayoutPanel.Controls.Add(wedLabel, 0, 3);
            tableLayoutPanel.Controls.Add(thurLabel, 0, 4);
            tableLayoutPanel.Controls.Add(friLabel, 0, 5);
            tableLayoutPanel.Controls.Add(satLabel, 0, 6);
            Controls.Add(tableLayoutPanel);*/

        }

        private void weekViewClick(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;
        }
    }
}
