using Commons.K2.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace K2DemoWorkFlow.Application.WorkFlowServices
{
    public interface IWorkFlowProcess
    {
        Task<ApiResponse> StartWorkflowAsync(ProcessCategory processCategory, ProcessNames processName, string requestId, Dictionary<WorkflowDataFields, object> dataFields = null);
        Task<ApiResponseOfK2WorklistItem> TakeActionOnWorkflowAsync(string taskSerialNumber, string actionName, string username,string comment,IFormFile attachment, Dictionary<WorkflowDataFields, object> dataFields = null);
        Task<ApiResponseOfK2Worklist> GetTasksAsync(List<string> processNamesList, string UserName);
        Task<ApiResponseOfK2Worklist> getUserHistory(string username);
    }
}
