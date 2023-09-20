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
    public partial class ApptAdd : Form
    {
        public ApptAdd()
        {
            InitializeComponent();
            var customerdb = DataPipe.getCustomerDB();
            var tmpdb = from row in customerdb select new { customerId = row.Key, customerName = row.Value };
            comboBox1.DataSource = tmpdb.ToArray();
        }
        public ApptAdd(int appointmentId)
        {
            InitializeComponent();
            var customerdb = DataPipe.getCustomerDB();
            var tmpdb = from row in customerdb select new { customerId = row.Key, customerName = row.Value };
            comboBox1.DataSource = tmpdb.ToArray();
            Dictionary<string, string>tmpdata = DataPipe.getAppointmentDetails(appointmentId);
            textBox3.Text = tmpdata["title"].ToString();
            textBox4.Text = tmpdata["description"].ToString();
            textBox5.Text = tmpdata["location"].ToString();
            textBox6.Text = tmpdata["contact"].ToString();
            textBox7.Text = tmpdata["type"].ToString();
            textBox8.Text = tmpdata["url"].ToString();
            dateTimePicker1.Value = DateTime.Parse(tmpdata["end"].ToString());
            dateTimePicker2.Value = DateTime.Parse(tmpdata["start"].ToString());

        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
