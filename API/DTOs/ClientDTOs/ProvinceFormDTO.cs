using System.Collections.Generic;

namespace API.DTOs.ClientDTOs
{
    public class ProvinceFormDTO
    {
        public string Name { get; set; }
        public ICollection<DistrictFormDTO> Districts { get; set; }
    }
}