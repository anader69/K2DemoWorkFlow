using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Application.Dto
{
    public class WorkFlowTaskDTO
    {
     
            public string Folio { get; set; }

            public int? Id { get; set; }


            public string OriginatorName { get; set; }

            public string ProcessFullName { get; set; }

            public string ActivityName { get; set; }

            public string Url { get; set; }

            public string SerialNumber { get; set; }

            public System.DateTime? CreatedOn { get; set; }

            public string AllocatedUser { get; set; }

            public string EventName { get; set; }

            public int? ProcessInstanceId { get; set; }

            public System.DateTime? TaskDate { get; set; }

            public string UserName { get; set; }

            public string ActionName { get; set; }

            public int? ProcessInstanceStatus { get; set; }

            public int? WorklistItemStatus { get; set; }

            public int? ActInstDestID { get; set; }

            public int? EventID { get; set; }

    

        
    }
}
