namespace ColocationApi.Domain.Entities
{
    public class TimeZoneEntity
    {
        public string Id { get; set; }
        public int GmtOffset { get; set; }
        public string Code { get; set; }
        public bool IsDaylightSaving { get; set; }
    }
}