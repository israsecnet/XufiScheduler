using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XufiScheduler
{
    public partial class MainForm : Form
    {
        public LoginForm loginForm;
        int month, year;
        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            displayDays();
        }
        private void displayDays()
        {
            flowLayoutPanel1.Controls.Clear();
            DateTime dt = DateTime.Now;
            month = dt.Month;
            year = dt.Year;
            String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            DateTime startMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < daysofweek; i++)
            {
                UserControlBlank UIBlank = new UserControlBlank();
                flowLayoutPanel1.Controls.Add(UIBlank);
            }
            DateTime tmpdate = startMonth;
            for (int i = 1; i < days; i++)
            {
                DayControlUser UIDay = new DayControlUser();
                UIDay.days(i);
                tmpdate.AddDays(1);
                Console.WriteLine(DataPipe.getNumAppts(tmpdate.ToString("yyyy-MM-dd")));
                UIDay.appts(DataPipe.getNumAppts(tmpdate.ToString("yyyy-MM-dd")));
                flowLayoutPanel1.Controls.Add(UIDay);
            }
        }

        private void back_button(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (month == 1)
            {
                month = 12;
                year--;
            }
            else
            {
                month--;
            }
            String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            DateTime startMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < daysofweek; i++)
            {
                UserControlBlank UIBlank = new UserControlBlank();
                flowLayoutPanel1.Controls.Add(UIBlank);
            }
            for (int i = 1; i < days; i++)
            {
                DayControlUser UIDay = new DayControlUser();
                UIDay.days(i);
                flowLayoutPanel1.Controls.Add(UIDay);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var appwin = new ManageCustomers();
            appwin.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var appwin = new ManageAppointments();
            appwin.Show();
        }

        private void next_button(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (month == 12)
            {
                month = 1;
                year++;
            }
            else
            {
                month++;
            }
            String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            DateTime startMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < daysofweek; i++)
            {
                UserControlBlank UIBlank = new UserControlBlank();
                flowLayoutPanel1.Controls.Add(UIBlank);
            }
            for (int i = 1; i < days; i++)
            {
                DayControlUser UIDay = new DayControlUser();
                UIDay.days(i);
                flowLayoutPanel1.Controls.Add(UIDay);
            }
        }
    }
}
