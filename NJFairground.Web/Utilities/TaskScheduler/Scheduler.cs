using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace NJFairground.Web.Utilities.TaskScheduler
{
    public class Scheduler
    {
        public readonly IScheduler SchedulerInstance;
        public string JobName { get; set; }
        public string JobGroup { get; set; }
        public int Priority  { get; set; }
        public string CronExpression  { get; set; }

        private readonly ISchedulerFactory _schedulerFactory;

        public Scheduler()
        {
            this._schedulerFactory = new StdSchedulerFactory();
        }
    }
}