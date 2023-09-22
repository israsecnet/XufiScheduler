using Google.Protobuf;
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
        public bool weekToggle = false;
        public bool endWeek = false;
        public bool endWeekback = false;
        public int weekCurrentDay = 1;
        public int weekRemain, weekRemain2;
        public static DateTime dt = DateTime.Now;
        public static DateTime weekPointer = DateTime.Now;
        int month = dt.Month;
        int year = dt.Year;
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            displayDays();
        }

        private void displayDaysWeeks()
        {
            if (weekToggle)
            {
                flowLayoutPanel1.Controls.Clear();

                DateTime today = DateTime.Now;
                String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(today.Month);
                monthLabel.Text = monthName + " " + year;
                DateTime startMonth = new DateTime(year, month, 1);
                weekPointer = startMonth;
                int days = DateTime.DaysInMonth(year, month);
                int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d"));
                int active = 0;
                int blank = 0;

                if (weekPointer.DayOfWeek == DayOfWeek.Sunday)
                {
                    blank = 1;
                    active = 7;
                }
                else if (weekPointer.DayOfWeek == DayOfWeek.Monday)
                {
                    blank = 2;
                    active = 6;
                }
                else if (weekPointer.DayOfWeek == DayOfWeek.Tuesday)
                {
                    blank = 3;
                    active = 5;
                }
                else if (weekPointer.DayOfWeek == DayOfWeek.Wednesday)
                {
                    blank = 4;
                    active = 4;
                }
                else if (weekPointer.DayOfWeek == DayOfWeek.Thursday)
                {
                    blank = 5;
                    active = 3;
                }
                else if (weekPointer.DayOfWeek == DayOfWeek.Friday)
                {
                    blank = 6;
                    active = 2;
                }
                else if (weekPointer.DayOfWeek == DayOfWeek.Saturday)
                {
                    blank = 7;
                    active = 1;
                }
                for (int i = 1; i < blank; i++)
                {
                    UserControlBlank UIBlank2 = new UserControlBlank();
                    flowLayoutPanel1.Controls.Add(UIBlank2);
                }
                for (int i = weekPointer.Day; i < weekPointer.Day + active; i++)
                {
                    WeekUserControl userControl = new WeekUserControl();
                    userControl.days(i, month, year);
                    flowLayoutPanel1.Controls.Add(userControl);
                }

            }
        }
        private void displayDays()
        {
            flowLayoutPanel1.Controls.Clear();
            DateTime dt = DateTime.Now;
            month = dt.Month;
            year = dt.Year;
            string datestring;
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
                datestring = year.ToString() + "-" + month.ToString() + "-" + i.ToString();
                UIDay.appts(DataPipe.getNumAppts(datestring));
                flowLayoutPanel1.Controls.Add(UIDay);
            }
         
            
        }

        private void back_button(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string datestring;
            DateTime startMonth = new DateTime(year, month, 1);
            int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;
            int days = DateTime.DaysInMonth(year, month);
            int active = 1;
            int blank = 1;

            if (weekPointer.Day == 1)
            {
                
                if (month == 1)
                {
                    month = 12;
                    year--;
                }
                else
                {
                    month--;
                }
                
                startMonth = new DateTime(year, month, 1);
                days = DateTime.DaysInMonth(year, month);
                daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;
                DateTime tmpdays = new DateTime(year, month, days);
                while (tmpdays.DayOfWeek != DayOfWeek.Sunday)
                {
                    tmpdays = tmpdays.AddDays(-1);
                }
                weekPointer = tmpdays;
            }
            else if (weekPointer.AddDays(-7).Month != weekPointer.Month)
            {
                weekPointer = new DateTime(year, month, 1);
            }
            else
            {
                weekPointer = weekPointer.AddDays(-7);

            }

            String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            

            if (weekToggle)
            {
                // if first of month and is not sunday
                if (weekPointer.Day == 1 && weekPointer.DayOfWeek != DayOfWeek.Sunday)
                {
                    blank = daysofweek;
                    active = 8 - daysofweek;
                }
                // if less than 7 days left
                else if (days - weekPointer.Day < 7)
                {
                    blank = 1;
                    active = days - weekPointer.Day + 1;
                }
                //if more than 7 days left
                else if (days - weekPointer.Day > 7)
                {
                    blank = 1;
                    active = 7;

                }
                for (int i = 1; i < blank; i++)
                {
                    UserControlBlank UIBlank2 = new UserControlBlank();
                    flowLayoutPanel1.Controls.Add(UIBlank2);
                }
                for (int i = weekPointer.Day; i < weekPointer.Day + active; i++)
                {
                    WeekUserControl userControl = new WeekUserControl();
                    userControl.days(i, month, year);
                    flowLayoutPanel1.Controls.Add(userControl);
                }

            }
            else
            {
                if (month == 1)
                {
                    month = 12;
                    year--;
                }
                else
                {
                    month--;
                }
                monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                monthLabel.Text = monthName + " " + year;
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
                    datestring = year.ToString() + "-" + month.ToString() + "-" + i.ToString();
                    UIDay.appts(DataPipe.getNumAppts(datestring));
                    flowLayoutPanel1.Controls.Add(UIDay);
                }
            }
        }

        private void next_button(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string datestring;
            DateTime startMonth = new DateTime(year, month, 1);
            int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;
            int active = 0;
            int blank = 0;

            if (weekPointer.AddDays(7).Month != weekPointer.Month)
            {
                if (month == 12)
                {
                    month = 1;
                    year++;
                }
                else
                {
                    month++;
                }
                weekPointer = new DateTime(year, month, 1);
                startMonth = new DateTime(year, month, 1);
                daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;
            }
            else if (weekPointer.Day == 1)
            {
                weekPointer = weekPointer.AddDays(8 - daysofweek);
            }
            else
            {
                weekPointer = weekPointer.AddDays(7);
                
            }

            String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            int days = DateTime.DaysInMonth(year, month);

            if (weekToggle)
            {
                // if first of month and is not sunday
                if (weekPointer.Day == 1 && weekPointer.DayOfWeek != DayOfWeek.Sunday)
                {
                    blank = daysofweek;
                    active = 8 - daysofweek;
                }
                // if less than 7 days left
                else if (days - weekPointer.Day < 7)
                {
                    blank = 1;
                    active = days - weekPointer.Day +1;
                }
                //if more than 7 days left
                else if (days - weekPointer.Day > 7)
                {
                    blank = 1;
                    active = 7;

                }
                for (int i = 1; i < blank; i++)
                {
                    UserControlBlank UIBlank2 = new UserControlBlank();
                    flowLayoutPanel1.Controls.Add(UIBlank2);
                }
                for (int i = weekPointer.Day; i < weekPointer.Day+active; i++)
                {
                    WeekUserControl userControl = new WeekUserControl();
                    userControl.days(i, month, year);
                    flowLayoutPanel1.Controls.Add(userControl);
                }

            }
            else
            {
                if (month == 12)
                {
                    month = 1;
                    year++;
                }
                else
                {
                    month++;
                }
                monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                monthLabel.Text = monthName + " " + year;
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
                    datestring = year.ToString() + "-" + month.ToString() + "-" + i.ToString();
                    UIDay.appts(DataPipe.getNumAppts(datestring));
                    flowLayoutPanel1.Controls.Add(UIDay);
                }
            }

        }
        private void exit_button(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void toggle_view_click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (weekToggle)
            {
                weekToggle = false;
                displayDays();
            }
            else
            {
                weekToggle = true;
                displayDaysWeeks();
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (label8.Text == "UTC")
            {
                label8.Text = "EST";
                DataPipe.est = true;
            }
            else
            {
                label8.Text = "UTC";
                DataPipe.est = false;
            }
        }
    }
}
