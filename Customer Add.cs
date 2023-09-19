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
    public partial class CustomerAdd : Form
    {
        public CustomerAdd()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string timestamp = DateTime.Now.ToString("u");
            
            bool completeForm = CustomerModify.detailCheck(textBox3.Text, textBox4.Text, textBox6.Text, textBox7.Text, textBox8.Text);
            if (completeForm && (radioButton1.Checked || radioButton2.Checked))
            {
                if (radioButton1.Checked)
                {
                    DataPipe.addCustomer(textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox1.Text, textBox7.Text, textBox8.Text, 1);
                }
                else
                {
                    DataPipe.addCustomer(textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox1.Text, textBox7.Text, textBox8.Text, 0);
                }
                
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
