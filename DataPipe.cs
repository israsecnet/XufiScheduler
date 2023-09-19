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

        public static List<Customer> getCustomerList()
        {
            List<Customer> list = new List<Customer>();
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT");
            return list;

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

        public static bool addCustomer(string customerName, string address, string address2, string city, string country, string zip, string phone, int active)
        {
            bool flag = false;
            int lastId = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT customerId FROM customer ORDER BY customerId DESC LIMIT 1", con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0]);
                    lastId = Convert.ToInt32(rdr[0]);
                }
            }
            rdr.Close() ;
            int custId = lastId + 1;
            DateTime dt = DateTime.Now;
            int addressId = addAddress(address, address2, city, country, zip, phone);
            cmd = new MySqlCommand($"INSERT INTO customer VALUES ({custId}, {customerName}, {addressId}, {active}, @dt1, @user1, @dt2, @user2 )", con);
            cmd.Parameters.AddWithValue("@user1", getCurrentUserName());
            cmd.Parameters.AddWithValue("@user2", getCurrentUserName());
            cmd.Parameters.AddWithValue("@dt1", dt);
            cmd.Parameters.AddWithValue("@dt2", dt);
            try
            {
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                Console.WriteLine(ex.ToString());
            }
            con.Close();
            return flag;
        }

        public static int addAddress(string address = "", string address2 = "", string city = "", string country = "", string zip = "", string phone = "") 
        {
            int addressId = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            int cityId = addCity(city, country);
            MySqlCommand cmd = new MySqlCommand($"SELECT phone FROM address WHERE (address='{address}') AND (city='{city}') AND (postalCode='{zip}') AND (phone='{phone}')", con);
            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    addressId = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                cmd = new MySqlCommand($"SELECT addressId FROM address ORDER BY addressId DESC LIMIT 1", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        addressId = Convert.ToInt32(rdr[0]);
                    }
                }
                DateTime dt = DateTime.Now;
                cmd = new MySqlCommand($"INSERT INTO address ({addressId},{address},{address2},{cityId},{zip},{phone},{dt},{getCurrentUserName()},{dt},{getCurrentUserName()}");
                //cmd.ExecuteReader();
                rdr.Close();
            }
            con.Close();
            return addressId;
        }
        public static int addCity(string city = "", string country = "")
        {
            int cityId = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT countryId FROM city WHERE (city={city})", con);
            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cityId = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
                if (addCountry(country) == cityId)
                {
                    return cityId;
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    cmd = new MySqlCommand($"SELECT cityId FROM city ORDER BY cityId DESC LIMIT 1", con);
                    rdr = cmd.ExecuteReader();
                    cityId = 0;
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            cityId = Convert.ToInt32(rdr[0]);
                        }
                    }
                    cmd = new MySqlCommand($"INSERT INTO city ({cityId}, {city}, {addCountry(country)},{dt},{getCurrentUserName()},{dt}, {getCurrentUserName()} )");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
                DateTime dt = DateTime.Now;
                cmd = new MySqlCommand($"SELECT cityId FROM city ORDER BY cityId DESC LIMIT 1", con);
                try
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        cityId = Convert.ToInt32(rdr[0]);
                    }
                    cmd = new MySqlCommand($"INSERT INTO city ({cityId}, {city}, {addCountry(country)},{dt},{getCurrentUserName()},{dt}, {getCurrentUserName()} )");
                    cmd.ExecuteNonQuery();
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            con.Close();
            return cityId;
        }
        public static int addCountry(string country)
        {
            int countryId = 0;
            MySqlConnection con = new MySqlConnection(connectstring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT countryId FROM country WHERE (country={country})", con);
            MySqlCommand cmd2 = new MySqlCommand($"SELECT countryId FROM country ORDER BY countryId DESC LIMIT 1", con);
            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    countryId = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                MySqlDataReader rdr = cmd2.ExecuteReader();
                while (rdr.Read())
                {
                    countryId = Convert.ToInt32(rdr[0]);
                }
                rdr.Close();
                countryId = countryId + 1;
                DateTime dt = DateTime.Now;
                cmd2 = new MySqlCommand($"INSERT INTO country ({countryId}, {country}, {dt}, {getCurrentUserName()}, {dt}, {getCurrentUserName()})", con);
                cmd2.ExecuteNonQuery();
            }
            con.Close();
            return countryId;
        }
    }
}
