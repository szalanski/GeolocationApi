using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationApi.Application.Models.GeolocationData
{

    public record GeolocationModel
    {
        public string ip { get; set; }
        public string type { get; set; }
        public string continent_code { get; set; }
        public string continent_name { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public LocationModel location { get; set; }
        public TimeZoneModel time_zone { get; set; }
        public CurrencyModel currency { get; set; }
        public ConnectionModel connection { get; set; }
    }

}
