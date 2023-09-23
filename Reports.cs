using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XufiScheduler
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void appt_by_month(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Dictionary<int, int> dispData = DataPipe.getApptTypes();
            for (int i = 1; i < 13; i++)
            {
                monthuserControl monthUI = new monthuserControl();
                monthUI.fillMonth(i, dispData[i]);
                flowLayoutPanel1.Controls.Add(monthUI);
            }
            

        }

        private void get_schedule(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Dictionary<int, double> tmpDict = DataPipe.getConsultSchedule();
            for (int i = 1;i < tmpDict.Keys.Count + 1;i++) 
            {
                consultScheduleUI consultUI = new consultScheduleUI();
                consultUI.fill_schedule(i, tmpDict[i]);
                flowLayoutPanel1.Controls.Add(consultUI);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Dictionary<string, string> tmpDict = DataPipe.getCustomerNumbers();
            List<string> lines = tmpDict.Select(x => x.Key + x.Value).ToList();
            for (int i = 1; i < lines.Count + 1; i++)
            {
                CustPhoneUI custPhoneUI = new CustPhoneUI();
                custPhoneUI.populate(lines[i]);
                flowLayoutPanel1.Controls.Add(custPhoneUI);
            }
        }
    }
}