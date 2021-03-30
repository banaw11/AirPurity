using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.ClientDTOs
{
    public class StationClientDTO
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public double GegrLat { get; set; }
        public double GegrLon { get; set; }
        public string AddressStreet { get; set; }
        public ICollection<SensorDTO> Sensors { get; set; }
    }
}
