using Commons.K2.Proxy;
using Framework.Core;
using Framework.Core.Extensions;
using K2DemoWorkFlow.Domain.Entities.Workflow;
using K2DemoWorkFlow.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZXing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace K2DemoWorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly K2Proxy _k2Proxy;

        private readonly WorkFlowContext _dbcontext;

        public LeaveRequestController(K2Proxy k2Proxy, WorkFlowContext dbcontext)
        {
            _k2Proxy = k2Proxy;
            _dbcontext = dbcontext;
        }
        // GET: api/<LeaveRequest>
        [HttpGet]
        public async Task<ActionResult> GetAsync(string username)
        {
            var id = Guid.NewGuid();
            var result = new ReturnResult<int>();
            var dataFields = new Dictionary<WorkflowDataFields, object>();
            dataFields.Add(WorkflowDataFields.RequestId, id.ToString());

            // dataFields.Add(WorkflowDataFields.RequestId,1002);
            var wfResult = await _k2Proxy.StartWorkflowAsync(ProcessCategory.anadertestk2, ProcessNames.LeaveRequestWorkFlow, id.ToString(), dataFields);
            if (wfResult.Success != null && !wfResult.Success.Value)
            {
                result.AddErrorItem(String.Empty, "error");
                return Ok(result);
            }
            result.Value = wfResult.Value.To<int>();
            _dbcontext.Tasks.Add(new Domain.Entities.Workflow.Task()
            {
                Id = id,
                ProcessInstanceId = result.Value,
                TaskStatusId = 2,
                Originator = username,
                AssignedTo = "SURE\\MHANNA"
            });

            await _dbcontext.SaveChangesAsync();
            return Ok(result);
        }

        // GET: api/<LeaveRequest>
        [HttpGet]
        public async Task<ActionResult> GetAsync(string Originator)
        {

            _dbcontext.Tasks.Add(new Domain.Entities.Workflow.Task()
            {
                //Id = id,
                //ProcessInstanceId = result.Value,
                //TaskStatusId = 2,
                //CreatedBy = "SURE\\MHANNA",
                //AssignedTo = "SURE\\MHANNA"
            });
         TaskDto taskDto =  _dbcontext.Tasks.Where(c => c.Originator == Originator).Select(c =>
            new TaskDto { Id = c.Id, ProcessActivityAr = c.ProcessActivity.NameAr, ProcessActivityEn = c.ProcessActivity.NameEn, ProcessInstanceId = c.ProcessInstanceId, TaskStatusNameAr = c.TaskStatus.NameAr, TaskStatusNameEn = c.TaskStatus.NameEn, AssignedTo = c.AssignedTo, TaskDate = c.TaskDate }
           ).FirstOrDefault();
            //await _dbcontext.SaveChangesAsync();
            return Ok(taskDto);
        }

        // GET api/<LeaveRequest>/5
        [HttpGet]
        [Route("takeAction")]
        public async Task<ActionResult> takeAction(string SerialNumber, string Action)
        {
            var dataFields = new Dictionary<WorkflowDataFields, object>();
            var response = await _k2Proxy.TakeActionOnWorkflowAsync(SerialNumber, Action, dataFields);
            return Ok(response);
        }

        [HttpGet]
        [Route("getInbox")]
        public async Task<ActionResult> getInbox(string username)
        {
            List<string> processlist = new List<string>();
            processlist.Add("Leave.Request.WorkFlow");
            var apiResult = await _k2Proxy.GetTasksAsync(processlist);
            return Ok(apiResult);
        }

   
    }
}
