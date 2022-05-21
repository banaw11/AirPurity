namespace AirPurity.API.Data.Entities
{
    public class City : BaseModel
    {
        public City()
        {
            this.Stations = new HashSet<Station>();
        }

        public string Name { get; set; }
        public int CommuneId { get; set; }
        public virtual Commune Commune { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
    }
}
