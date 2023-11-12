

namespace K2DemoWorkFlow.Domain.Entities.Workflow
{
    public class ProcessActionTracking : FullAuditedEntityBase<Guid>
    {
        public Guid TaskId { get; set; }
        public int ProcessActionId { get; set; }
        public int? ProcessId { get; set; }
        public DateTime TaskAssignDate { get; set; }
        public string Comments { get; set; }
        public int? TaskStatusId { get; set; }
        public int OrderNumber { get; set; }
        public string DelegatedFrom { get; set; }
        public string DelegatedTo { get; set; }
        public Process Process { get; private set; }
        public Task Task { get; private set; }
        public ProcessAction ProcessAction { get; private set; }
        public TaskStatus TaskStatus { get; private set; }

    }
}
