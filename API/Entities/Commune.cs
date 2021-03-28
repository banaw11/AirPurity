using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Entities
{
    public class Commune
    {
        public string CommuneName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
