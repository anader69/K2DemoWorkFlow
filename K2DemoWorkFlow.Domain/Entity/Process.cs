

using System.ComponentModel.DataAnnotations.Schema;
//km release
namespace K2DemoWorkFlow.Domain.Entities.Workflow
{
    public class Process : EntityBase<int>
    {
        public Process()
        {
            ProcessActivities = new HashSet<ProcessActivity>();
            ProcessActions = new HashSet<ProcessAction>();
            TaskStatues = new HashSet<TaskStatus>();
            ProcessActionTrackings = new HashSet<ProcessActionTracking>();
            Tasks = new HashSet<Task>();
        }

        [NotMapped]
      //   public string Name => CultureHelper.IsArabic ? NameAr : NameEn;
        public string WorkflowName { get; private set; }
        public string NameAr { get; private set; }
        public string NameEn { get; private set; }
        //public string ActivityControlsPath { get; private set; }
        //public int StateCode { get; private set; }
        //public string DecisionFormURL { get; private set; }
        //public string DetailsFormURL { get; private set; }
        //public string EditFormURL { get; private set; }
        //public string BackFormURL { get; private set; }

        public ICollection<ProcessActivity> ProcessActivities { get; private set; }
        public ICollection<ProcessAction> ProcessActions { get; private set; }
        public ICollection<TaskStatus> TaskStatues { get; private set; }
        public ICollection<Task> Tasks { get; private set; }
        public ICollection<ProcessActionTracking> ProcessActionTrackings { get; private set; }
    }
}
