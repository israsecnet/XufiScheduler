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
            customerGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void populateTable()
        {
            customerGrid.Rows.Clear();
            customerGrid.Refresh();
            var customerdb = DataPipe.getCustomerDB();
            var tmpdb = from row in customerdb select new { customerId = row.Key, customerName = row.Value };
            //Lambda used to convert into usable data source
            customerGrid.DataSource = tmpdb.ToArray();
            customerGrid.Columns[0].HeaderCell.Value = "Customer ID";
            customerGrid.Columns[1].HeaderCell.Value = "Customer Name";

        }

        private void add_customer_click(object sender, EventArgs e)
        {
            var appwin = new CustomerAdd();
            appwin.Show();
        }

        private void exit_button(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modify_customer_click(object sender, EventArgs e)
        {
            if (customerGrid.SelectedRows != null && customerGrid.SelectedRows.Count > 0)
            {
                int modId = Convert.ToInt32(customerGrid.CurrentRow.Cells[0].Value);
                var appwin = new CustomerAdd(modId);
                appwin.Show();
            }
        }

        private void delete_click(object sender, EventArgs e)
        {
            if (customerGrid.SelectedRows != null && customerGrid.SelectedRows.Count > 0)
            {
                int modId = Convert.ToInt32(customerGrid.CurrentRow.Cells[0].Value);
                DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DataPipe.deleteCustomer(modId);
                }
            }
        }
    }
}
