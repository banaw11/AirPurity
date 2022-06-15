using AirPurity.API.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace AirPurity.API.QuartzCore
{
    public class NotificationJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    var notificationThread = notificationService.StartNotificationThread();
                    if (!notificationThread.IsCompleted)
                    {
                        notificationThread.Start();
                    }
                }
                catch (Exception)
                {

                }
            }
            return Task.CompletedTask;
        }
    }
}
