using System.Collections.Generic;

namespace API.DTOs.ClientDTOs
{
    public class CommuneFormDTO
    {
        public string Name { get; set; }
        public ICollection<CityFormDTO> Cities { get; set; }
    }
}