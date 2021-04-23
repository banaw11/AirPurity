using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Pagination
{
    public class SensorsDataQuery
    {
        [Required]
        public int stationId { get; set; }
        [Required]
        public RangeOfData Range { get; set; }
    }
}