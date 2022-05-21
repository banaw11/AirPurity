namespace AirPurity.API.Data.Entities
{
    public class Commune : BaseModel
    {
        public Commune()
        {
            this.Cities = new HashSet<City>();
        }

        public string CommuneName { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
