using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationApi.Application.Models.GeolocationData
{
    public record ErrorResponse
    {
        public bool Success { get; set; }
        public ErrorModel Error { get; set; }
    }

    public record ErrorModel
    {
        public int Code{ get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }
}
