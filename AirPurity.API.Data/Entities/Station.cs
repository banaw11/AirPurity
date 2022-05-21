namespace AirPurity.API.Data.Entities
{
    public class Station : BaseModel
    {
        public string StationName { get; set; }
        public double GegrLat { get; set; }
        public double GegrLon { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string AddressStreet { get; set; }
    }
}
