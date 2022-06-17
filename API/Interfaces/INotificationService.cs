using AirPurity.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirPurity.API.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetAll();
        bool Add(Notification notification, string email);
        void Remove(int id);
        void RemoveAllNotification(Guid stopNotificationToken);
        Task StartNotificationThread();
        void ResetNotificationLastIndexValues();
        Task<bool> SendConfirmationEmailAsync(string email);
        bool ConfirmEmail(Guid token);
    }
}
