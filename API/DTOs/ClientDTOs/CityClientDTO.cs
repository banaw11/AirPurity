using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.ClientDTOs
{
    public class CityClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<StationClientDTO> Stations { get; set; }
    }
}
