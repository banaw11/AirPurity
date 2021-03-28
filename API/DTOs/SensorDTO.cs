using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SensorDTO
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public ParamDTO Param { get; set; }
    }
}
