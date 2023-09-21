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
        public int weekCurrentDay = 1;
        public int weekRemain, weekRemain2;
        public static DateTime dt = DateTime.Now;
        int month = dt.Month;
        int year = dt.Year;
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

        private void displayDaysWeeks()
        {
            if (weekToggle)
            {
                flowLayoutPanel1.Controls.Clear();
                
                String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                monthLabel.Text = monthName + " " + year;
                DateTime startMonth = new DateTime(year, month, 1);
                int days = DateTime.DaysInMonth(year, month);
                int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;
                int firstRow = 9 - daysofweek;
                int remain = days - (8 - daysofweek);
                for (int i = 1; i < daysofweek; i++)
                {
                    UserControlBlank UIBlank2 = new UserControlBlank();
                    flowLayoutPanel1.Controls.Add(UIBlank2);
                }
                for (int i = 1; i < firstRow; i++)
                {
                    WeekUserControl userControl = new WeekUserControl();
                    userControl.days(i, month, year);
                    flowLayoutPanel1.Controls.Add(userControl);
                }

                weekRemain = remain;
                weekRemain2 = 0;
                weekCurrentDay = firstRow - 1;

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
            String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            DateTime startMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;
            

            if (weekToggle)
            {
                
                if (weekRemain2 > 7)
                {
                    for (int i = weekCurrentDay - 6; i < weekCurrentDay + 1; i++)
                    {
                        WeekUserControl userControl = new WeekUserControl();
                        userControl.days(i, month, year);
                        flowLayoutPanel1.Controls.Add(userControl);
                    }
                    weekRemain2 -= 7;
                    weekCurrentDay -= 7;
                    weekRemain += 7;
                }
                else if (weekRemain2 > 0)
                {
                    displayDaysWeeks();
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
                    days = DateTime.DaysInMonth(year, month);
                    startMonth = new DateTime(year, month, days);
                    daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;

                    for (int i = days-daysofweek; i < days + 1; i++)
                    {
                        WeekUserControl userControl = new WeekUserControl();
                        userControl.days(i, month, year);
                        flowLayoutPanel1.Controls.Add(userControl);
                    }

                    weekRemain = daysofweek;
                    weekRemain2 = days-daysofweek;
                    weekCurrentDay = weekRemain2;
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

        private void next_button(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string datestring;
            String monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            monthLabel.Text = monthName + " " + year;
            DateTime startMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysofweek = Convert.ToInt32(startMonth.DayOfWeek.ToString("d")) + 1;
            if (weekToggle)
            {
                if (endWeek)
                {
                    endWeek = false;
                    displayDaysWeeks();
                }
                else
                {
                    if (weekRemain > 7)
                    {
                        for (int i = weekCurrentDay + 1; i < weekCurrentDay + 8; i++)
                        {
                            WeekUserControl userControl = new WeekUserControl();
                            userControl.days(i, month, year);
                            flowLayoutPanel1.Controls.Add(userControl);
                        }
                        weekRemain -= 7;
                        weekCurrentDay += 7;
                        weekRemain2 += 7;
                    }
                    else
                    {
                        for (int i = weekCurrentDay + 1; i < days + 1; i++)
                        {
                            WeekUserControl userControl = new WeekUserControl();
                            userControl.days(i, month, year);
                            flowLayoutPanel1.Controls.Add(userControl);

                        }
                        endWeek = true;
                        if (month == 12)
                        {
                            month = 1;
                            year++;
                        }
                        else
                        {
                            month++;
                        }
                    }
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
    }
}
