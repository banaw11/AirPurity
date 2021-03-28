using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SensorDataDTO
    {
        public int Id { get; set; }
        public string ParamName { get; set; }
        public string ParamCode { get; set; }
        public ICollection<MeasureDTO> Values { get; set; }
    }
}
