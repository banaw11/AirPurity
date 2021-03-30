using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Entities
{
    public class Station
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public double GegrLat { get; set; }
        public double GegrLon { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string AddressStreet { get; set; }
    }
}
