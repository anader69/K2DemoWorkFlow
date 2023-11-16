
using K2DemoWorkFlow.Application.Dto;
using K2DemoWorkFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
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
        public async Task<ActionResult> TakeAction([FromForm]WorkflowActionDTO model)
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
        public async Task<ActionResult> getUserHistory()
        {
            return Ok(await leaveRequest.getUserHistory(""));

            // //get the raw data from the OData Feed
            // //set the environment URLs and the SmartObject to query
            // //TODO: replace these values with appropriate values for your environment and target SmartObject
            // string BaseK2SmOUri = @"https://03k2sure.sure.com.sa/api/odata/v3/";
            // string SmartObjectSystemName = @"com_k2_System_Reports_SmartObject_AnalyticsProcessInstance";

            // //set credentials for basic auth
            // //TODO: replace these values with appropriate values for your env
            // string username = @"SURE\mhanna";
            // string password = "M@RCOhunter2110";

            // //example: using a WebClient to read the response
            // var webclient = new WebClient
            // {
            //     Credentials = new NetworkCredential(username, password)
            // };
            // //var webClientResponse = webclient.DownloadString(BaseK2SmOUri + SmartObjectSystemName);
            // ////output reponse as a string for simplicity
            // ////TODO: you will probably want to do some JSON deserialization here
            // //Console.WriteLine("** WEBCLIENT START **");
            // //Console.WriteLine(webClientResponse.ToString());
            // //Console.WriteLine("** WEBCLIENT END **");

            // //example: using a HTTPWebRequest to read the response
            // Console.WriteLine("** HTTPWEBREQUEST START **");
            // System.Net.HttpWebRequest request = WebRequest.Create(BaseK2SmOUri + SmartObjectSystemName+ "?$filter=Folder eq anadertestk2") as HttpWebRequest;
            // //set the credentials
            //// String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            // request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password)));
            // //get the response
            // System.Net.WebResponse response = request.GetResponse();
            // System.IO.Stream responseStream = response.GetResponseStream();
            // System.IO.StreamReader responseReader = new System.IO.StreamReader(responseStream);
            // //read the response to output as a string for simplicity
            // //TODO: you will probably want to do some JSON deserialization here
            // string responsestring = responseReader.ReadToEnd();
            // Console.WriteLine(responsestring);
            // Console.WriteLine("** HTTPWEBREQUEST END **");
            // responseReader.Close();
            // responseStream.Dispose();
            // responseStream.Dispose();











        

            // return Ok(await leaveRequest.getUserHistory(username));
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
