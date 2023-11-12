using Commons.K2.Proxy;
using K2DemoWorkFlow.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Application.Services
{
    public interface ILeaveRequest
    {
        Task< int> StartWorkflowAsync(ProcessDTO model);
        List<TaskDto> GetUserTask(string Originator);

        Task<bool> TakeActionAsync(WorkflowActionDTO model);
        Task<ApiResponseOfK2Worklist> GetInbox(string username);
        Task<List<TaskDto>> getUserHistory(string Originator);
        string UserToken(string code);
        string GetLoginUrl();
    }
}
