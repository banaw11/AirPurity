using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Entities
{
    public class Commune
    {
        public int Id { get; set; }
        public string CommuneName { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
