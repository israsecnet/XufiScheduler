using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Crypto;

namespace XufiScheduler
{
    public class DataPipe
    {
        private static Dictionary<int, Hashtable> appts = new Dictionary<int, Hashtable>();
        private static int currentUser;
        private static string currentUserName;
        public static string connectstring = "Server=127.0.0.1;User ID=sqlUser;Password=Passw0rd!;Database=client_schedule;";
        
        public static int getCurrentUserId()
        {
            return currentUser;
        }

        public static void setCurrentUserId(int curuser)
        {
            currentUser = curuser;
        }

        public static string getCurrentUserName()
        {
            return currentUserName;
        }

        public static void setCurrentUserName(string curuser)
        {
            currentUserName = curuser;
        }

        public static void setAppts(Dictionary<int, Hashtable> tmpappts)
        {
            appts = tmpappts;
        }

        public static int newId(List<int> idList)
        {
            int max = 0;
            foreach(int id in idList)
            {
                if (id > max)
                {
                    max = id;
                }
            }
            return max + 1;
        }

        public static List<Appointment> getappts()
        {
            string query = "SELECT * FROM appointment";
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            MySqlCommand cmd = new MySqlCommand(query, c);
            List<Appointment> customerList = new List<Appointment>();
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Appointment tmp = new Appointment();
                    tmp.appointmentId = Convert.ToInt32(rdr[0]);
                    tmp.customerId = Convert.ToInt32(rdr[1]);
                    tmp.userId = Convert.ToInt32(rdr[2]);
                    tmp.title = Convert.ToString(rdr[3]);
                    tmp.description = Convert.ToString(rdr[4]);
                    tmp.location = Convert.ToString(rdr[5]);
                    tmp.contact = Convert.ToString(rdr[6]);
                    tmp.type = Convert.ToString(rdr[7]);
                    tmp.url = Convert.ToString(rdr[8]);
                    tmp.start = DateTime.Parse(Convert.ToString(rdr[9]));
                    tmp.end = DateTime.Parse(Convert.ToString(rdr[10]));

                    customerList.Add(tmp);
                }
                rdr.Close();
            }
            c.Close();

            return customerList;
        }

        public static void deleteCustomer(int custId)
        {
            string query = $"DELETE FROM customer WHERE customerId={custId.ToString()}";
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            MySqlCommand cmd = new MySqlCommand(query, c);
            cmd.ExecuteNonQuery();
            c.Close();
        }
        public static void deleteAppointment(int apptId)
        {
            string query = $"DELETE FROM appointment WHERE appointmentId={apptId.ToString()}";
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            MySqlCommand cmd = new MySqlCommand(query, c);
            cmd.ExecuteNonQuery();
            c.Close();
        }
        
        public static Dictionary<string, string> getCustomerDetails(int customerId)
        {
            string query = $"SELECT * FROM customer WHERE customerId={customerId.ToString()}";
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            MySqlCommand cmd = new MySqlCommand(query, c);
            Dictionary<string, string> customerInfo = new Dictionary<string, string>();
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    customerInfo.Add("customerName", rdr[1].ToString());
                    customerInfo.Add("addressId", rdr[2].ToString());
                    customerInfo.Add("active", rdr[3].ToString());
                }
                rdr.Close();
            }

            query = $"SELECT * FROM address WHERE addressId={customerInfo["addressId"]}";
            cmd = new MySqlCommand(query, c);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    customerInfo.Add("address", rdr[1].ToString());
                    customerInfo.Add("address2", rdr[2].ToString());
                    customerInfo.Add("cityId", rdr[3].ToString());
                    customerInfo.Add("zip", rdr[4].ToString());
                    customerInfo.Add("phone", rdr[5].ToString());

                }
                rdr.Close();
            }

            query = $"SELECT * FROM city WHERE cityId={customerInfo["cityId"]}";
            cmd = new MySqlCommand(query, c);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    customerInfo.Add("city", rdr[1].ToString());
                    customerInfo.Add("countryId", rdr[2].ToString());
                }
                rdr.Close();
            }

            query = $"SELECT * FROM country WHERE countryId={customerInfo["countryId"]}";
            cmd = new MySqlCommand(query, c);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    customerInfo.Add("country", rdr[1].ToString());
                }
                rdr.Close();
            }
            c.Close();
            return customerInfo;
        }
        public static Dictionary<int,string> getCustomerDB()
        {
            string query = "SELECT customerId, customerName FROM customer";
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            MySqlCommand cmd = new MySqlCommand(query, c);
            Dictionary<int, string> customerList = new Dictionary<int, string>();
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while(rdr.Read())
                {
                    customerList.Add(Convert.ToInt32(rdr[0]), rdr[1].ToString());
                }
                rdr.Close();
            }
            c.Close();

            return customerList;
        }
        
        static public void updateAppointment(int appointmentId, string title, string description, string location, string contact, string type, string url, string end, string start)
        {
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            DateTime dt = DateTime.Now;
            MySqlCommand cmd = new MySqlCommand($"UPDATE appointment SET title='{title}', description='{description}', location='{location}',contact='{contact}',type='{type}',url='{url}',start='{start}', end='{end}', lastUpdateBy='{getCurrentUserName()}', lastUpdate='{dt.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE appointmentId={appointmentId}", c);
            cmd.ExecuteNonQuery();
        }
        static public void updateCustomer(int customerId, string customerName, string address, string address2, string city, string country, string zip, string phone, int active)
        {
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            int addressId = addAddress(address, address2, city, country, zip, phone);
            DateTime dt = DateTime.Now;
            MySqlCommand cmd = new MySqlCommand($"UPDATE customer SET customerName='{customerName}', addressId={addressId}, active={active}, lastUpdateBy='{getCurrentUserName()}', lastUpdate='{dt.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE customerId={customerId}", c);
            cmd.ExecuteNonQuery();
        }
        static public Dictionary<string, string> getAppointmentDetails(int appointmentId)
        {
            string query = $"SELECT * FROM appointment WHERE appointmentId = '{appointmentId}'";
            MySqlConnection c = new MySqlConnection(DataPipe.connectstring);
            c.Open();
            MySqlCommand cmd = new MySqlCommand(query, c);
            Dictionary<string, string> appointmentDict = new Dictionary<string, string>();
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while(rdr.Read())
                {
                    appointmentDict.Add("appointmentId", appointmentId.ToString());
                    appointmentDict.Add("customerId", rdr[1].ToString());
                    appointmentDict.Add("title", rdr[3].ToString());
                    appointmentDict.Add("description", rdr[4].ToString());
                    appointmentDict.Add("location", rdr[5].ToString());
                    appointmentDict.Add("contact", rdr[6].ToString());
                    appointmentDict.Add("type", rdr[7].ToString());
                    appointmentDict.Add("url", rdr[8].ToString());
                    appointmentDict.Add("start", rdr[9].ToString());
                    appointmentDict.Add("end", rdr[10].ToString());
                }
                rdr.Close();
            }
            c.Close();
            return appointmentDict;
        }
        public static int createId(string table)
        {
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT {table + "Id"} FROM {table}", con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<int> ids = new List<int>();
            while (rdr.Read())
            {
                ids.Add(Convert.ToInt32(rdr[0]));
            }
            rdr.Close();
            con.Close();
            return newId(ids);
        }

        public static int getNumAppts(string date)
        {
            int numAppts = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM appointment WHERE DATE(start) = DATE('{date}')", con);
            int tmpnum = Convert.ToInt32(cmd.ExecuteScalar());
            return tmpnum;
        }

        public static bool addAppointment(int customerId, string title, string description, string location, string contact, string type, string url, string start, string end)
        {
            bool result = false;
            bool overlap = false;
            DateTime startDate = DateTime.Parse(start);
            string start_date = startDate.ToString("yyyy-MM-DD");
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            //Find all appointments on same day, look at start time and end time for overlap
            MySqlCommand cmd = new MySqlCommand($"SELECT start, end FROM appointment WHERE start LIKE '{start_date}'", con);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DateTime tmpstart = DateTime.Parse(rdr[0].ToString());
                    DateTime tmpend = DateTime.Parse(rdr[1].ToString());
                    DateTime curstart = DateTime.Parse(start);
                    DateTime curend = DateTime.Parse(end);
                    if (curstart < tmpend && tmpstart < curend)
                    {
                        overlap = true;
                    }
                }
                rdr.Close();
            }
            if (!overlap)
            {
                cmd = new MySqlCommand($"SELECT appointmentId FROM appointment ORDER BY appointmentId DESC LIMIT 1", con);
                int newId = Convert.ToInt32(cmd.ExecuteScalar());
                newId++;
                DateTime dt = DateTime.Now;
                cmd = new MySqlCommand($"INSERT INTO appointment VALUES ({newId},{customerId},{DataPipe.getCurrentUserId()},'{title}','{description}','{location}','{contact}','{type}', '{url}', '{start}','{end}', '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}', '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}')", con);
                cmd.ExecuteNonQuery();
                result = true;
            }
            
            return result;
        }
        public static bool addCustomer(string customerName, string address, string address2, string city, string country, string zip, string phone, int active)
        {
            bool flag = false;
            int lastId = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT customerId FROM customer ORDER BY customerId DESC LIMIT 1", con);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while(rdr.Read())
                {
                    lastId = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
            }
            int custId = lastId + 1;
            DateTime dt = DateTime.Now;
            int addressId = addAddress(address, address2, city, country, zip, phone);
            cmd = new MySqlCommand($"INSERT INTO customer VALUES ({custId.ToString()}, '{customerName.ToString()}', {addressId.ToString()}, {active.ToString()}, '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}', '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            return flag;
        }

        public static int addAddress(string address = "", string address2 = "", string city = "", string country = "", string zip = "", string phone = "") 
        {
            int addressId = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            int cityId = addCity(city, country);
            MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(addressId) FROM address WHERE (address='{address}') AND (cityId={cityId}) AND (postalCode='{zip}') AND (phone='{phone}')", con);
            var count = cmd.ExecuteScalar();
            if (Convert.ToInt32(count) > 0)
            {
                cmd = new MySqlCommand($"SELECT addressId FROM address WHERE (address='{address}') AND (cityid={cityId}) AND (postalCode='{zip}') AND (phone='{phone}')", con);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        addressId = Convert.ToInt32(rdr[0]);
                    }
                    rdr.Close();
                }
            }
            else
            {
                cmd = new MySqlCommand($"SELECT addressId FROM address ORDER BY addressId DESC LIMIT 1",con);
                addressId = Convert.ToInt32(cmd.ExecuteScalar());
                addressId++;
                DateTime dt = DateTime.Now;
                cmd = new MySqlCommand($"INSERT INTO address VALUES ({addressId},'{address}','{address2}',{cityId},{zip},{phone}, '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}', '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}')", con);
                cmd.ExecuteNonQuery();            
            }
            return addressId;
        }
 
        public static int addCity(string city, string country)
        {
            int cityId = 0;
            int countryId = addCountry(country);
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM city WHERE (countryId={countryId}) AND (city='{city}')", con);
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                cmd = new MySqlCommand($"SELECT cityId FROM city WHERE (countryId={countryId}) AND (city='{city}') LIMIT 1", con);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            else // create cityID
            {
                cmd = new MySqlCommand($"SELECT cityID FROM city ORDER BY cityID DESC LIMIT 1", con);
                cityId = Convert.ToInt32(cmd.ExecuteScalar());
                cityId++;
                DateTime dt = DateTime.Now;
                cmd = new MySqlCommand($"INSERT INTO city VALUES ({cityId.ToString()}, '{city}', {countryId}, '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}', '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}')", con);
                cmd.ExecuteNonQuery();
                return cityId;
            }
        }
        public static int addCountry(string country)
        {
            int countryId = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM country WHERE (country='{country}')", con);
            var tmp = cmd.ExecuteScalar();
            if (Convert.ToInt32(tmp) > 0)
            {
                cmd = new MySqlCommand($"SELECT countryId FROM country WHERE (country='{country}')", con);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            else // create country ID
            {
                cmd = new MySqlCommand($"SELECT countryId FROM country ORDER BY countryId DESC LIMIT 1", con);
                countryId = Convert.ToInt32(cmd.ExecuteScalar());
                countryId++;
                DateTime dt = DateTime.Now;
                cmd = new MySqlCommand($"INSERT INTO country VALUES ({countryId}, '{country}', '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}', '{dt.ToString("yyyy-MM-dd HH:mm:ss")}', '{getCurrentUserName()}')", con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            return countryId;
        }
    }
}
