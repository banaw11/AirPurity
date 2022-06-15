using AirPurity.API.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirPurity.API.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetAll();
        void Add(Notification notification);
        void Remove(int id);
        void RemoveAllNotification(string userEmail);
        Task StartNotificationThread();
        void ResetNotificationLastIndexValues();
    }
}
