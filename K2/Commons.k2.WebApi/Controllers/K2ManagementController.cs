using Commons.K2;
using k2.API.Code;
using Microsoft.AspNetCore.Mvc;
using SourceCode.Workflow.Management;

namespace k2.API.Controllers
{
    [Route("api/K2Management")]
    public class K2ManagementController : BaseApi
    {
        public K2ManagementController(IConfiguration iConfig) : base(iConfig)
        {
        }
        #region Post
        /// <summary>
        /// to start procees instance by instance Id
        /// </summary>
        /// <param name="processInstanceId"></param>
        /// <returns></returns>
        [HttpPost("StartProcessInstance")]
        public ApiResponse StartProcessInstance([FromBody] int processInstanceId)
        {
            if (processInstanceId < 1)
            {
                throw new ArgumentOutOfRangeException(processInstanceId.ToString());
            }

            bool retVal;
            using (var manager = new K2Management())
            {
                retVal = manager.StartProcessInstance(processInstanceId);
            }

            return new ApiResponse { Value = retVal };
        }

        /// <summary>
        /// for delegation users
        /// </summary>
        /// <param name="task"></param>
        /// <param name="oldUser"></param>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("RedirectTask")]
        public ApiResponse RedirectTask([FromBody] K2Delegation k2delegation)
        {
            if (k2delegation.Task == null)
            {
                throw new ArgumentNullException(nameof(k2delegation.Task));
            }

            if (string.IsNullOrEmpty(k2delegation.OldUser))
            {
                throw new ArgumentNullException(nameof(k2delegation.OldUser));
            }

            if (string.IsNullOrEmpty(k2delegation.NewUser))
            {
                throw new ArgumentNullException(nameof(k2delegation.NewUser));
            }

            bool retVal;
            using (var manager = new K2Management())
            {
                retVal = manager.RedirectTask(k2delegation.Task, k2delegation.OldUser, k2delegation.NewUser);
            }

            return new ApiResponse { Value = retVal };
        }

        #endregion

        #region Get
        /// <summary>
        /// to get process instance by instanceId
        /// </summary>
        /// <param name="processInstanceId"></param>
        /// <returns></returns>
        [HttpGet("GetProcessInstanceByInstanceId")]
        public ApiResponse GetProcessInstanceByInstanceId(int processInstanceId)
        {
            if (processInstanceId <= 1)
            {
                throw new ArgumentOutOfRangeException(processInstanceId.ToString());
            }
            ProcessInstance processInstance;
            using (var manager = new K2Management())
            {
                processInstance = manager.GetProcessInstanceByInstanceId(processInstanceId);
            }

            return new ApiResponse { Value = processInstance };
        }

        /// <summary>
        /// check if process instance is completed
        /// </summary>
        /// <param name="processInstanceId"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        [HttpGet("IsProcessInstanceCompleteState")]
        public ApiResponse IsProcessInstanceCompleteState(int processInstanceId, string folio)
        {
            if (processInstanceId <= 1)
            {
                throw new ArgumentOutOfRangeException(processInstanceId.ToString());
            }
            bool retVal;
            using (var manager = new K2Management())
            {
                retVal = manager.IsProcessInstanceCompleteState(processInstanceId, folio);
            }

            return new ApiResponse { Value = retVal };
        }

        /// <summary>
        /// check if process instance is exsisted
        /// </summary>
        /// <param name="processInstanceId"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        [HttpGet("IsProcessInstanceExist")]
        public ApiResponse IsProcessInstanceExist(int processInstanceId, string folio)
        {
            if (processInstanceId <= 1)
            {
                throw new ArgumentOutOfRangeException(processInstanceId.ToString());
            }
            bool retVal;
            using (var manager = new K2Management())
            {
                retVal = manager.IsProcessInstanceExist(processInstanceId);
            }

            return new ApiResponse { Value = retVal };
        }

        /// <summary>
        /// to move the process to specific activivty
        /// </summary>
        /// <param name="processInstanceId"></param>
        /// <param name="activityName"></param>
        /// <returns></returns>
        [HttpGet("GoToActivity")]
        public ApiResponse GoToActivity(int processInstanceId, string activityName)
        {
            if (processInstanceId <= 1)
            {
                throw new ArgumentOutOfRangeException(processInstanceId.ToString());
            }

            if (string.IsNullOrEmpty(activityName))
            {
                throw new ArgumentNullException(processInstanceId.ToString());
            }

            bool retVal;
            using (var manager = new K2Management())
            {
                retVal = manager.GoToActivity(processInstanceId, activityName);
            }

            return new ApiResponse { Value = retVal };
        }

        /// <summary>
        /// to check if user is destination in process that has current serialNumber
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("IsDestinationUser")]
        public ApiResponse IsDestinationUser(string serialNumber, string userName)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                throw new ArgumentNullException(serialNumber.ToString());
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(userName);
            }

            bool retVal;
            using (var manager = new K2Management())
            {
                retVal = manager.IsDestinationUser(serialNumber, userName);
            }

            return new ApiResponse { Value = retVal };
        }

        /// <summary>
        /// get all Participated Users in current request
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpGet("GetAllParticipatedUsers")]
        public ApiResponse GetAllParticipatedUsers(Guid requestId)
        {
            if (requestId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(requestId));
            }

            List<string> retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetAllParticipatedUsers(requestId);
            }

            return new ApiResponse { Value = retList };
        }

        /// <summary>
        /// get all Participated Users in current event
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        [HttpGet("GetAllParticipatedUsersInEventName")]
        public ApiResponse GetAllParticipatedUsersInEventName(Guid requestId, string eventName)
        {
            if (requestId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(requestId));
            }

            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException(eventName);
            }

            List<string> retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetAllParticipatedUsersInEventName(requestId, eventName);
            }

            return new ApiResponse { Value = retList };
        }

        /// <summary>
        /// to get all work lest details 
        /// </summary>
        /// <param name="processNames"></param>
        /// <returns></returns>
        [HttpGet("GetUserWorklistItems")]
        public ApiResponse GetUserWorklistItems([FromQuery] string[] processNames)
        {
            if (processNames == null || processNames.Count() <= 0)
            {
                throw new ArgumentNullException(nameof(processNames));
            }

            K2ManagerWorklist retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetAllWorklistItems(processNames);
            }
            return new ApiResponse { Value = retList };
        }

        /// <summary>
        /// to get all work list item by folio
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="processNames"></param>
        /// <returns></returns>
        [HttpGet("GetAllWorklistItemsByFolio")]
        public ApiResponse GetAllWorklistItemsByFolio(string folio, [FromQuery] string[] processNames)
        {
            if (processNames == null || processNames.Count() <= 0)
            {
                throw new ArgumentNullException(nameof(processNames));
            }

            K2ManagerWorklist retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetAllWorklistItemsByFolio(folio, processNames);
            }

            return new ApiResponse { Value = retList };
        }

        [HttpGet("GetWorklistItemByFolio")]
        public ApiResponse GetWorklistItemByFolio(string folio)
        {

            K2ManagerWorklist retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetWorklistItemByFolio(folio);
            }

            return new ApiResponse { Value = retList };
        }


        /// <summary>
        /// to return Folio for current user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("GetUserWorklistFolioItems")]
        public ApiResponse GetUserWorklistFolioItems(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            List<string> retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetUserWorklistFolioItems(userName);
            }

            return new ApiResponse { Value = retList };
        }

        /// <summary>
        /// return list of summery for user task
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        [HttpGet("GetUsersTasksSummary")]
        public ApiResponse GetUsersTasksSummary(List<string> userNames)
        {
            if (userNames == null || userNames.Count() <= 0)
            {
                throw new ArgumentNullException(nameof(userNames));
            }

            List<K2TaskSummary> retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetUsersTasksSummary(userNames);
            }

            return new ApiResponse { Value = retList };
        }

        /// <summary>
        /// to return list of summery by daterange for current user
        /// </summary>
        /// <param name="userNames"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GetUsersTasksSummaryByDateRange")]
        public ApiResponse GetUsersTasksSummaryByDateRange(List<string> userNames, DateTime startDate, DateTime endDate)
        {
            if (userNames == null || userNames.Count() <= 0)
            {
                throw new ArgumentNullException(nameof(userNames));
            }


            if (startDate == null)
            {
                throw new ArgumentNullException(nameof(startDate));
            }

            if (endDate == null)
            {
                throw new ArgumentNullException(nameof(endDate));
            }

            List<K2TaskSummary> retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetUsersTasksSummaryByDateRange(userNames, startDate, endDate);
            }

            return new ApiResponse { Value = retList };
        }

        [HttpGet("GetAllWorklistItemsByFolioOnly")]
        public ApiResponse GetAllWorklistItemsByFolioOnly(string folio)
        {
            K2ManagerWorklist retList;
            using (var manager = new K2Management())
            {
                retList = manager.GetAllWorklistItemsByFolioOnly(folio);
            }

            return new ApiResponse { Value = retList };
        }

        #endregion

    }
}
