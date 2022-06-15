namespace AirPurity.API.Data.Entities
{
    public class NotificationSubject
    {
        public int Id { get; set; }
        public string ParamCode { get; set; }
        public int IndexLevelId { get; set; }
        public int? LastIndexLevelId { get; set; }
        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
