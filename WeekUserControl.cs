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
            var datasrc = daylist.Select(x => new { custid = x.customerId, title = x.title, start = x.start }).ToList();
            //Lambda used for converting data into readable data source
            dataGridView1.DataSource = datasrc;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode= DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Columns[0].HeaderCell.Value = "Customer ID";
            dataGridView1.Columns[1].HeaderCell.Value = "Title";
            dataGridView1.Columns[2].HeaderCell.Value = "Start";


        }
    }
}
