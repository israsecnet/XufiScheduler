using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XufiScheduler
{
    public class Appointment
    {
        public int appointmentId;
        public int customerId;
        public int userId;
        public string title;
        public string description;
        public string location;
        public string contact;
        public string type;
        public string url;
        public DateTime start;
        public DateTime end;
        public DateTime createDate;
        public string createdBy;
        public DateTime lastUpdate;
        public string lastUpdateBy;


        //Check M-F 9-5 Business hours of appt start and end
        public static bool businessHoursCheck(DateTime apptStart, DateTime apptEnd)
        {
            if ((apptStart.Hour > 17 || apptStart.Hour < 9 || apptStart.DayOfWeek == DayOfWeek.Saturday || apptStart.DayOfWeek == DayOfWeek.Sunday) || (apptEnd.Hour > 17 || apptEnd.Hour < 9 || apptEnd.DayOfWeek == DayOfWeek.Saturday || apptEnd.DayOfWeek == DayOfWeek.Sunday))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check against all appointment details and dates
        /*
         * Check if same day, else skip
         * if apptStart.Day == tmpapptStart.Day
         * then
         *  if apptStart.Hour > tmpapptStart.Hour && apptStart.Hour < tmpapptStart.Hour
         *      return error
         *  if apptEnd.Hour > tmpapptStart.Hour 
         *      return error
         * else
         *  Skip
         * */
        public static bool conflictHoursCheck(DateTime apptStart, DateTime apptEnd)
        {
            if(apptStart.Hour == apptEnd.Hour)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    
}
