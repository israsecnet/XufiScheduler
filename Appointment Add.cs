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
            List<int> idList = customerdb.Keys.ToList();
            comboBox1.DataSource = idList;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        public static int apptId;
        public ApptAdd(int appointmentId)
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            apptId = appointmentId;
            comboBox1.Enabled = false;
            var customerdb = DataPipe.getCustomerDB();
            var tmpdb = from row in customerdb select new { customerId = row.Key, customerName = row.Value };
            List<int> idList = customerdb.Keys.ToList();
            comboBox1.DataSource = idList;
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
            if (comboBox1.Enabled == true)
            {
                bool result = DataPipe.addAppointment(Convert.ToInt32(comboBox1.Text), textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                DataPipe.updateAppointment(apptId, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
                this.Close();
        }
    }
}
