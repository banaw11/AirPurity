using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Pagination
{
    public class SensorsDataQuery
    {
        [Required]
        public int StationId { get; set; }
        [Required]
        public RangeOfData Range { get; set; }
    }
}