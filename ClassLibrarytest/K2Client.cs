// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2Client.cs" company="SURE International Technology">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ClassLibrarytest
{
    using Commons.Framework;
    using Commons.Framework.Logging;
    using SourceCode.Workflow.Client;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Action = SourceCode.Workflow.Client.Action;

    /// <summary>
    ///     K2 Workflow Client
    /// </summary>
    public class K2Client : IDisposable
    {
        #region Static Fields

        /// <summary>
        ///     The logger
        /// </summary>
        private Logger Logger;

        #endregion

        #region Fields

        /// <summary>
        ///     The K2 connection
        /// </summary>
        private readonly Connection connection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="K2Client" /> class with config values from appSettings
        /// </summary>
        public K2Client()
            : this(K2Config.GetFromConnectionStrings(), true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="K2Client"/> class.
        /// </summary>
        /// <param name="config">
        /// The configuration.
        /// </param>
        /// <param name="impersonate"></param>
        /// <exception cref="System.Exception">
        /// Can't create connection
        /// </exception>
        public K2Client(K2Config config, bool impersonate)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            try
            {
                var baseType = GetType().BaseType ?? GetType();
                Logger = new Logger(baseType);

                connection = new Connection();
                DoWithRetry(() =>
                {
                    var connectionSetup = new ConnectionSetup();
                    connectionSetup.ParseConnectionString(config.ClientConnectionString);
                    connection.Open(connectionSetup);

                    //Impersonate with the current user
                    if (impersonate)
                    {
                        try
                        {
                            var currentUser = "DSC-k2Admin";
                            //HttpContext.Current?.User?.Identity?.Name ?? "PGD-K2Admin";
                            //Logger.Info($"currentUser:{currentUser}");

                            // Note: ImpersonateUser function will append "PGD:"
                            // to current user name as a label of K2 provider, using FormatK2User helper function
                            ImpersonateUser(currentUser);
                        }
                        catch (Exception ex)
                        {
                            Logger.Fatal("Commons::K2::K2Client: Can't impersonate the current user!", ex);
                            Debug.WriteLine("Can't impersonate the current user!");
                        }

                    }

                }, TimeSpan.FromSeconds(1), 5);
            }
            catch (Exception exception)
            {
                this.Dispose();
                Logger.Fatal($"Commons::K2::K2Client(K2Config: {config}, impersonate:{impersonate}):: \nCan't connect to K2 server!\n", exception);
                throw new Exception($"Unable to connect to K2 server with connection string: {config.ClientConnectionString}", exception);
            }
        }

        public K2Client(K2Config config, bool impersonate, string userName)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            try
            {
                var baseType = GetType().BaseType ?? GetType();
                Logger = new Logger(baseType);

                connection = new Connection();
                DoWithRetry(() =>
                {
                    var connectionSetup = new ConnectionSetup();
                    using (EventLog eventLog = new EventLog("Application"))
                    {
                        eventLog.Source = "Application";
                        eventLog.WriteEntry(config.ClientConnectionString, EventLogEntryType.Information, 101, 1);
                    }
                    connectionSetup.ParseConnectionString(config.ClientConnectionString);
                    connection.Open(connectionSetup);

                    //Impersonate with the current user
                    if (impersonate)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(userName))
                            {
                                userName = "DSC-k2Admin";
                            }
                            var currentUser = userName;
                            ImpersonateUser(currentUser);
                        }
                        catch (Exception ex)
                        {
                            Logger.Fatal("Commons::K2::K2Client: Can't impersonate the current user!", ex);
                            Debug.WriteLine("Can't impersonate the current user!");
                        }

                    }

                }, TimeSpan.FromSeconds(1), 5);
            }
            catch (Exception exception)
            {
                throw;
                //this.Dispose();
                // Logger.Fatal($"Commons::K2::K2Client(K2Config: {config}, impersonate:{impersonate}):: \nCan't connect to K2 server!\n", exception);
                //throw new Exception($"Unable to connect to K2 server with connection string: {config.ClientConnectionString}", exception);
            }
        }

        /// <summary>
        /// Does the with retry.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="sleepPeriod">The sleep period.</param>
        /// <param name="tryCount">The try count.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public void DoWithRetry(System.Action action, TimeSpan sleepPeriod, int tryCount = 3)
        {
            if (tryCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(tryCount));

            while (true)
            {
                try
                {
                    action();

#if DEBUG
                    //Logger.Fatal($"\n K2Client::DoWithRetry {tryCount} sleepPeriod {sleepPeriod}\n");
                    Debug.WriteLine($"\n K2Client::DoWithRetry {tryCount} sleepPeriod {sleepPeriod}\n");
#endif
                    break; // success!
                }
                catch (Exception ex)
                {
                    if (--tryCount == 0)
                    {
#if DEBUG
                        Debug.WriteLine($"\n K2Client::DoWithRetry {tryCount} sleepPeriod {sleepPeriod}\n");
                        Debug.WriteLine("");
                        Debug.WriteLine("    ***     This is the debug veriosn of Kafala Apps    ***     ");
                        Debug.WriteLine("    ***     Sorry but there is as exception             ***     ");
                        Debug.WriteLine(ex);
                        Debug.WriteLine("    ***     the end of exception                        ***     ");
                        Debug.WriteLine("");
#endif
                        Logger.Fatal(ex);
                        throw;
                    }
                    Thread.Sleep(sleepPeriod);
                }
            }
        }
        //To be used as:
        //DoWithRetry(DoSomething, TimeSpan.FromSeconds(2), tryCount: 10);

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether this <see cref="K2Client" /> is impersonated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if impersonated; otherwise, <c>false</c>.
        /// </value>
        public bool Impersonated { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Formats the K2 user.
        /// </summary>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        /// <param name="securityLabel">
        /// The security label.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// userName
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// userName
        /// </exception>
        public static string FormatK2User(string userName, string securityLabel)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (securityLabel == null)
            {
                var serviceProviderLabel = CommonsSettings.ServiceProvider;

                securityLabel = serviceProviderLabel;
            }

            // securityLabel += ":";

            return userName.StartsWith(securityLabel) ? userName : securityLabel + userName;
        }

        /// <summary>
        /// Formats the k2 user.
        /// </summary>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        /// <returns>
        /// The K2 formatted user.
        /// </returns>
        public static string FormatK2User(string userName)
        {
            return FormatK2User(userName, null);
        }

        /// <summary>
        /// Actions the work list item.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="SourceCode.Workflow.Client.WorklistItem"/>.
        /// </returns>
        public K2WorklistItem ActionWorklistItem(string serialNumber, string action)
        {
            IEnumerable<Action> actions = this.GetActions(serialNumber).Cast<Action>();
            WorklistItem worklistItem = this.connection.OpenWorklistItem(serialNumber, "ASP", true);
            if (actions.Any(a => a.Name.Equals(action, StringComparison.InvariantCultureIgnoreCase)))
            {
                worklistItem.Actions[action].Execute();
                return worklistItem.GetK2WorklistItem();
            }

            throw new InvalidDataException(
                string.Format("Action: {0} doesn't exits in the worklist item actions", action));
        }

        /// <summary>
        /// Actions the worklist item.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="action">The action.</param>
        /// <param name="dataFields">The data fields.</param>
        /// <returns></returns>
        /// <created>7/25/2018</created>
        /// <exception cref="InvalidDataException"></exception>
        public K2WorklistItem ActionWorklistItem(string serialNumber, string action, Dictionary<string, object> dataFields = null)
        {
            IEnumerable<Action> actions = this.GetActions(serialNumber).Cast<Action>();
            WorklistItem worklistItem = this.connection.OpenWorklistItem(serialNumber, "ASP", true);
            if (actions.Any(a => a.Name.Equals(action, StringComparison.InvariantCultureIgnoreCase)))
            {
                var processInstance = this.GetProcessInstance(serialNumber);
                if (dataFields != null)
                {
                    foreach (var field in dataFields)
                    {
                        if (processInstance.DataFields.Cast<DataField>().Any(df => df.Name == field.Key))
                            processInstance.DataFields[field.Key].Value = field.Value;
                    }
                }

                processInstance.Update();
                worklistItem.Actions[action].Execute();
                return worklistItem.GetK2WorklistItem();
            }

            throw new InvalidDataException(
                string.Format("Action: {0} doesn't exits in the worklist item actions", action));
        }

        /// <summary>
        /// Actions the delegated worklist item.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="action">The action.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.InvalidDataException"></exception>
        public K2WorklistItem ActionDelegatedWorklistItem(string serialNumber, string action, string userName)
        {
            IEnumerable<Action> actions = this.GetDelegatedActions(serialNumber, userName).Cast<Action>();
            Connection delegateConnection = new Connection();
            var connectionSetup = new ConnectionSetup();
            connectionSetup.ParseConnectionString(string.Format(ConfigurationManager.ConnectionStrings["K2Client"].ConnectionString, userName));
            delegateConnection.Open(connectionSetup);
            delegateConnection.ImpersonateUser(userName);
            WorklistItem worklistItem = delegateConnection.OpenWorklistItem(serialNumber, "ASP", true);
            if (actions.Any(a => a.Name.Equals(action, StringComparison.InvariantCultureIgnoreCase)))
            {
                worklistItem.Actions[action].Execute();
                var kwl = worklistItem.GetK2WorklistItem();
                delegateConnection.Close();
                return kwl;
            }

            throw new InvalidDataException(
                string.Format("Action: {0} doesn't exits in the worklist item actions", action));
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.connection == null)
            {
                return;
            }

            this.connection.Close();
            this.connection.Dispose();

            //Logger.Info("K2 connection closed.");
        }

        /// <summary>
        /// Gets the data field value.
        /// </summary>
        /// <typeparam name="T">
        /// The type
        /// </typeparam>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <param name="dataFieldName">
        /// Name of the data field.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetDataFieldValue<T>(string serialNumber, string dataFieldName)
        {
            WorklistItem worklistItem = this.connection.OpenWorklistItem(serialNumber, "ASP", true);
            if (worklistItem != null)
            {
                DataFields dataFields = worklistItem.ProcessInstance.DataFields;
                if (dataFields.Cast<DataField>().Any(df => df.Name == dataFieldName))
                {
                    DataField dataField = dataFields[dataFieldName];
                    return (T)dataField.Value;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the data field value.
        /// </summary>
        /// <typeparam name="T">
        /// the type
        /// </typeparam>
        /// <param name="processInstanceId">
        /// The process instance identifier.
        /// </param>
        /// <param name="dataFieldName">
        /// Name of the data field.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetDataFieldValue<T>(int processInstanceId, string dataFieldName)
        {
            ProcessInstance processInstance = this.connection.OpenProcessInstance(processInstanceId);
            if (processInstance != null)
            {
                DataFields dataFields = processInstance.DataFields;
                if (dataFields.Cast<DataField>().Any(df => df.Name == dataFieldName))
                {
                    DataField dataField = dataFields[dataFieldName];
                    return (T)dataField.Value;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the data fields.
        /// </summary>
        /// <param name="processInstanceId">
        /// The process instance identifier.
        /// </param>
        /// <returns>
        /// The Dictionary
        /// </returns>
        public Dictionary<string, object> GetDataFields(int processInstanceId)
        {
            ProcessInstance processInstance = this.GetProcessInstance(processInstanceId);
            var data = new Dictionary<string, object>();
            if (processInstance == null)
            {
                return data;
            }

            DataFields dataFields = processInstance.DataFields;
            foreach (DataField dataField in dataFields)
            {
                data.Add(dataField.Name, dataField.Value);
            }

            return data;
        }

        /// <summary>
        /// Gets the data fields.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <returns>
        /// The Dictionary.
        /// </returns>
        public Dictionary<string, object> GetDataFields(string serialNumber)
        {
            if (serialNumber == null)
            {
                throw new ArgumentNullException(nameof(serialNumber));
            }

            ProcessInstance processInstance = this.GetProcessInstance(serialNumber);

            DataFields dataFields = processInstance.DataFields;
            var data = new Dictionary<string, object>(processInstance.DataFields.Count);
            foreach (DataField dataField in dataFields)
            {
                data.Add(dataField.Name, dataField.Value);
            }

            return data;
        }

        /// <summary>
        /// Gets the delegated data fields.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">serialNumber</exception>
        public Dictionary<string, object> GetDelegatedDataFields(string serialNumber, string userName)
        {
            if (serialNumber == null)
            {
                throw new ArgumentNullException(nameof(serialNumber));
            }
            var data = new Dictionary<string, object>();
            Connection delegateConnection = new Connection();
            var connectionSetup = new ConnectionSetup();
            connectionSetup.ParseConnectionString(string.Format(ConfigurationManager.ConnectionStrings["K2Client"].ConnectionString, userName));
            delegateConnection.Open(connectionSetup);
            delegateConnection.ImpersonateUser(userName);
            WorklistItem worklistItem = delegateConnection.OpenWorklistItem(serialNumber, "ASP", true);

            //ProcessInstance processInstance = null;
            if (worklistItem?.ProcessInstance != null)
            {
                DataFields dataFields = worklistItem.ProcessInstance.DataFields;

                foreach (DataField dataField in dataFields)
                {
                    data.Add(dataField.Name, dataField.Value);
                }
            }

            delegateConnection.Close();
            return data;

        }

        /// <summary>
        /// Gets the work list item.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <returns>
        /// The <see cref="WorklistItem"/>.
        /// </returns>
        public K2WorklistItem GetWorklistItem(string serialNumber)
        {
            return this.GetWorklistItem(serialNumber, true);
        }

        /// <summary>
        /// Gets the delegated worklist item.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public K2WorklistItem GetDelegatedWorklistItem(string serialNumber, string userName)
        {
            Connection delegateConnection = new Connection();
            var connectionSetup = new ConnectionSetup();
            connectionSetup.ParseConnectionString(string.Format(ConfigurationManager.ConnectionStrings["K2Client"].ConnectionString, userName));
            delegateConnection.Open(connectionSetup);
            delegateConnection.ImpersonateUser(userName);
            WorklistItem worklistItem = delegateConnection.OpenWorklistItem(serialNumber, "ASP", true);
            var wl = worklistItem.GetK2WorklistItem();
            delegateConnection.Close();
            return wl;
        }

        /// <summary>
        /// Gets the work list item.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <param name="allocate">
        /// if set to <c>true</c> [allocate].
        /// </param>
        /// <returns>
        /// The <see cref="WorklistItem"/>. 
        /// </returns>
        public K2WorklistItem GetWorklistItem(string serialNumber, bool allocate)
        {
            WorklistItem worklistItem = this.connection.OpenWorklistItem(serialNumber, "ASP", allocate);
            return worklistItem.GetK2WorklistItem();
        }

        /// <summary>
        /// Gets the work list item.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <param name="impersonateUser">
        /// The impersonate user.
        /// </param>
        /// <param name="allocate">
        /// if set to <c>true</c> [allocate].
        /// </param>
        /// <returns>
        /// The <see cref="WorklistItem"/>.
        /// </returns>
        public K2WorklistItem GetWorklistItem(string serialNumber, string impersonateUser, bool allocate)
        {
            impersonateUser = FormatK2User(impersonateUser);

            this.ImpersonateUser(impersonateUser);
            WorklistItem worklistItem = this.connection.OpenWorklistItem(serialNumber, "ASP", allocate);
            this.RevertUser();
            return worklistItem.GetK2WorklistItem();
        }

        /// <summary>
        /// Gets all work list item.
        /// </summary>
        /// <param name="processId">
        /// The process instance identifier.
        /// </param>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        /// <returns>
        /// The <see cref="Worklist"/>.
        /// </returns>
        public K2Worklist GetWorklist(int processId, string userName = null)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                this.ImpersonateUser(userName);
            }

            var worklistCriteria = new WorklistCriteria();
            worklistCriteria.AddFilterField(WCField.ProcessID, WCCompare.Equal, processId);
            Worklist worklist = this.connection.OpenWorklist(worklistCriteria);

            if (!string.IsNullOrEmpty(userName))
            {
                this.RevertUser();
            }

            return worklist.GetK2Worklist();
        }

        /// <summary>
        /// Gets the request worklist item.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public K2WorklistItem GetRequestWorklistItem(Guid requestId, string userName = null)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                this.ImpersonateUser(userName);
            }

            var worklistCriteria = new WorklistCriteria();
            worklistCriteria.AddFilterField(WCField.ProcessFolio, WCCompare.Equal, requestId);
            Worklist worklist = this.connection.OpenWorklist(worklistCriteria);

            if (!string.IsNullOrEmpty(userName))
            {
                this.RevertUser();
            }

            return worklist.GetK2Worklist().FirstOrDefault();
        }

        /// <summary>
        /// Gets the request worklist item task number.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public string GetRequestWorklistItemTaskNumber(Guid requestId, string userName = null)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                this.ImpersonateUser(userName);
            }

            var worklistCriteria = new WorklistCriteria();
            worklistCriteria.AddFilterField(WCField.ProcessFolio, WCCompare.Equal, requestId);

            K2WorklistItem task = null;

#if DEBUG
            Logger.Info("\n GetRequestWorklistItemTaskNumber start retry \n");
            Debug.WriteLine("\n GetRequestWorklistItemTaskNumber start retry \n");
#endif



            DoWithRetry(() =>
            {
                Worklist worklist = this.connection.OpenWorklist(worklistCriteria);

                if (!string.IsNullOrEmpty(userName))
                {
                    this.RevertUser();
                }

                task = worklist.GetK2Worklist().FirstOrDefault();

                if (string.IsNullOrWhiteSpace(task?.SerialNumber))
                {
                    throw new Exception(" still GetRequestWorklistItemTaskNumber::GetK2Worklist return null!, so we will retry ");
                }

            }, TimeSpan.FromSeconds(1), 20);

#if DEBUG
            Logger.Info("\n GetRequestWorklistItemTaskNumber end retry \n");
            Debug.WriteLine("\n GetRequestWorklistItemTaskNumber end retry \n");
#endif


            return task != null ? task.SerialNumber : string.Empty;
        }

        /// <summary>
        /// Gets the work list.
        /// </summary>
        /// <param name="processFullName">
        /// Full name of the process.
        /// </param>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        /// <returns>
        /// The work list.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// processFullName
        /// </exception>
        public K2Worklist GetWorklist(string processFullName, string userName)
        {
            if (processFullName == null)
            {
                throw new ArgumentNullException(nameof(processFullName));
            }

            var worklistCriteria = new WorklistCriteria();

            if (!string.IsNullOrEmpty(processFullName))
            {
                worklistCriteria.AddFilterField(WCField.ProcessFullName, WCCompare.Equal, processFullName);
            }

            Worklist worklist = this.connection.OpenWorklist(worklistCriteria);

            return worklist.GetK2Worklist();
        }

        /// <summary>
        /// Gets the work list item actions.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <returns>
        /// List of the available actions for a work list item
        /// </returns>
        public List<string> GetWorklistItemActions(string serialNumber)
        {
            Actions actions = this.GetActions(serialNumber);
            return actions.Cast<Action>().Select(a => a.Name).ToList();
        }

        /// <summary>
        /// Gets the delegated worklist item actions.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public List<string> GetDelegatedWorklistItemActions(string serialNumber, string userName)
        {
            Actions actions = this.GetDelegatedActions(serialNumber, userName);
            return actions.Cast<Action>().Select(a => a.Name).ToList();
        }

        /// <summary>
        /// Impersonates the user.
        /// </summary>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        public void ImpersonateUser(string userName)
        {
            //Logger.Info($"K2 client ImpersonateUser {userName}");

            userName = FormatK2User(userName);
            //Logger.Info($"ImpersonateUser after format will return {userName} ");

            connection.ImpersonateUser(userName);

            Impersonated = true;

            //Logger.Info($"User {userName} impersonated");
        }

        /// <summary>
        ///     Reverts the user impersonation.
        /// </summary>
        public void RevertUser()
        {
            if (!this.Impersonated)
            {
                Logger.Warn("User is not impersonated");
                return;
            }

            this.connection.RevertUser();
            this.Impersonated = false;
            Logger.Info("Impersonation reverted");
        }

        /// <summary>
        /// Starts the k2 process.
        /// </summary>
        /// <param name="processName">
        /// Name of the process.
        /// </param>
        /// <param name="folio">
        /// The folio.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int StartK2Process(string processName, object folio)
        {
            int processInstanceId = this.StartK2Process(processName, folio, null);

            return processInstanceId;
        }

        /// <summary>
        /// Starts the k2 process.
        /// </summary>
        /// <param name="processName">
        /// Name of the process.
        /// </param>
        /// <param name="folio">
        /// The folio.
        /// </param>
        /// <param name="dataFields">
        /// The data fields.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// processName
        ///     or
        ///     folio
        /// </exception>
        public int StartK2Process(string processName, object folio, Dictionary<string, object> dataFields)
        {
            if (processName == null)
            {
                throw new ArgumentNullException(nameof(processName));
            }

            if (folio == null)
            {
                throw new ArgumentNullException(nameof(folio));
            }

            try
            {
                ProcessInstance processInstance = this.connection.CreateProcessInstance(processName);
                processInstance.Folio = Convert.ToString(folio);

                if (dataFields != null)
                {
                    foreach (var field in dataFields)
                    {
                        processInstance.DataFields[field.Key].Value = field.Value;
                    }
                }

                this.connection.StartProcessInstance(processInstance);

                Logger.Info($"Process with Folio: {folio} is started with the following process instance id {processInstance.ID}");

                return processInstance.ID;
            }
            catch (Exception exception)
            {
                Logger.Fatal("Failed to start the process with Folio: " + folio, exception);
                throw;
            }
        }

        /// <summary>
        /// Updates the data fields.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="dataFields">The data fields.</param>
        public void UpdateDataFields(string serialNumber, Dictionary<string, object> dataFields)
        {
            if (!dataFields.Any())
            {
                Logger.Info("There are no data fields provided to update.");
                return;
            }

            var worklistItem = this.connection.OpenWorklistItem(serialNumber);
            var processInstance = worklistItem.ProcessInstance;
            var processDataFields = processInstance.DataFields.Cast<DataField>().ToList();
            foreach (var dataField in dataFields)
            {
                var processDataField = processDataFields.SingleOrDefault(d => d.Name == dataField.Key);
                if (processDataField != null)
                {
                    processDataField.Value = dataField.Value;
                }
            }

            processInstance.Update();
        }


        /// <summary>
        /// Updates the data fields.
        /// </summary>
        /// <param name="processInstanceId">The process instance identifier.</param>
        /// <param name="dataFields">The data fields.</param>
        /// out
        public void UpdateDataFields(int processInstanceId, Dictionary<string, object> dataFields)
        {
            if (!dataFields.Any())
            {
                Logger.Info("There are no data fields provided to update.");
                return;
            }

            var processInstance = this.connection.OpenProcessInstance(processInstanceId);
            var processDataFields = processInstance.DataFields.Cast<DataField>().ToList();
            foreach (var dataField in dataFields)
            {
                var processDataField = processDataFields.SingleOrDefault(d => d.Name == dataField.Key);
                if (processDataField != null)
                {
                    processDataField.Value = dataField.Value;
                }
            }

            processInstance.Update();
        }

        /// <summary>
        /// Gets the workList for the current logged in user for the provided process names
        /// </summary>
        /// <param name="processNames">The process names.</param>
        /// <returns>WorkList for the current logged in user.</returns>
        /// <exception cref="System.ArgumentNullException">processNames</exception>
        public K2Worklist GetWorkList(params string[] processNames)
        {
            try
            {
                if (processNames == null)
                {
                    throw new ArgumentNullException(nameof(processNames));
                }

                var workListCriteria = new WorklistCriteria()
                {
                    NoData = true
                };

                for (var i = 0; i < processNames.Length; i++)
                {
                    if (i == 0)
                    {
                        workListCriteria.AddFilterField(
                            processNames[i].Contains("\\") ? WCField.ProcessFullName : WCField.ProcessName, WCCompare.Like,
                            processNames[i]);
                    }
                    else
                    {
                        workListCriteria.AddFilterField(WCLogical.Or,
                            processNames[i].Contains("\\") ? WCField.ProcessFullName : WCField.ProcessName, WCCompare.Like,
                            processNames[i]);
                    }
                }

                var workList = this.connection.OpenWorklist(workListCriteria);
                return workList.GetK2Worklist();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets all work list items.
        /// Just for test client connection (for particular user)
        /// </summary>
        /// <param name="processNames">The process names.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">processNames</exception>
        public K2Worklist GetAllWorkListItems(params string[] processNames)
        {
            if (processNames == null)
            {
                throw new ArgumentNullException(nameof(processNames));
            }

            var worklistCriteria = new WorklistCriteria
            {
                NoData = true,
                Platform = "ASP"
            };

            //faster, does not return the data for each item
            //helps when multiple platform are used
            //Open the Worklist and get the worklistitem count
            var k2WList = connection.OpenWorklist(worklistCriteria);
            //get the number of items in the worklist
            var workItemCount = k2WList.TotalCount;

            Debug.WriteLine($"All k2 work list items count: {workItemCount}");

            //iterate over the worklist items in the worklist
            foreach (WorklistItem item in k2WList)
            {
                //do something with the worklist item
                //you can query properties/objects contained in the worklist item object
                var serialNumber = item.SerialNumber;
                var folio = item.ProcessInstance.Folio;
                var processFullName = item.ProcessInstance.FullName;
                var processName = item.ProcessInstance.Name;
                Debug.WriteLine($"WorklistItem serialNumber : {serialNumber}");
                Debug.WriteLine($"WorklistItem folio : {folio}");
                Debug.WriteLine($"WorklistItem processFullName : {processFullName}");
                Debug.WriteLine($"WorklistItem processName : {processName}");
                // Worklist worklist = this.connection.OpenWorklist(worklistCriteria);
            }

            return k2WList.GetK2Worklist();
        }

        /// <summary>
        /// Gets the delegated worklist.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="processNames">The process names.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public K2Worklist GetDelegatedWorklist(string userName, params string[] processNames)
        {
            if (processNames == null)
            {
                throw new ArgumentNullException(nameof(processNames));
            }

            var worklistCriteria = new WorklistCriteria();

            for (int i = 0; i < processNames.Length; i++)
            {
                if (i == 0)
                {
                    worklistCriteria.AddFilterField(WCField.ProcessFullName, WCCompare.Equal, processNames[i]);
                }
                else
                {
                    worklistCriteria.AddFilterField(WCLogical.Or, WCField.ProcessFullName, WCCompare.Equal, processNames[i]);
                }
            }

            Connection delegateConnection = new Connection();
            var connectionSetup = new ConnectionSetup();
            connectionSetup.ParseConnectionString(string.Format(ConfigurationManager.ConnectionStrings["K2Client"].ConnectionString, userName));
            delegateConnection.Open(connectionSetup);
            delegateConnection.ImpersonateUser(userName);
            Worklist worklist = delegateConnection.OpenWorklist(worklistCriteria);
            var wl = worklist.GetK2Worklist();
            delegateConnection.Close();
            return wl;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <returns>
        /// The actions from the work list item.
        /// </returns>
        private Actions GetActions(string serialNumber)
        {
            WorklistItem worklistItem = this.connection.OpenWorklistItem(serialNumber, "ASP", true);
            return worklistItem.Actions;
        }

        /// <summary>
        /// Gets the delegated actions.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private Actions GetDelegatedActions(string serialNumber, string userName)
        {
            Connection delegateConnection = new Connection();
            var connectionSetup = new ConnectionSetup();
            connectionSetup.ParseConnectionString(string.Format(ConfigurationManager.ConnectionStrings["K2Client"].ConnectionString, userName));
            delegateConnection.Open(connectionSetup);
            delegateConnection.ImpersonateUser(userName);
            WorklistItem worklistItem = delegateConnection.OpenWorklistItem(serialNumber, "ASP", true);
            var actions = worklistItem.Actions;
            delegateConnection.Close();
            return actions;
        }

        /// <summary>
        /// Gets the process instance.
        /// </summary>
        /// <param name="processInstanceId">
        /// The process instance identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ProcessInstance"/>.
        /// </returns>
        private ProcessInstance GetProcessInstance(int processInstanceId)
        {
            ProcessInstance processInstance = this.connection.OpenProcessInstance(processInstanceId);
            if (processInstance != null)
            {
                return processInstance;
            }

            Logger.Error("Can't find the process instance with ID " + processInstanceId);
            return null;
        }

        /// <summary>
        /// The get process instance.
        /// </summary>
        /// <param name="serialNumber">
        /// The serial number.
        /// </param>
        /// <returns>
        /// The <see cref="ProcessInstance"/>.
        /// </returns>
        private ProcessInstance GetProcessInstance(string serialNumber)
        {
            WorklistItem worklistItem = this.connection.OpenWorklistItem(serialNumber, "ASP", true);
            if (worklistItem != null)
            {
                return worklistItem.ProcessInstance;
            }

            Logger.Error("Can't find the WorkListItem with SN " + serialNumber);
            return null;
        }




        #endregion
    }
}