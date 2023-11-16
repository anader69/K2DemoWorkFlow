

using ClassLibrary1;
using Commons.K2;
using k2.API.Code;
using k2.API.Models;
using Microsoft.AspNetCore.Mvc;
using SourceCode.Hosting.Client.BaseAPI;

namespace k2.API.Controllers
{
    [Route("api/K2Client")]

    public class K2ClientController : BaseApi
    {
        public K2ClientController(IConfiguration iConfig) : base(iConfig)
        {
        }
        #region Post

        /// <summary>
        /// to start process with dataField
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="processName"></param>
        /// <param name="folio"></param>
        /// <param name="dataFields"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StartK2Process")]
        public ApiResponse StartK2Process([FromBody] K2StartProcess k2StartProcess)
        {
            K2Config userConfig = new K2Config();
            userConfig.ClientConnectionString = this.DefaultConnectionStringK2Client;
            var processInstanceId = 0;
            using (var client = new K2Client(userConfig, true, k2StartProcess.userName))
            {

                switch (k2StartProcess.ActionTypeName)
                {
                    case nameof(ActionType.BasicParam):
                        processInstanceId = client.StartK2Process(k2StartProcess.processName, k2StartProcess.folio);
                        break;
                    case nameof(ActionType.WithDataField):
                        processInstanceId = client.StartK2Process(k2StartProcess.processName, k2StartProcess.folio, k2StartProcess.dataFields);
                        break;
                }

            }
            return new ApiResponse { Value = processInstanceId };
        }


        /// <summary>
        /// Submit User Action
        /// </summary>
        /// <param name="k2param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ActionWorklistItem")]
        public ApiResponse<K2WorklistItem> ActionWorklistItem([FromForm] K2SubmitAction k2SubmitAction)
        {
            K2Config userConfig = new K2Config();
            userConfig.ClientConnectionString = this.DefaultConnectionStringK2Client;
            K2WorklistItem k2WorklistItem = null;
            using (var client = new K2Client(userConfig, true, k2SubmitAction.userName))
            {
                switch (k2SubmitAction.ActionTypeName)
                {
                    case nameof(ActionType.BasicParam):
                        k2WorklistItem = client.ActionWorklistItem(k2SubmitAction.serialNumber, k2SubmitAction.action, k2SubmitAction.comment, k2SubmitAction.attachment);
                        break;
                    case nameof(ActionType.WithDataField):
                        k2WorklistItem = client.ActionWorklistItem(k2SubmitAction.serialNumber, k2SubmitAction.action, k2SubmitAction.comment, k2SubmitAction.attachment, k2SubmitAction.dataFields);
                        break;
                }


            }
            return new ApiResponse<K2WorklistItem>(k2WorklistItem);
        }


        /// <summary>
        /// Submit User Action
        /// </summary>
        /// <param name="k2param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gethistory")]
        public ApiResponse<K2WorklistItem> gethistory()
        {
            K2Config userConfig = new K2Config();
            userConfig.ClientConnectionString = this.DefaultConnectionStringK2Client;
            K2WorklistItem k2WorklistItem = null;
            var k2history = new k2History();
            

                k2history.gethistory(userConfig.ClientConnectionString);

            
            return new ApiResponse<K2WorklistItem>(k2WorklistItem);
        }
        
        /// <summary>
        /// get user Inbox
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="processNames"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorklist")]
        public ApiResponse<K2Worklist> GetWorklist([FromBody] WorkListModel workListModel)
        {
            K2Config userConfig = new K2Config();
            K2Worklist filteredWorklistItems = new K2Worklist();

            userConfig.ClientConnectionString = this.DefaultConnectionStringK2Client;
            K2Worklist worklistItems;
            using (var client = new K2Client(userConfig, true, workListModel.userName))
            {
                worklistItems = client.GetWorkList(workListModel.processNames.ToArray());

                //get allocated tasks only
                foreach (var item in worklistItems)
                {
                    if (item.AllocatedUser.Equals(K2Client.FormatK2User(workListModel.userName), System.StringComparison.OrdinalIgnoreCase))
                        filteredWorklistItems.Add(item);
                }
            }

            return new ApiResponse<K2Worklist>(filteredWorklistItems);
        }


        [HttpPost]
        [Route("GetWorklistCount")]
        public ApiResponse<int> GetWorklistCount([FromBody] WorkListModel workListModel)
        {
            K2Config userConfig = new K2Config();
            int count = 0;
            userConfig.ClientConnectionString = this.DefaultConnectionStringK2Client;
            K2Worklist worklistItems;
            K2Worklist filteredWorklistItems = new K2Worklist();
            using (var client = new K2Client(userConfig, true, workListModel.userName))
            {
                worklistItems = client.GetWorkList(workListModel.processNames.ToArray());

                //get allocated tasks only
                foreach (var item in worklistItems)
                {
                    if (item.AllocatedUser.Equals(K2Client.FormatK2User(workListModel.userName), System.StringComparison.OrdinalIgnoreCase))
                        filteredWorklistItems.Add(item);
                }

                count = filteredWorklistItems.Count;
            }

            return new ApiResponse<int>(count);
        }


        /// <summary>
        /// Get Activity Details
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="serialNumber"></param>
        /// <param name="impersonateUser"></param>
        /// <param name="allocate"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorklistItem/{K2listItem=K2listItem}")]
        public ApiResponse<K2WorklistItem> GetWorklistItem([FromBody] K2listItem k2listItem)
        {
            K2Config userConfig = new K2Config();
            userConfig.ClientConnectionString = this.DefaultConnectionStringK2Client;
            K2WorklistItem k2WorklistItem = null;
            using (var client = new K2Client(userConfig, true, k2listItem.userName))
            {
                switch (k2listItem.ActionTypeName)
                {
                    case nameof(ActionType.BasicParam):
                        k2WorklistItem = client.GetWorklistItem(k2listItem.serialNumber, k2listItem.allocate);
                        break;
                    case nameof(ActionType.withImpersonation):
                        k2WorklistItem = client.GetWorklistItem(k2listItem.serialNumber, k2listItem.impersonateUser, k2listItem.allocate);
                        break;
                }
            }
            return new ApiResponse<K2WorklistItem>(k2WorklistItem);
        }



        #endregion

        #region Get


        #endregion

    }
}