
using K2DemoWorkFlow.Domain.Enum;

namespace K2DemoWorkFlow.Domain.Entities.Workflow
{
    public class TaskDto  
    {  
         public Guid Id { get; set; }
         public int? ProcessInstanceId { get; set; }
        public string ProcessName { get; set; }
        public string TaskStatusNameAr { get; set; }
        public string TaskStatusNameEn { get; set; }
         public DateTime? TaskDate { get; set; }
        public string Originator { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToFullName { get; set; }
        public string ProcessActivityAr { get; set; }
        public string ProcessActivityEn { get; set; }
     }
}
