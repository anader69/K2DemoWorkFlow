namespace Commons.K2.Proxy
{
    public enum SecurityLabel
    {
        K2,
        DSC
    }

    public enum ProcessCategory
    {
        anadertestk2= 193
    }

    public enum ProcessNames
    {
        LeaveRequestWorkFlow=1
    }

    #region Workflow Enums

    public enum WorkflowDataFields
    {
        RequestId

    }

    public enum ActionTypeEnum
    {
        BasicParam = 1,
        WithDataField = 2,
        WithImpersonation = 3
    }

    #endregion Workflow Enums

}
