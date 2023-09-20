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
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
            populateTable();
        }

        public void populateTable()
        {
            var customerdb = DataPipe.getCustomerDB();
            var tmpdb = from row in customerdb select new { customerId = row.Key, customerName = row.Value };
            customerGrid.DataSource = tmpdb.ToArray();
            customerGrid.Columns[0].HeaderCell.Value = "Customer ID";
            customerGrid.Columns[1].HeaderCell.Value = "Customer Name";

        }
    }
}
