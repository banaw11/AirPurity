using System.Collections.Generic;

namespace API.DTOs.ClientDTOs
{
    public class DistrictFormDTO
    {
        public string Name { get; set; }
        public ICollection<CommuneFormDTO> Communes { get; set; }
    }
}