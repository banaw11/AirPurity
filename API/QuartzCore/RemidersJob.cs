using API.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace API.QuartzCore
{
    public class RemidersJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public RemidersJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopeContext = scope.ServiceProvider.GetRequiredService<IHubRepository>();
                scopeContext.RefreshClientsData();
            }
            return Task.CompletedTask;
        }
    }
}
