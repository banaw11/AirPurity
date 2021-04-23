using System.ComponentModel.DataAnnotations;

namespace API.SignalR
{
    public class ClientDto
    {
        [Required]
        public string ConnectionId { get; set; }
        [Required]
        public int StationId { get; set; }
    }
}
