
using Framework.Core.Data;
using K2DemoWorkFlow.Domain.Enum;

namespace K2DemoWorkFlow.Domain.Entities.Workflow
{
    public class Task : FullAuditedEntityBase<Guid>
    {
        public Task()
        {
            ProcessActionTrackings = new HashSet<ProcessActionTracking>();
        }

        public Task(RequestStatusEnum requestStatus, int processId, Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
                //.AsSequentialGuid();
            TaskStatusId = (int)requestStatus;
            ProcessId = processId;
            ProcessActionTrackings = new HashSet<ProcessActionTracking>();
        }

        public string Number { get; set; }
        public int? ProcessInstanceId { get; set; }
        public int? ProcessId { get; set; }
        public int? TaskStatusId { get; set; }
        public DateTime? TaskDate { get; set; }
        public string Originator { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToFullName { get; set; }
        public int? ProcessActivityId { get; set; }
        public Process Process { get; private set; }
        public TaskStatus TaskStatus { get; private set; }
        public ProcessActivity ProcessActivity { get; private set; }
        public ICollection<ProcessActionTracking> ProcessActionTrackings { get; private set; }
    }
}
