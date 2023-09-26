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
            dataGridView1.DataSource = apptlist.Select(c => new { c.customerId, c.title, c.description, c.contact, c.location, c.type, c.url, c.start, c.end }).ToList();
            
        }
    }
}
