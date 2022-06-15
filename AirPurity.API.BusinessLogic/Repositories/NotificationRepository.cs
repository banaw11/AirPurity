using AirPurity.API.BusinessLogic.Repositories;
using AirPurity.API.Data;
using AirPurity.API.Data.Entities;

namespace AirPurity.API.Repositories.BusinessLogic.Repositories
{
    public class NotificationRepository : Repository<Notification>
    {
        private readonly DataContext _context;

        public NotificationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public void RemoveNotificationSubject(NotificationSubject notificationSubject)
        {
            if(notificationSubject != null && notificationSubject.Id > 0)
            {
                _context.NotificationSubjects.Remove(notificationSubject);
            }
        }

        public void AddNotificationSubject(NotificationSubject notificationSubject)
        {
            if(notificationSubject != null)
            {
                _context.NotificationSubjects.Add(notificationSubject);
            }
        }
    }
}
