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
    public partial class monthuserControl : UserControl
    {
        public monthuserControl()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void monthuserControl_Load(object sender, EventArgs e)
        {

        }

        public void fillMonth(int month, int types)
        {
            DateTime dt = new DateTime(2023, month, 1);
            label1.Text = dt.ToString("MMMM");
            label2.Text = types.ToString();
        }
    }
}
