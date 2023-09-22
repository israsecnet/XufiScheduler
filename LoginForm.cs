using MySql.Data.MySqlClient;
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
    public partial class LoginForm : Form
    {
        public string errMsg = "Username and password do not match";
        public LoginForm()
        {
            InitializeComponent();

            if (CultureInfo.CurrentUICulture.LCID == 1034)
            {
                label1.Text = "Nombre de usuario";
                label2.Text = "Contraseña";
                button1.Text = "Iniciar sesión";
                errMsg = "El nombre de usuario y la contraseña no coinciden.";
            }
        }
        
        public static void reminderLaunch(string customerName, string apptTime)
        {
            string message = $"Appointment Reminder:\n{customerName} @ {apptTime}";
            string title = "Upcoming Appointments";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static int UserSearch(string username, string pass)
        {
            MySqlConnection conn = new MySqlConnection(DataPipe.connectstring);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT userId FROM user WHERE userName = '{username}' AND password = '{pass}'", conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                DataPipe.setCurrentUserId(Convert.ToInt32(rdr[0]));
                DataPipe.setCurrentUserName(username);
                rdr.Close();
                conn.Close();
                return DataPipe.getCurrentUserId();
            }
            return 0;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserSearch(textBox1.Text, textBox2.Text) != 0)
            {
                MainForm dashboard = new MainForm();
                dashboard.loginForm = this;
                Logger.login_log(DataPipe.getCurrentUserId());//Get Current User ID
                dashboard.Show();
                this.Hide();
                DateTime dt = DateTime.Now;
                List<Appointment> aplist = DataPipe.getDailyAppts(dt.ToString("yyyy-MM-dd"));
                foreach (Appointment ap in aplist)
                {
                    if (ap.start < dt.AddMinutes(15) && ap.start > dt)
                    {
                        reminderLaunch(ap.customerId.ToString(), ap.start.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show(errMsg, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
            }
        }
    }
}
