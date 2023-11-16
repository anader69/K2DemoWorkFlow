using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commons.K2.Proxy
{
    public class K2Proxy
    {
       // private readonly AppSettingsService _appSettingsService;
       // private readonly IUserAppService _identityUserAppService;
        private readonly K2Client _k2Client;
        private readonly K2ManagementClient _k2ManagementClient;


        public K2Proxy()
        {
           // _appSettingsService = appSettingsService;
           // _identityUserAppService = identityUserAppService;
           // _k2Client = new K2Client(_appSettingsService.K2WebApiUrl);
            _k2Client = new K2Client("https://localhost:44339");
            _k2ManagementClient = new K2ManagementClient("https://localhost:44339");
           // _k2ManagementClient = new K2ManagementClient(_appSettingsService.K2WebApiUrl);
        
        }

        /// <summary>
        /// start a new workflow instance
        /// </summary>
        /// <param name="processCategory"></param>
        /// <param name="processName"></param>
        /// <param name="requestId">request id</param>        
        /// <param name="dataFields">data fields to pass to workflow instance</param>        
        /// <returns>response which has the InstanceId as value of the response ex:response.Value </returns>
        public async Task<ApiResponse> StartWorkflowAsync(ProcessCategory processCategory, ProcessNames processName, string requestId, Dictionary<WorkflowDataFields, object> dataFields = null)
        {
            K2StartProcess k2StartProcess = new K2StartProcess
            {
                ProcessName = $"{processCategory.ToString()}\\{"Leave.Request.WorkFlow".ToString()}",
                Folio = requestId,
                ActionTypeName = dataFields != null ? nameof(ActionTypeEnum.WithDataField) : nameof(ActionTypeEnum.BasicParam),
                DataFields = dataFields?.ToDictionary(p => p.Key.ToString(), p => p.Value),
                UserName = "SURE\\mhanna"
            };

            return await _k2Client.StartK2ProcessAsync(k2StartProcess);
        }



        /// <summary>
        /// take action on workflow
        /// </summary>
        /// <param name="taskSerialNumber">serial number of the task</param>
        /// <param name="actionName">name of the action</param>
        /// <param name="userName">current user login name</param>
        /// <param name="dataFields">data fields to pass to workflow instance</param>
        /// <returns>response which contains the task details ex:response.Value</returns>
        public async Task<ApiResponseOfK2WorklistItem> TakeActionOnWorkflowAsync(string taskSerialNumber, string actionName,string username, string comment, IFormFile attachment, Dictionary<WorkflowDataFields, object> dataFields = null)
        {
            var k2Action = new K2SubmitAction
            {
                SerialNumber = taskSerialNumber,
                ActionTypeName = dataFields != null ? nameof(ActionTypeEnum.WithDataField) : nameof(ActionTypeEnum.BasicParam),
                DataFields = dataFields?.ToDictionary(p => p.Key.ToString(), p => p.Value),
                UserName = username,
                Action = actionName,
                comment = comment,
                Attacment = attachment
                
            };
            // UserName = _identityUserAppService.CurrentUserName,

            return await _k2Client.ActionWorklistItemAsync(k2Action);
        }

        public async Task<ApiResponseOfK2Worklist> GetTasksAsync(List<string> processNamesList,string UserName)
        {
            WorkListModel workListModel = new WorkListModel();
             workListModel.UserName = "";
            // workListModel.UserName = _identityUserAppService.CurrentUserName;
            workListModel.UserName = UserName;
            workListModel.CategotyName = "All";
            workListModel.ProcessNames = processNamesList;
            return await _k2Client.GetWorklistPOSTAsync(workListModel);
        }


        

       public async Task<ApiResponseOfK2Worklist> getUserHistory( string username)
        {
  
            return await _k2Client.IsgetUserHistory(username);
        }

        public async Task<ApiResponseOfK2Worklist> GetTasksOfUserAsync(List<string> processNamesList, string username)
        {
            WorkListModel workListModel = new WorkListModel();
            workListModel.UserName = username;
            workListModel.CategotyName = "All";
            workListModel.ProcessNames = processNamesList;
            return await _k2Client.GetWorklistPOSTAsync(workListModel);
        }

        public async Task<ApiResponse> GetTasksCountAsync(List<string> processNamesList)
        {
            WorkListModel workListModel = new WorkListModel();
            workListModel.UserName = "";
           // workListModel.UserName = _identityUserAppService.CurrentUserName;
            workListModel.CategotyName = "All";
            workListModel.ProcessNames = processNamesList;
            return await _k2Client.GetWorklistCountPOSTAsync(workListModel);
        }

        public async Task<ApiResponseOfK2WorklistItem> GetTaskDetailsAsync(string serialNumber)
        {
            var k2ListItem = new K2listItem
            {
                SerialNumber = serialNumber,
                Allocate = true,
                ActionTypeName = nameof(ActionTypeEnum.BasicParam),
                UserName = ""
                //_identityUserAppService.CurrentUserName
            };

            return await _k2Client.GetWorklistItemAsync(k2ListItem);
        }

        public async Task<ApiResponse> RedirectTaskAsync(K2WorklistItem task, string fromUser, string toUser)
        {
            var k2ListItem = new K2Delegation()
            {
                Task = task,
                OldUser = $"{fromUser}",
                NewUser = $"{toUser}",
            };

            return await _k2ManagementClient.RedirectTaskAsync(k2ListItem);
        }

        public K2WorklistItem GetWorklistItemByFolio(string folio)
        {
            var apiresult = _k2Client.GetAllWorklistItemsByFolioOnlyAsync(folio).Result;
            var worklistItems = apiresult.Value.ToList();
            return worklistItems.FirstOrDefault();
            //.Where(w => w.AllocatedUser.ToLower() != FormatUser(WorkFlowSettings.K2ServerAdmin).ToLower()).FirstOrDefault();
        }
    }
}
