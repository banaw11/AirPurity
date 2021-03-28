using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MeasureDataDTO
    {
        public string Key { get; set; }
        public ICollection<MeasureDTO> Values { get; set; }
    }
}
