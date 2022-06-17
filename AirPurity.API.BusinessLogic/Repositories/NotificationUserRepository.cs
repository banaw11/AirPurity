using AirPurity.API.BusinessLogic.Repositories;
using AirPurity.API.Data;
using AirPurity.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirPurity.API.Repositories.Repositories
{
    public class NotificationUserRepository : Repository<NotificationUser>
    {
        private readonly DataContext _context;

        public NotificationUserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public NotificationUser DeactivateUser(Guid stopNotificationToken)
        {
           var user = _context.NotificationUsers.FirstOrDefault(x => x.StopNotificationToken == stopNotificationToken);
            if (user != null)
            {
                user.IsActive = false;
                Update(user);
                SaveChanges();

                return user;
            }

            return null;
        }

        public bool ActivateUser(string email)
        {
            var user = _context.NotificationUsers.FirstOrDefault(x => x.UserEmail.ToLower() == email.ToLower());
            if (user != null)
            {
                user.IsActive = true;
                Update(user);

                return true;
            }

            return false;
        }

        public bool ConfirmEmail(Guid emailConfirmationToken)
        {
            var user = _context.NotificationUsers.FirstOrDefault(x => x.EmailConfirmationToken == emailConfirmationToken);
            if(user != null)
            {
                user.IsEmailConfirmed = true;
                user.StopNotificationToken = Guid.NewGuid();

                Update(user);
                SaveChanges();
                return true;
            }

            return false;
        }

        public NotificationUser GetByEmail(string email)
        {
            return _context.NotificationUsers.Where(x => x.UserEmail == email).FirstOrDefault();
        }

    }
}
