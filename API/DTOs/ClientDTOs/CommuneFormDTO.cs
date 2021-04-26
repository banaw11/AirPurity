using System.Collections.Generic;

namespace API.DTOs.ClientDTOs
{
    public class CommuneFormDTO
    {
        public string CommuneName { get; set; }
        public ICollection<CityFormDTO> Cities { get; set; }
    }
}