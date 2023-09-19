using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XufiScheduler
{
    public class Customer
    {
        public int customerId;
        public string customerName;
        public int addressId;
        public int active;
        public DateTime createDate;
        public string createdBy;
        public DateTime lastUpdate;
        public string lastUpdateBy;
    }

    public class Address
    {
        public int addressId;
        public string address;
        public string address2;
        public int cityId;
        public string postalCode;
        public string phone;
        public DateTime createDate;
        public string createdBy;
        public DateTime lastUpdate;
        public string lastUpdateBy;
    }

    public class City
    {
        public int cityId;
        public string city;
        public int countryId;
        public DateTime createDate;
        public string createdBy;
        public DateTime lastUpdate;
        public string lastUpdateBy;
    }

    public class Country
    {
        public int countryId;
        public string country;
        public DateTime createDate;
        public string createdBy;   
        public DateTime lastUpdate;
        public string lastUpdateBy;
    }
}
