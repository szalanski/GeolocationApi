using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationApi.Application.Dtos.GeolocationData
{

    public record GeolocationDto
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
        public LocationDto location { get; set; }
        public TimeZoneDto time_zone { get; set; }
        public CurrencyDto currency { get; set; }
        public ConnectionDto connection { get; set; }
    }

}
