using System.Collections.Generic;

namespace API.DTOs.ClientDTOs
{
    public class DistrictFormDTO
    {
        public string DistrictName { get; set; }
        public ICollection<CommuneFormDTO> Communes { get; set; }
    }
}