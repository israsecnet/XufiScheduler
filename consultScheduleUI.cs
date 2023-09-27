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
            foreach(Appointment appointment in apptlist)
            {

                FlowLayoutPanel flp = new FlowLayoutPanel
                {
                    WrapContents = true,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };
                Label startTime = new Label()
                {
                    Text="Start: " + appointment.start.ToString("ddd HH:mm")
                };
                Label EndTime = new Label()
                {
                    Text = "End: " + appointment.end.ToString("ddd HH:mm")
                };
                Label Title = new Label()
                {
                    Text = "Title: " + appointment.title.ToString()
                };
                Dictionary<string, string> tmpdict = DataPipe.getCustomerDetails(appointment.customerId);
                string name = tmpdict["customerName"];
                Label CustomerName = new Label()
                {
                    Text = "Customer Name:" + name
                };
                flp.Controls.Add(startTime);
                flp.Controls.Add(EndTime);
                flp.Controls.Add(Title);
                flp.Controls.Add(CustomerName);
                flowLayoutPanel1.Controls.Add(flp);
            }
            //dataGridView1.DataSource = apptlist.Select(c => new { c.customerId, c.title, c.description, c.contact, c.location, c.type, c.url, c.start, c.end }).ToList();
            
        }
    }
}
