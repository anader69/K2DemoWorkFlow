using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Commons.Framework.Extensions;
using Commons.K2.Proxy;
using K2DemoWorkFlow.Application.Dto;
using K2DemoWorkFlow.Application.IReprositary;
using K2DemoWorkFlow.Application.Services;
using K2DemoWorkFlow.Application.WorkFlowServices;
using K2DemoWorkFlow.Domain.Enum;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace K2DemoWorkFlow.Application.ServicesImplementation
{
    public class LeaveRequest : ILeaveRequest
    {
        private readonly IWorkFlowProcess workFlowProcess;
        private readonly IleaveRequestReprositary leaveRequestReprositary;
        private readonly IConfiguration _configuration;

        public LeaveRequest(IWorkFlowProcess _WorkFlowProcess, IleaveRequestReprositary _leaveRequestReprositary, IConfiguration configuration)
        {
            workFlowProcess = _WorkFlowProcess;
            leaveRequestReprositary = _leaveRequestReprositary;
            _configuration = configuration;
        }
        public async Task<ApiResponseOfK2Worklist> GetInbox(string username)
        {
            List<string> processlist = new List<string>();
            processlist.Add("Leave.Request.WorkFlow");
           var data=await workFlowProcess.GetTasksAsync(processlist, username);
         
            return data;
            
        }

        public  List<TaskDto> GetUserTask(string Originator)
        {
            var task = leaveRequestReprositary.GetUserTask(Originator);
            List<TaskDto> taskDto = new List<TaskDto>();
            foreach (var item in task)
            {
                taskDto.Add(new TaskDto
                { Id = item.Id, ProcessInstanceId = item.ProcessInstanceId, TaskStatusNameAr = item.TaskStatus.NameAr, TaskStatusNameEn = item.TaskStatus.NameEn, AssignedTo = item.AssignedTo, TaskDate = item.TaskDate });
            }
            return taskDto;


        }

        public async Task<int> StartWorkflowAsync(ProcessDTO model)
        {
            var id = Guid.NewGuid();
            var result = new ReturnResult<int>();
            var dataFields = new Dictionary<WorkflowDataFields, object>();
            dataFields.Add(WorkflowDataFields.RequestId, id.ToString());
            var wfResult = await workFlowProcess.StartWorkflowAsync(ProcessCategory.anadertestk2, ProcessNames.LeaveRequestWorkFlow, id.ToString(), dataFields);

            if (wfResult.Success != null && !wfResult.Success.Value)
            {
                throw new Exception("Error");
               
            }
            result.Value = wfResult.Value.To<int>();
            var saveresult = await leaveRequestReprositary.SaveTask(new Domain.Entities.Workflow.Task()
            {
                Id = id,
                Number = result.Value.ToString(),
                ProcessInstanceId = result.Value,
                TaskStatusId = (int)TaskAction.Submitted,
                Originator = model.username,
                AssignedToFullName = model.AssignedTo,
                AssignedTo = model.AssignedTo,
                TaskDate = DateTime.Now,

            });

            return result.Value;

        }

        public async Task<bool> TakeActionAsync(WorkflowActionDTO model)
        {
            var dataFields = new Dictionary<WorkflowDataFields, object>();
            var response = await workFlowProcess.TakeActionOnWorkflowAsync(model.SerialNumber, model.Action, model.username, model.comment, model.attachment, dataFields);
            if (response.Success != null && !response.Success.Value)
            {
                throw new Exception("Error");

            }

            var action=(TaskAction)  Enum.Parse(typeof(TaskAction), model.Action);
            var result=await leaveRequestReprositary.UpdateTask(action, model.ProcessId);

            return result;
        
        }


        public async Task< List<TaskDto>> getUserHistory(string Originator)
        {
            var task =await workFlowProcess.getUserHistory(Originator);
            List<TaskDto> taskDto = new List<TaskDto>();
            foreach (var item in task.Value)
            {
                taskDto.Add(new TaskDto
                {  ProcessInstanceId = item.ProcessInstanceId, TaskDate = item.TaskDate });
            }
            return taskDto;


        }


        public string GetLoginUrl()
        {
            var tenantID = _configuration.GetValue<string>("tenantID");
            var clinetID = _configuration.GetValue<string>("clinetID");
            var client_secret = _configuration.GetValue<string>("client_secret");
            var redirectUrl = _configuration.GetValue<string>("redirectUrl");
            //var coedverifier = "EvkUOGiwfeQ0ORAfySg4pcP33K3ha7moL14ByjycOsY";

            var reqContent = "?response_type=id_token&redirect_uri="+ redirectUrl + "&client_id=" + clinetID + "&response_mode=fragment&state=12345&nonce=678910&scope=openid+email";
          var url=( "https://login.microsoftonline.com/" + tenantID + "/oauth2/v2.0/authorize" + reqContent).ToString();
            
          return url;


        }



        public string UserToken(string id_token)
        {
            //var tenantID = "cd656731-140b-47f0-bf68-ec25880ccfff";
            //var clinetID = "a5f32666-967b-41a6-a7ff-4aa757af016e";
            //var client_secret = "RMx8Q~AJ7z7f4dlqR-oVKvIUDyfvCzD8M2FkpbWG";
            //var coedverifier = "EvkUOGiwfeQ0ORAfySg4pcP33K3ha7moL14ByjycOsY";

            //HttpClient client = new HttpClient();
            //var reqContent = @"grant_type=authorization_code&client_id=" + clinetID + "&code="+ code + "&redirect_uri=http://localhost:44415/" + "&code_verifier="+coedverifier + "&client_secret=" + System.Web.HttpUtility.UrlEncode(client_secret) + "&scope=openid+profile+email";
            //var Content = new StringContent(reqContent, Encoding.UTF8, "application/x-www-form-urlencoded");
            //var responsetoken = client.PostAsync("https://login.microsoftonline.com/" + tenantID + "/oauth2/v2.0/token", Content).Result;
            //var token = responsetoken.Content.ReadAsStringAsync().Result;
            //return token;


            // A jwt encoded token string in this case extracted from the 'Authorization' HTTP header

            // Trim 'Bearer ' from the start since its just a prefix for the token
          //  var jwtEncodedString = id_token.Substring(7);

            // Instantiate a new Jwt Security Token from the Jwt Encoded String
            var token = new JwtSecurityToken(id_token);
            var useremail = token.Claims.First(c => c.Type == "email").Value;
            // Retrieve info from the Json Web Token 
            // Console.WriteLine("email => " + token.Claims.First(c => c.Type == "Email").Value);
           return useremail;
        }
    }
}
