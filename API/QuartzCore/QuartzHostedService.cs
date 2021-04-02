using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace API.QuartzCore
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<Job> _jobs;
        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<Job> jobs)
        {
            _jobs = jobs;
            _jobFactory = jobFactory;
            _schedulerFactory = schedulerFactory;
        }

        public IScheduler Scheduler {get; set;}

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var job in _jobs)
            {
                var task = CreateJob(job);
                var trigger = CreateTrigger(job);
                await Scheduler.ScheduleJob(task,trigger,cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(Job job)
        {
            var type = job.Type;
            return JobBuilder.Create(type)
                .WithIdentity(type.FullName)
                .WithDescription(type.Name)
            .Build();
        }
        
         private static ITrigger CreateTrigger(Job job)
        {
            return TriggerBuilder.Create()
                .WithIdentity($"{job.Type.FullName}.trigger")
                .WithCronSchedule(job.Expression)
                .WithDescription(job.Expression)
                .Build();
        }
    }
}