namespace AirPurity.API.Data.Entities
{
    public class District : BaseModel
    {
        public District()
        {
            this.Communes = new HashSet<Commune>();
        }

        public string DistrictName { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province {get; set;}
        public virtual ICollection<Commune> Communes { get; set; }
    }
}