using API.Models.BLL;
using Quartz;
using Quartz.Impl;

namespace API.Models.Entity;

class CronJob
{
    public async Task Task1()
    {
        var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        await scheduler.Start();
        var job = JobBuilder.Create<CheckAndAdd>().Build();
        var trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .WithCronSchedule("0 0 1 1 * ?")
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }

    private class CheckAndAdd : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Task Started");

            var res = await BllBntDatabase.GetBntDatabase();
            await Console.Out.WriteLineAsync((string?) res);
        }
    }
}