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

        public void fill_schedule(int userid, int hours)
        {
            label1.Text = "User ID " + userid.ToString() + " - Total hours booked:";
            label2.Text = hours.ToString();
        }
    }
}
