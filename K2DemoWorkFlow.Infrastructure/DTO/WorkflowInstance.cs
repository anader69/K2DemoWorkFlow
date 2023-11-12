using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Infrastructure.DTO
{
    public class WorkflowInstance
    {
        [DataMember(Name = "expectedDuration")]
        public long ExpectedDuration { get; set; }

        [DataMember(Name = "folio")]
        public string Folio { get; set; }

        [DataMember(Name = "priority")]
        public long Priority { get; set; }


        [DataMember(Name = "dataFields")]
        public dynamic DataFields { get; set; }

        
    }




  
}
