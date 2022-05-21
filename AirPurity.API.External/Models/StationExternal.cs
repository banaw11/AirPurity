namespace AirPurity.API.BusinessLogic.External.Models
{
    public class StationExternal
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public double GegrLat { get; set; }
        public double GegrLon { get; set; }
        public CityExternal City { get; set; }
        public string AddressStreet { get; set; }
    }
}
