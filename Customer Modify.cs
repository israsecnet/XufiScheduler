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
    public partial class CustomerModify : Form
    {
        public CustomerModify()
        {
            InitializeComponent();
        }

        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var winapp = new CustomerAdd();
            winapp.ShowDialog();
        }

        public static bool detailCheck(string box1, string box2, string box3, string box4, string box5)
        {
            bool name = false;
            bool address = false;
            bool city = false;
            bool postal = false;
            bool phone = false;
            if (box1 != string.Empty)
            {
                name = true;
            }
            if (box2 != string.Empty)
            {
                address = true;
            }
            if (box3 != string.Empty)
            {
                city = true;
            }
            if (box4 != string.Empty)
            {
                postal = true;
            }
            if (box5 != string.Empty)
            {
                phone = true;
            }
            if (!name || !address || !city || !postal || !phone)
            {
                MessageBox.Show("Please check all fields!", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            detailCheck(textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text);
        }
    }
}
