using K2DemoWorkFlow.Domain.Entities.Workflow;
using Microsoft.EntityFrameworkCore;

namespace K2DemoWorkFlow.Data
{
    public class LeaveRequestContext: DbContext
    {
        protected LeaveRequestContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessAction> ProcessActions { get; set; }
        public DbSet<ProcessActionTracking> ProcessActionTrackings { get; set; }
        public DbSet<ProcessActivity> ProcessActivitys { get; set; }
        public DbSet<Domain.Entities.Workflow.Task> Tasks { get; set; }
        public DbSet<Domain.Entities.Workflow.TaskStatus> TaskStatuses { get; set; }
    }
}
