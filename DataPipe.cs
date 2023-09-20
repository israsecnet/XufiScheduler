﻿using System;
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
            cmd = new MySqlCommand($"INSERT INTO customer VALUES ({custId.ToString()}, {customerName.ToString()}, {addressId.ToString()}, {active.ToString()}, @dt1, @user1, @dt2, @user2 )", con);
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
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        addressId = Convert.ToInt32(rdr[0]);
                    }
                    rdr.Close();
                }
            }

            catch (Exception ex)
            {
            
                cmd = new MySqlCommand($"SELECT addressId FROM address ORDER BY addressId DESC LIMIT 1", con);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        addressId = Convert.ToInt32(rdr[0]);
                    }
                    rdr.Close();
                }
                DateTime dt = DateTime.Now;
                cmd = new MySqlCommand($"INSERT INTO address VALUES ({addressId},'{address.ToString()}','{address2.ToString()}',{cityId},{zip},{phone.ToString()},'{dt.ToString("dd/MM/yyy HH:mm:ss")}',{getCurrentUserName()},'{dt.ToString("dd/MM/yyy HH:mm:ss")}',{getCurrentUserName()})", con);
                cmd.ExecuteNonQuery();
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
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        cityId = Convert.ToInt32(rdr[0]);
                    }
                    rdr.Close();
                }
                if (addCountry(country) == cityId)
                {
                    return cityId;
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    cmd = new MySqlCommand($"SELECT cityId FROM city ORDER BY cityId DESC LIMIT 1", con);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            cityId = Convert.ToInt32(rdr[0]);
                        }
                        rdr.Close();
                    }
                    cmd = new MySqlCommand($"INSERT INTO city VALUES ({cityId}, {city}, {addCountry(country)},{dt},{getCurrentUserName()},{dt}, {getCurrentUserName()} )");
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
                    cmd = new MySqlCommand($"INSERT INTO city VALUES ({cityId}, {city}, {addCountry(country)},{dt},{getCurrentUserName()},{dt}, {getCurrentUserName()} )");
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
                cmd2 = new MySqlCommand($"INSERT INTO country VALUES ({countryId}, {country}, {dt}, {getCurrentUserName()}, {dt}, {getCurrentUserName()})", con);
                cmd2.ExecuteNonQuery();
            }
            con.Close();
            return countryId;
        }
    }
}
