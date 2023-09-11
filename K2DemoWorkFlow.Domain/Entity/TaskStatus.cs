
using K2DemoWorkFlow.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.Domain.Entities.Workflow
{
    public class TaskStatus : EntityBase<int>
    {
        public TaskStatus()
        {
            ProcessActionTrackings = new HashSet<ProcessActionTracking>();
            Tasks = new HashSet<Task>();
        }
        [NotMapped]
        public string Name =>  NameAr ;
        //public string Name => CultureHelper.IsArabic ? NameAr : NameEn;
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [NotMapped]
        public string ApplicantStatus => ApplicantStatusAr;
        //public string ApplicantStatus => CultureHelper.IsArabic ? ApplicantStatusAr : ApplicantStatusEn;

        public string ApplicantStatusAr { get; set; }
        public string ApplicantStatusEn { get; set; }
        public bool ShowInInbox { get; set; }
        public bool ShowInRequest { get; set; }
        public string Code { get; set; }
        public int? ProcessId { get; set; }
        public string ActivityTitle { get; set; }
        public Process Process { get; private set; }
        public ICollection<ProcessActionTracking> ProcessActionTrackings { get; private set; }
        public ICollection<Task> Tasks { get; private set; }

        //public short? ApplicationStageId { get; set; }
        //public short? StatusVisibilityId { get; set; }
        //public string DescriptionAr { get; set; }
        //public string DescriptionEn { get; set; }
    }
}
