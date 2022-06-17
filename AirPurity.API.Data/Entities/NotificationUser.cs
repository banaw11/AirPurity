namespace AirPurity.API.Data.Entities
{
    public class NotificationUser : BaseModel
    {
        public NotificationUser()
        {
            this.Notifications = new HashSet<Notification>();
        }

        public string UserEmail { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Guid? EmailConfirmationToken { get; set; }
        public Guid? StopNotificationToken { get; set; }
    }
}
