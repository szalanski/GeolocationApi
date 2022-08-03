using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocationApi.Domain.Entities
{
    public class Geolocation
    {
        public string Ip { get; set; }
        public string Type { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public TimeZoneEntity TimeZone { get; set; }
        public string TimeZoneId { get; set; }
        public Currency Currency { get; set; }
        public string CurrencyId { get; set; }
        public int ConnectionAsn { get; set; }
        public string ConnectionIsp { get; set; }
    }
}
