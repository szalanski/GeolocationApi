namespace GeolocationApi.Application.Dtos.GeolocationData
{
    public record ConnectionDto
    {
        public int asn { get; set; }
        public string isp { get; set; }
    }

}
