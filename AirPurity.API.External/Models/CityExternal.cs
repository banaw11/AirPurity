namespace AirPurity.API.BusinessLogic.External.Models
{
    public class CityExternal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CommuneExternal Commune { get; set; }
    }
}
