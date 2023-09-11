
using Framework.Core.Globalization;
using K2DemoWorkFlow.Domain.Entity;

namespace K2DemoWorkFlow.Domain.Entities.Workflow
{
    public class ProcessAction : EntityBase<int>
    {
        public ProcessAction()
        {

        }

        public int ProcessId { get; private set; }
        public string ActionName { get; set; }
        public string ActivityTitle { get; private set; }
        public string NameAr { get; private set; }
        public string NameEn { get; private set; }
        public string ActivityName { get; private set; }
        public bool IsCommentRequired { get; private set; }
        public bool IsCommentVisible { get; private set; }
        public int ProcessActivityId { get; private set; }
        //public int? TaskStatusId { get; private set; }
        //public TaskStatus TaskStatus { get; set; }
        public Process Process { get; private set; }
        public ProcessActivity ProcessActivity { get; private set; }
       
        public string Name => CultureHelper.IsArabic ? this.NameAr : this.NameEn;

    }
}
