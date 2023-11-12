
namespace K2DemoWorkFlow.Domain.Entities.Workflow
{
    public class ProcessActivity : EntityBase<int>
    {
        public int? ProcessId { get; private set; }
        public string Name { get; private set; }
        public string AccessRoleKey { get; private set; }
        public string TaskDecisionUrl { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Process Process { get; private set; }
    }
}
