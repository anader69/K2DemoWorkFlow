using K2DemoWorkFlow.Domain.Entities.Workflow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Infrastructure
{
    public class WorkFlowContext: DbContext
    {
        public WorkFlowContext(DbContextOptions<WorkFlowContext> options)
       : base(options)
        { }

        public DbSet<Process> Processes { get; set; }

        public DbSet<ProcessAction> ProcessActions { get; set; }
        public DbSet<ProcessActionTracking> ProcessActionTrackings { get; set; }
        public DbSet<ProcessActivity> ProcessActivities { get; set; }
        public DbSet<Domain.Entities.Workflow.Task> Tasks { get; set; }
        public DbSet<Domain.Entities.Workflow.TaskStatus> TaskStatus { get; set; }

    }
}
