using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuartzSchedule;
using Quartz;
using Quartz.Impl;

namespace QuartzSchedule
{
    public class CloseCommissionInscriptionSchedule
    {


        public static void Start()
        {

            try
            {
                var cronExpression = "0 0 0 1/1 * ? *"; //Todos los días a las 00:00hs

                ISchedulerFactory schedFact = new StdSchedulerFactory();

                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                IJobDetail job = JobBuilder.Create<Start>()
                        .WithIdentity("closeCommissionsInscriptionJob", "closeCommissionsInscriptionGroup")
                        .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("closeCommissionsInscriptionTrigger", "closeCommissionsInscriptionTriggerGroup")
                    .WithCronSchedule(cronExpression)
                    .Build();

                sched.ScheduleJob(job, trigger);

            }
            catch (SchedulerException e)
            {

            }
        }
    }


}

