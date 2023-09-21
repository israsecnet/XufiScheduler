using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XufiScheduler
{
    public partial class WeekUserControl : UserControl
    {
        public WeekUserControl()
        {
            InitializeComponent();
        }
        public int numday2, month2, year2;


        private void dayNumber_Click(object sender, EventArgs e)
        {

        }
        public void days(int numday, int month, int year)
        {
            label1.Text = numday + "";
            numday2 = numday;
            month2 = month;
            year2 = year;
        }


        private void WeekUserControl_Load(object sender, EventArgs e)
        {
            string datestring = year2.ToString() + "-" + month2.ToString() + "-" + numday2.ToString();
            List<Appointment> daylist = DataPipe.getDailyAppts(datestring);
            dataGridView1.DataSource = daylist.ToArray();
        }
    }
}
