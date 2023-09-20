﻿using System;
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

        public CustomerAdd(int customerId)
        {
            InitializeComponent();
            Dictionary<string, string> tmpdata = DataPipe.getCustomerDetails(customerId);
            textBox3.Text = tmpdata["customerName"].ToString();
            textBox4.Text = tmpdata["address"].ToString();
            textBox5.Text = tmpdata["address2"].ToString();
            textBox6.Text = tmpdata["city"].ToString();
            textBox1.Text = tmpdata["country"].ToString();
            textBox7.Text = tmpdata["zip"].ToString();
            textBox8.Text = tmpdata["phone"].ToString();
            if (tmpdata["active"] == "1")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

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
