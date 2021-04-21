using System.Collections.Generic;

namespace API.DTOs.ClientDTOs
{
    public class ProvinceFormDTO
    {
        public string ProvinceName { get; set; }
        public ICollection<DistrictFormDTO> Districts { get; set; }
    }
}