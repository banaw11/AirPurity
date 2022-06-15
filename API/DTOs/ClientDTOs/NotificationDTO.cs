using System.Collections.Generic;

namespace AirPurity.API.DTOs.ClientDTOs
{
    public class NotificationDTO
    {
        public string UserEmail { get; set; }
        public int CityId { get; set; }
        public int StationId { get; set; }
        public int? IndexLevelId { get; set; }
        public List<NotificationSubjectDTO> NotificationSubjects { get; set; }
    }

    public class NotificationSubjectDTO
    {
        public string ParamCode { get; set; }
        public int IndexLevelId { get; set; }
        public int? LastIndexLebelId { get; set; }
        public int NotificationId { get; set; }
    }
}
