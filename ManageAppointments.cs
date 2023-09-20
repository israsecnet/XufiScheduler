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
    public partial class ManageAppointments : Form
    {
        public ManageAppointments()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Apointment_click(object sender, EventArgs e)
        {
            var appwin = new ApptAdd();
            appwin.Show();
        }
    }
}
