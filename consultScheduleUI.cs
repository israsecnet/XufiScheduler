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
    public partial class consultScheduleUI : UserControl
    {
        public consultScheduleUI()
        {
            InitializeComponent();
        }

        public void fill_schedule(int userid, List<Appointment> apptlist)
        {
            label1.Text = "User ID " + userid.ToString();
            Dictionary<string, string> tmpdict = new Dictionary<string, string>();
            string custname;
            foreach (Appointment appointment in apptlist)
            {
                tmpdict = DataPipe.getCustomerDetails(appointment.customerId);
                custname = tmpdict["customerName"].ToString();
                FlowLayoutPanel flp = new FlowLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowOnly
                };
                Label startTime = new Label()
                {
                    Text="Start: " + appointment.start.ToString("MM-dd HH:mm")
                };
                Label EndTime = new Label()
                {
                    Text = "End: " + appointment.end.ToString("MM-dd HH:mm")
                };
                Label Title = new Label()
                {
                    Text = "Title: " + appointment.title.ToString()
                };
                Label custName = new Label()
                {
                    Text = $"Customer Name: {custname.ToString()}",
                    Width = 200
                };
                flp.Controls.Add(custName);
                flp.Controls.Add(startTime);
                flp.Controls.Add(EndTime);
                flp.Controls.Add(Title);
                flowLayoutPanel1.Controls.Add(flp);
            }
            //dataGridView1.DataSource = apptlist.Select(c => new { c.customerId, c.title, c.description, c.contact, c.location, c.type, c.url, c.start, c.end }).ToList();
            
        }
    }
}
