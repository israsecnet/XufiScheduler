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
    public partial class DayControlUser : UserControl
    {
        public DayControlUser()
        {
            InitializeComponent();
        }

        public void days(int numday)
        {
            dayNumber.Text = numday + "";
        }
    }
}
