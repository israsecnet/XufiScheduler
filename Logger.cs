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
        public static void writeUserLogin(int userId)
        {
            string path = "log.text";
            string log = $"User {userId} | LOGGED IN | {DateTime.Now.ToString("u")}" + Environment.NewLine;

            File.AppendAllText(path, log);
        }
    }
}
