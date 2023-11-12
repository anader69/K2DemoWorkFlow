using Commons.K2.Proxy;
using K2DemoWorkFlow.Application.WorkFlowServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Infrastructure.WorkFlowImplementation
{
    public class WorkFlowProcess : IWorkFlowProcess
    {
        private readonly K2Proxy k2Proxy;

        public WorkFlowProcess(K2Proxy _k2Proxy)
        {
            k2Proxy = _k2Proxy;
        }
        public async Task<ApiResponseOfK2Worklist> GetTasksAsync(List<string> processNamesList, string UserName)
        {
            var apiResult = await k2Proxy.GetTasksAsync(processNamesList, UserName);
            return apiResult;
        }

        public async Task<ApiResponse> StartWorkflowAsync(ProcessCategory processCategory, ProcessNames processName, string requestId, Dictionary<WorkflowDataFields, object> dataFields = null)
        {

            //var dataFields = new Dictionary<WorkflowDataFields, object>();
            //dataFields.Add(WorkflowDataFields.RequestId, RequestId.ToString());
            var wfResult = await k2Proxy.StartWorkflowAsync(ProcessCategory.anadertestk2, ProcessNames.LeaveRequestWorkFlow, requestId, dataFields);
            return wfResult;
        }

        public async Task<ApiResponseOfK2WorklistItem> TakeActionOnWorkflowAsync(string taskSerialNumber, string actionName, string username, Dictionary<WorkflowDataFields, object> dataFields = null)
        {

            var response = await k2Proxy.TakeActionOnWorkflowAsync(taskSerialNumber, actionName, username, dataFields);
            return response;
        }

        public async Task<ApiResponseOfK2Worklist> getUserHistory(string username)
        {

            var response = await k2Proxy.getUserHistory( username);
            return response;
        }
        
    }
}
