using AirPurity.API.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace AirPurity.API.QuartzCore
{
    public class ResetNotificationJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public ResetNotificationJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                notificationService.ResetNotificationLastIndexValues();
            }
            return Task.CompletedTask;
        }
    }
}
