namespace AirPurity.API.Data.Entities
{
    public class Notification : BaseModel
    {
        public Notification()
        {
            this.NotificationSubjects = new HashSet<NotificationSubject>();
        }

        public string UserEmail { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
        public virtual ICollection<NotificationSubject> NotificationSubjects { get; set; }
        public int? IndexLevelId { get; set; }
        public int? LastIndexLevelId { get; set; }

    }
}
