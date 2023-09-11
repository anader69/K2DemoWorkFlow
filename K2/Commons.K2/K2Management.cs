// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2Management.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the K2Management type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.K2
{
    using Commons.Framework.Logging;
    using SourceCode.Workflow.Management;
    using SourceCode.Workflow.Management.Criteria;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// The K2 Management
    /// </summary>    
    public class K2Management : IDisposable
    {
        #region Static Fields

        /// <summary>
        ///     The logger
        /// </summary>
        private Logger Logger;

        #endregion

        #region Fields

        /// <summary>
        ///     The workflow management server
        /// </summary>
        private readonly WorkflowManagementServer workflowManagementServer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="K2Management" /> class.
        /// </summary>
        public K2Management()
            : this(K2Config.GetFromConnectionStrings())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="K2Management"/> class.
        /// </summary>
        /// <param name="config">
        /// The configuration.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// config
        /// </exception>
        /// <exception cref="System.Exception">
        /// $Unable to connect to K2 server with connection string:
        ///     {config.ClientConnectionString}
        /// </exception>
        public K2Management(K2Config config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            try
            {
                var baseType = GetType().BaseType ?? GetType();
                Logger = new Logger(baseType);

                this.workflowManagementServer = new WorkflowManagementServer();
                this.workflowManagementServer.Open(config.ManagementConnectionString);

                Logger.Info("K2 Management connection opened.");
            }
            catch (Exception exception)
            {
                this.Dispose();
                Logger.Fatal("Can't connect to K2 management server!", exception);
                throw new Exception(
                    $"Unable to connect to K2 server management with connection string: {config.ManagementConnectionString}",
                    exception);
            }

        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.workflowManagementServer.Connection?.Close();
            this.workflowManagementServer.Connection?.Dispose();
            Logger.Info("K2 Management connection closed.");
        }

        /// <summary>
        /// Get the least work weight user
        /// </summary>
        /// <param name="users"></param>
        /// <returns>User Login Name </returns>
        //public string GetLeastWork(IEnumerable<string> users)
        //{
        //    var usersData = new Dictionary<string, int>();

        //    foreach (var user in users)
        //    {
        //        usersData[user] = GetUserWorklistItems(user).Count;
        //    }

        //    return usersData.OrderBy(i => i.Value).Select(i => i.Key).First();
        //}

        /// <summary>
        /// Stops the process instance.
        /// </summary>
        /// <param name="processInstanceId">The process instance identifier. in</param>
        /// <returns></returns>
        public bool StopProcessInstance(int processInstanceId)
        {
            Logger.Info($"K2Management::StopProcessInstance of instanceId:{processInstanceId}");

            return this.workflowManagementServer.StopProcessInstances(processInstanceId);
        }

        /// <summary>
        /// Starts the process instance.
        /// </summary>
        /// <param name="processInstanceId">The process instance identifier. in</param>
        /// <returns></returns>        
        public bool StartProcessInstance(int processInstanceId)
        {
            Logger.Info($"K2Management::StartProcessInstance of instanceId:{processInstanceId}");
            return this.workflowManagementServer.StartProcessInstances(processInstanceId);
        }

        /// <summary>
        /// Gets the process instance by instance identifier.
        /// </summary>
        /// <param name="processInstanceId">The process instance identifier.</param>
        /// <returns></returns>        
        public ProcessInstance GetProcessInstanceByInstanceId(int processInstanceId)
        {
            // Get all Process Instances
            var processInstances = workflowManagementServer.GetProcessInstances();

            // loop over the Process Instances
            return processInstances.Cast<ProcessInstance>()
                .FirstOrDefault(processInstance => processInstance.ID == processInstanceId);
        }

        /// <summary>
        /// Determines whether [is process instance stopped] 
        /// [the specified process instance identifier].
        /// </summary>
        /// <param name="processInstanceId">The process instance identifier.</param>
        /// <param name="folio">The folio.</param>
        /// <returns></returns>
        /// in
        public bool IsProcessInstanceStopped(int processInstanceId, string folio)
        {
            Logger.Info($"\n\t check if K2Management::IsProcessInstanceStopped of processInstanceId:{processInstanceId}\n");

            var processInstances = workflowManagementServer.GetProcessInstances(folio);

            // loop over the Process Instances
            // SELECT * FROM [K2].[ServerLog].[Status] where GroupName='Process'
            // Stopped = 4
            return processInstances.Cast<ProcessInstance>().Any(processInstance => processInstance.ID == processInstanceId
            && processInstance.Status == "4");
        }

        /// <summary>
        /// Determines whether [is process instance deleted or in error state] 
        /// [the specified process instance identifier].
        /// </summary>
        /// <param name="processInstanceId">The process instance identifier.</param>
        /// <param name="folio">The folio.</param>
        /// <returns></returns>
        /// in
        public bool IsProcessInstanceDeletedOrInErrorState(int processInstanceId, string folio)
        {
            Logger.Info($"\n\t check if K2Management::IsProcessInstanceDeletedOrInErrorState of processInstanceId:{processInstanceId}\n");

            var processInstances = workflowManagementServer.GetProcessInstances(folio);

            // loop over the Process Instances
            // SELECT * FROM [K2].[ServerLog].[Status] where GroupName='Process'
            // 0   Error
            // 5   Deleted
            return processInstances.Cast<ProcessInstance>().Any(processInstance => processInstance.ID == processInstanceId
            && (processInstance.Status == "0" || processInstance.Status == "5"));
        }

        // in
        public bool IsProcessInstanceCompleteState(int processInstanceId, string folio)
        {
            Logger.Info($"\n\t check if K2Management::IsProcessInstanceCompleteState of processInstanceId:{processInstanceId}\n");

            var processInstances = workflowManagementServer.GetProcessInstances(folio);

            // loop over the Process Instances
            // SELECT * FROM [K2].[ServerLog].[Status] where GroupName='Process'
            // 3	Completed
            return processInstances.Cast<ProcessInstance>().Any(processInstance => processInstance.ID == processInstanceId
            && (processInstance.Status == "3"));
        }

        // in
        public bool IsProcessInstanceExist(int processInstanceId)
        {
            // Get all Process Instances
            var processInstances = workflowManagementServer.GetProcessInstances();

            // loop over the Process Instances
            return processInstances.Cast<ProcessInstance>()
                .Any(processInstance => processInstance.ID == processInstanceId);
        }

        /// <summary>
        /// Goes to activity.
        /// </summary>
        /// <param name="processInstanceId">The process instance identifier.</param>
        /// <param name="activityName">Name of the activity. in</param>
        /// <returns></returns>
        public bool GoToActivity(int processInstanceId, string activityName)
        {
            if (activityName == null)
            {
                throw new ArgumentNullException(nameof(activityName));
            }
            return this.workflowManagementServer.GotoActivity(processInstanceId, activityName);
        }

        /// <summary>
        /// Determines whether [is destination user] [the specified serial number].
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public bool IsDestinationUser(string serialNumber, string userName)
        {
            WorklistItems worklist = GetUserWorklistItems(userName);
            return worklist.Cast<WorklistItem>().Any(i => serialNumber.Equals($"{i.ProcInstID}_{i.ActInstDestID}"));
        }

        /// <summary>
        /// Gets all workflow participated users by request id.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns></returns>
        /// in
        public List<string> GetAllParticipatedUsers(Guid requestId)
        {
            List<string> tempUsers = new List<string>();

            var criteriaFilter = new WorklistCriteriaFilter();
            //Get all worklistitems that folio matches requestId parameter
            criteriaFilter.AddRegularFilter(WorklistFields.Folio, Comparison.Equals, requestId.ToString());

            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            //Setup an enumerator on the WorkListItems returned
            IEnumerator e = worklist.GetEnumerator();

            while (e.MoveNext())
            {
                tempUsers.Add(((WorklistItem)e.Current).Destination.Split(':')[1]);
            }

            return tempUsers;
        }

        /// <summary>
        /// Gets all participated users in activity.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <returns></returns>
        /// in
        public List<string> GetAllParticipatedUsersInEventName(Guid requestId, string eventName)
        {
            List<string> tempUsers = new List<string>();

            var criteriaFilter = new WorklistCriteriaFilter();
            //Get all worklistitems that folio matches requestId parameter
            criteriaFilter.AddRegularFilter(WorklistFields.Folio, Comparison.Equals, requestId.ToString());
            criteriaFilter.AddRegularFilter(WorklistFields.EventName, Comparison.Equals, eventName);
            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            //Setup an enumerator on the WorkListItems returned
            IEnumerator e = worklist.GetEnumerator();

            while (e.MoveNext())
            {
                tempUsers.Add(((WorklistItem)e.Current).Destination.Split(':')[1]);
            }

            return tempUsers;
        }

        /// <summary>
        /// Gets the user worklist items.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// in
        private WorklistItems GetUserWorklistItems(string userName)
        {
            var destinationUser = K2Client.FormatK2User(userName);
            var criteriaFilter = new WorklistCriteriaFilter();
            criteriaFilter.AddRegularFilter(WorklistFields.Destination, Comparison.Equals, destinationUser);
            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            return worklist;
        }

        /// <summary>
        /// Get All Worklist Items filterd by process names
        /// </summary>
        /// <param name="processNames">the name of processes</param>
        /// <returns></returns>
        /// in
        public K2ManagerWorklist GetAllWorklistItems(params string[] processNames)
        {
            if (processNames == null)
            {
                throw new ArgumentNullException(nameof(processNames));
            }

            var criteriaFilter = new WorklistCriteriaFilter();

            for (int i = 0; i < processNames.Length; i++)
            {
                if (i == 0)
                {
                    criteriaFilter.AddRegularFilter(WorklistFields.ProcessFullName, Comparison.Like, processNames[i]);
                }
                else
                {
                    criteriaFilter.AddRegularFilter(WorklistFields.ProcessFullName, Comparison.Like, processNames[i], RegularFilter.FilterCondition.OR);
                }
            }

            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            return worklist.GetK2ManagerWorklist();
        }

        /// <summary>
        /// Gets all worklist items by folio.
        /// </summary>
        /// <param name="folio">The folio.</param>
        /// <param name="processNames">The process names.</param>
        /// <returns></returns>
        /// <created>10/9/2018</created>
        /// <exception cref="ArgumentNullException">processNames</exception>
        /// in
        public K2ManagerWorklist GetAllWorklistItemsByFolio(string folio, params string[] processNames)
        {
            if (processNames == null)
            {
                throw new ArgumentNullException(nameof(processNames));
            }

            var criteriaFilter = new WorklistCriteriaFilter();

            for (int i = 0; i < processNames.Length; i++)
            {
                if (i == 0)
                {
                    criteriaFilter.AddRegularFilter(WorklistFields.ProcessFullName, Comparison.Like, processNames[i]);
                }
                else
                {
                    criteriaFilter.AddRegularFilter(WorklistFields.ProcessFullName, Comparison.Like, processNames[i], RegularFilter.FilterCondition.OR);
                }
            }

            criteriaFilter.AddRegularFilter(WorklistFields.Folio, Comparison.Like, folio, RegularFilter.FilterCondition.AND);

            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            return worklist.GetK2ManagerWorklist();
        }

        public K2ManagerWorklist GetWorklistItemByFolio(string folio)
        {
            var criteriaFilter = new WorklistCriteriaFilter();

            criteriaFilter.AddRegularFilter(WorklistFields.Folio, Comparison.Like, folio);

            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            return worklist.GetK2ManagerWorklist();
        }


        /// <summary>
        /// Gets the user worklist folio items.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// in
        public List<string> GetUserWorklistFolioItems(string userName)
        {
            var destinationUser = K2Client.FormatK2User(userName);
            var criteriaFilter = new WorklistCriteriaFilter();
            criteriaFilter.AddRegularFilter(WorklistFields.Destination, Comparison.Equals, destinationUser);
            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            IEnumerator e = worklist.GetEnumerator();

            List<string> folios = new List<string>();

            while (e.MoveNext())
            {
                folios.Add(((WorklistItem)e.Current).Folio);
            }

            return folios;
        }

        /// <summary>
        /// Get Users Tasks Summary 
        /// </summary>
        /// <param name="userNames">List of user names in</param>
        /// <returns> List of task summary per user</returns>   
        /// in
        public List<K2TaskSummary> GetUsersTasksSummary(List<string> userNames)
        {
            var result = new List<K2TaskSummary>();
            var criteriaFilter = new WorklistCriteriaFilter();

            foreach (var userName in userNames)
            {
                criteriaFilter.AddRegularFilter(WorklistFields.Destination, Comparison.Equals,
                    K2Client.FormatK2User(userName), RegularFilter.FilterCondition.OR);
            }

            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            IEnumerator workLists = worklist.GetEnumerator();
            while (workLists.MoveNext())
            {
                var userWorkListItem = ((WorklistItem)workLists.Current);
                result.Add(new K2TaskSummary()
                {
                    UserName = userWorkListItem.Destination.ToLower().Replace("k2:", ""),
                    ProcessName = userWorkListItem.ProcName,
                    StartDate = userWorkListItem.StartDate,
                    Folio = userWorkListItem.Folio,
                    ActivityName = userWorkListItem.ActivityName
                });
            }

            return result;
        }

        /// <summary>
        /// Get Users Tasks Summary
        /// </summary>
        /// <param name="userNames"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate">in</param>
        /// <returns></returns>        
        /// in
        public List<K2TaskSummary> GetUsersTasksSummaryByDateRange(List<string> userNames, DateTime startDate, DateTime endDate)
        {
            var result = new List<K2TaskSummary>();
            var criteriaFilter = new WorklistCriteriaFilter();

            Logger.Info("\n***** K2Management::GetUsersTasksSummaryByDateRange **** \n");
            Logger.Info($"\n^^^ from startDate: {startDate.ToString("F")}  ^^^\n");
            Logger.Info($"\n^^^ to endDate: {endDate.ToString("F")}  ^^^\n");

            foreach (var userName in userNames)
            {
                criteriaFilter.AddRegularFilter(WorklistFields.Destination, Comparison.Equals,
                    K2Client.FormatK2User(userName), RegularFilter.FilterCondition.OR);
            }

            // get all work list items by range of filters implemented as above : destination user in userNames
            var worklistItems = workflowManagementServer.GetWorklistItems(criteriaFilter);

            // foreach work list item get only items between start & end dates
            IEnumerator workLists = worklistItems.GetEnumerator();
            while (workLists.MoveNext())
            {
                var userWorkListItem = ((WorklistItem)workLists.Current);

                if (userWorkListItem.StartDate >= startDate && userWorkListItem.StartDate <= endDate)
                {
                    result.Add(
                        new K2TaskSummary
                        {
                            UserName = userWorkListItem.Destination.ToLower(),
                            ProcessName = userWorkListItem.ProcName,
                            StartDate = userWorkListItem.StartDate,
                            Folio = userWorkListItem.Folio,
                            ActivityName = userWorkListItem.EventName,
                            SN = userWorkListItem.ProcInstID + "_" + userWorkListItem.ActInstDestID
                        });
                }
            }

            return result;
        }

        /// <summary>
        /// Redirect Task
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="newUser">in</param>
        /// <returns></returns>    
        /// in
        public bool RedirectTask(K2WorklistItem task, string oldUser, string newUser)
        {
            return workflowManagementServer.RedirectWorklistItem(oldUser, newUser, task.ProcessInstanceId, int.Parse(task.SerialNumber.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1]), task.Id);
        }


        /*
         * Using the K2 Workflow Management API
         * RepairWorkflowsInErrorState
         * 
         https://help.k2.com/onlinetraining/tutorials/default.htm#Resources/Projects/WorkflowRuntimeAPI/WorkflowManagementAPI.htm%3FTocPath%3DDeveloper%2520Tutorials%7CWorkflows%7C_____4
        */
        /// <summary>
        /// Lists the state of the workflows in error.
        /// </summary>
        /// <returns></returns> 
        public ErrorLogs ListWorkflowsInErrorState()
        {
            //get the workflows in error state
            //We are using the default "All" (1) error profile to retrieve all errored workflows. 
            //but you can normally construct a set of criteria to filter the list of error workflows
            return workflowManagementServer.GetErrorLogs(1);
        }

        /// <summary>
        /// Define K2 error model which inherits from Error Log class
        /// </summary>
        public class K2ErrorLogsModel : ErrorLog
        {

        }

        /// <summary>
        /// Repairs the workflow.
        /// </summary>        
        public List<K2ErrorLogsModel> RepairWorkflowsInErrorState(List<K2ErrorLogsModel> exceptedErrorLogs, bool onlyTimeoutErros = false)
        {
            var k2ErrorLogs = ListWorkflowsInErrorState();

            Logger.Info($"\n ListWorkflowsInErrorState Count: {k2ErrorLogs.Count} \n");

            Debug.WriteLine($"\n ListWorkflowsInErrorState Count: {k2ErrorLogs.Count} \n");

            var notFixedErrors = new List<K2ErrorLogsModel>();

            foreach (ErrorLog k2Error in k2ErrorLogs)
            {
                if (exceptedErrorLogs.Any(x => x.ID == k2Error.ID)) continue;

                if (onlyTimeoutErros && !k2Error.Description.Contains("TimeoutException")) continue;

                if (onlyTimeoutErros)
                {
                    Logger.Info($"\n System.TimeoutException::: error folio: {k2Error.Folio}\n k2Error.ProcInstID: {k2Error.ProcInstID}\n k2Error.ID: {k2Error.ID}\n");
                }

                Logger.Info($"\nerror folio: {k2Error.Folio}\n k2Error.ProcInstID: {k2Error.ProcInstID}\n k2Error.ID: {k2Error.ID}\n");

                Debug.WriteLine($"\nerror folio: {k2Error.Folio}\n k2Error.ProcInstID: {k2Error.ProcInstID}\n k2Error.ID: {k2Error.ID}\n");

                //repair a workflow in error state using the Error Id
                var retryResult = workflowManagementServer.RetryError(k2Error.ProcInstID, k2Error.ID, Environment.UserName);

                // trace the result
                Logger.Info($"\n the k2 Retry Error result is: {retryResult} \n");
                Debug.WriteLine($"\n the k2 Retry Error result is: {retryResult} \n");

                if (!retryResult)
                {
                    var k2ErrorLogsModel = (K2ErrorLogsModel)k2Error;
                    notFixedErrors.Add(k2ErrorLogsModel);
                }
            }

            return notFixedErrors;
        }

        public K2ManagerWorklist GetAllWorklistItemsByFolioOnly(string folio)
        {

            var criteriaFilter = new WorklistCriteriaFilter();

            criteriaFilter.AddRegularFilter(WorklistFields.Folio, Comparison.Like, folio);

            WorklistItems worklist = this.workflowManagementServer.GetWorklistItems(criteriaFilter);
            return worklist.GetK2ManagerWorklist();
        }
        #endregion
    }
}