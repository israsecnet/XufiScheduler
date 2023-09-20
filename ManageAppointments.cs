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
    public partial class ManageAppointments : Form
    {
        public ManageAppointments()
        {
            InitializeComponent();
            appointmentGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            List<Appointment> tmpdata = DataPipe.getappts();
            appointmentGrid.DataSource = tmpdata.Select(c => new { c.appointmentId, c.title, c.location, c.customerId, c.start }).ToList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Apointment_click(object sender, EventArgs e)
        {
            var appwin = new ApptAdd();
            appwin.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (appointmentGrid.SelectedRows != null && appointmentGrid.SelectedRows.Count > 0)
            {
                int modId = Convert.ToInt32(appointmentGrid.CurrentRow.Cells[0].Value);
                var appwin = new ApptAdd(modId);
                appwin.Show();
            }
            
        }

        private void delete_click(object sender, EventArgs e)
        {
            if (appointmentGrid.SelectedRows != null && appointmentGrid.SelectedRows.Count > 0)
            {
                int modId = Convert.ToInt32(appointmentGrid.CurrentRow.Cells[0].Value);
                DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DataPipe.deleteAppointment(modId);
                }
            }
        }
    }
}
