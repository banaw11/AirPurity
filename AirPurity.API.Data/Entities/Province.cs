namespace AirPurity.API.Data.Entities
{
    public class Province : BaseModel
    {
        public Province()
        {
            this.Districts = new HashSet<District>();
        }

        public string ProvinceName { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}