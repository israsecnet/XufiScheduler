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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
