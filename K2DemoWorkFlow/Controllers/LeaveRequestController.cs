
using K2DemoWorkFlow.Application.Dto;
using K2DemoWorkFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace K2DemoWorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequest leaveRequest;

        public LeaveRequestController(ILeaveRequest _leaveRequest)
        {
            leaveRequest = _leaveRequest;
        }
        
        [HttpPost]
        [Route("startworkflow")]
        public async Task<ActionResult> StartWorkflow(ProcessDTO model)
        {
            return Ok(await leaveRequest.StartWorkflowAsync(model));
       
        }

        // GET: api/<LeaveRequest>
        [HttpGet]
        [Route("getusertask")]
        public ActionResult GetUserTask(string Originator)
        {
            return Ok( leaveRequest.GetUserTask(Originator));
        }

     
        [HttpPost]
        [Route("takeAction")]
        public async Task<ActionResult> TakeAction(WorkflowActionDTO model)
        {
            return Ok(await leaveRequest.TakeActionAsync(model));
        
        }

        [HttpGet]
        [Route("getInbox")]
        public async Task<ActionResult> GetInbox(string username)
        {
            return Ok(await leaveRequest.GetInbox(username));
        }

        [HttpGet]
        [Route("getUserHistory")]
        public async Task<ActionResult> getUserHistory(string username)
        {
            return Ok(await leaveRequest.getUserHistory(username));
        }


        [HttpGet]
        [Route("GetUrl")]
        public ActionResult GetUrl()

        
        {
            var c = leaveRequest.GetLoginUrl();
            return Ok(new {url=c});
        }

        [HttpGet]
        [Route("auth")]
        public async Task<ActionResult> auth(string id_token)
        {
            return Ok(leaveRequest.UserToken(id_token));
        }


        public class tokenModel
        {
            
            public string id_token { get; set; }

           
            public string scope { get; set; }
            public string state { get; set; }
        }

    }
}
