using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XufiScheduler
{
    public class Logger
    {
        public static void login_log(int userId)
        {
            string filepath = "log.text";
            string msg = $"User {userId} | LOGGED IN | {DateTime.Now.ToString("u")}" + Environment.NewLine;
            File.AppendAllText(filepath, msg);
        }
    }
}
