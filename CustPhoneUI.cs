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
    public partial class CustPhoneUI : UserControl
    {
        public CustPhoneUI()
        {
            InitializeComponent();
        }

        public void populate(string custName)
        {
            label1.Text = custName;
        }
    }
}
