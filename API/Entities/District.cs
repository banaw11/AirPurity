using System.Collections.Generic;

namespace API.Entities
{
    public class District
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int ProvienceId { get; set; }
        public virtual Province Province {get; set;}
        public virtual ICollection<Commune> Communes { get; set; }
    }
}