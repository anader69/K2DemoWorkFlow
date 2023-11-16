
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Client.Filters;
using System;

namespace ClassLibrary1
{
    public class k2History
    {

        public void gethistory(string connection)
        {
            // string _connectionstring = connection;
           SCConnectionStringBuilder hostServerConnectionString = new SCConnectionStringBuilder();
            hostServerConnectionString.Host = "10.2.2.164"; //name of the K2 host server, or the name of the DNS entry pointing to the K2 Farm
            hostServerConnectionString.Port = 5252; //use port 5555 for all non-workflow client connections
            hostServerConnectionString.IsPrimaryLogin = true; //true = re-authenticate user, false = use cached security credentials
            hostServerConnectionString.Integrated = false;
            hostServerConnectionString.UserID = "SURE\\mhanna";
            hostServerConnectionString.Password = "M@RCOhunter2110";
            hostServerConnectionString.WindowsDomain = "SURE";
            hostServerConnectionString.Authenticate = true;
            hostServerConnectionString.SecurityLabelName = "K2";//true = use the logged on user, false = use the specified user
                                                                //Open the connection to K2
            SourceCode.SmartObjects.Client.SmartObjectClientServer soServer = new SmartObjectClientServer();
            soServer.CreateConnection();
            soServer.Connection.Open(hostServerConnectionString.ToString());


            // open a K2 Server connection
            SmartObjectClientServer serverName = new SmartObjectClientServer();
            serverName.CreateConnection();
            serverName.Connection.Open(hostServerConnectionString.ToString());
            SmartObject smartObject = serverName.GetSmartObject("com_K2_System_Reports_SmartObject_AnalyticsActivityInstanceDestination");

            // specify which method will be called. Here we call the GetList method
            SmartListMethod getList = smartObject.ListMethods["List"];
            smartObject.MethodToExecute = getList.Name;
            Equals firstFilter = new Equals();
            firstFilter.Left = new PropertyExpression("ProcessFolder", PropertyType.Text);
            firstFilter.Right = new ValueExpression("anadertestk2", PropertyType.Text);
            //And and1 = new And();
            //and1.Left = firstFilter;
            getList.Filter = firstFilter;

            SmartObjectList smoList = serverName.ExecuteList(smartObject);

            /* read the return properties of the SmartObject where the FirstName is "K2" and the
        LastName contains "S" and then write each of them and their email address to the console */
            foreach (SmartObject smo in smoList.SmartObjectsList)
            {
                Console.WriteLine("First Name: " + smo.Properties["FirstName"].Value);
                Console.WriteLine("Last Name: " + smo.Properties["LastName"].Value);
                Console.WriteLine("Email: " + smo.Properties["Email"].Value);
            }
        }

    }
}
