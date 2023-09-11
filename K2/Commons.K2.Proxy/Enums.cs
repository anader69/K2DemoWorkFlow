namespace Commons.K2.Proxy
{
    public enum SecurityLabel
    {
        K2,
        DSC
    }

    public enum ProcessCategory
    {
        DSC
    }

    public enum ProcessNames
    {
        UploadStudyWF = 1,
        ChangeRequestWF = 3,
        DiscussionRequestWF = 4,
        RequestAccessWF = 2,
        ReferralRequestWF = 5,
        ResearchWF = 6,
        CompleteNeededInfoWF = 7,
        ExtensionRequestWF = 8
    }

    #region Workflow Enums

    public enum WorkflowDataFields
    {
        BaseRequestId,
        SecurityLabel,
        SubmitterUserName,
        StudyOwner,
        ApplicantIsStudyOwner
    }

    public enum ActionTypeEnum
    {
        BasicParam = 1,
        WithDataField = 2,
        WithImpersonation = 3
    }

    #endregion Workflow Enums

}
