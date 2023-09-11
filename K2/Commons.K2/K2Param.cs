namespace Commons.K2
{
    public class K2SubmitAction
    {
        public string userName { get; set; }
        public string serialNumber { get; set; }
        public string action { get; set; }
        public Dictionary<string, object> dataFields { get; set; }

        public string ActionTypeName { get; set; }
    }

    public class K2StartProcess
    {
        public string userName { get; set; }
        public string processName { get; set; }
        public object folio { get; set; }
        public Dictionary<string, object> dataFields { get; set; }
        public string ActionTypeName { get; set; }
    }

    public class WorkListModel
    {
        public string userName { get; set; }
        public string categotyName { get; set; }
        public List<string> processNames { get; set; }
        public bool IsDelegated { get; set; }
        public string ParentUserName { get; set; }
    }

    public class K2listItem
    {
        public string userName { get; set; }
        public string serialNumber { get; set; }
        public string impersonateUser { get; set; }
        public bool allocate { get; set; } = true;
        public string ActionTypeName { get; set; }
    }
    public class K2Delegation
    {
        public K2WorklistItem Task { get; set; }
        public string OldUser { get; set; }
        public string NewUser { get; set; }
    }



}
