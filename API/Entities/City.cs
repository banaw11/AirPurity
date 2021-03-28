using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CommuneName { get; set; }
        public string DistrictName { get; set; }
        public virtual Commune Commune { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
    }
}
