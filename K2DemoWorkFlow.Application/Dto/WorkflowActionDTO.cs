using Microsoft.AspNetCore.Http;

namespace K2DemoWorkFlow.Application.Dto
{
    public class WorkflowActionDTO
    {
        public string SerialNumber { get; set; }
        public string Action { get; set; }
        public int ProcessId { get; set; }
        public string username { get; set; }
        public string comment { get; set; }
        public IFormFile attachment { get; set; }


    }
}
