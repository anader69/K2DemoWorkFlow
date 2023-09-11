using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Domain.Enum
{



    #region Workflow

    public enum RequestStatusEnum
    {
        Draft = 1,
        Submitted = 2,
        Rejected = 3,
        Approved = 4,
        ReEdit = 5,

    }

    public enum ProcessEnum
    {
        UploadStudyWF = 1,
        RequestAccessWF = 2,
        ChangeRequestWF = 3,
        DiscussionRequestWF = 4,
        ReferralRequestWF = 5,
        ResearchWF = 6,
        CompleteNeededInfoWF = 7,
        ExtensionRequestWF = 8
    }

    public enum ProcessActivityEnum
    {
        UploadStudyWF_StudyOwnerReview = 1,
        RequetsAccessWF_DirectorReview = 8,
        DiscussionRequestWF_Viewd = 15
    }

    public enum ProcessActivityWFStatusEnum
    {
        UploadStudyWF_Request_Submitted = 9,
        UploadStudyWF_UnderReviewStudyOwner = 3,

        RequetsAccessWF_Request_Submitted = 15,
        DiscussionRequestWF_RequestSubmitted = 24,
        DiscussionRequestWF_Pending = 22

    }


    public enum UploadStudyWFActions
    {
        RequestSent = 13,
        ResearcherReEditResend = 12,
        StudyOwnerReEditResend = 11,
        DirectorApprove = 9,
    }

    public enum RequestAccessWFActions
    {
        RequestSubmitted = 24,
        VPApproved = 16,
        UnderReviewDirector = 10,
        DirectorReviewReject = 15
    }

    public enum DiscussionRequestWFActions
    {
        RequestSubmitted = 25
    }

    public enum ChangeRequestWFActions
    {
        ChangeRequestWF_RequestSent = 19,
        ChangeRequestWF_StudyOwnerSendChanges = 20,
        ChangeRequestWF_KMManagerApprove = 21,
        ChangeRequestWF_KMManagerReject = 22,
        ChangeRequestWF_KMManagerClose = 23
    }

    public enum ChangeRequestWFStatusEnum
    {
        Request_Submitted = 16,
        UnderChangeStudyOwner = 17,
        Approved = 19,
        Closed = 21
    }
    public enum ChangeRequestWFActivities
    {
        ChangeRequestWF_StudyOwnerProvideInfo = 12
    }


    #endregion

    #region App
    public enum LevelOfSecrecyEnum
    {
        Secret = 1,
        TopSecret = 2,
        ExtremelySecret = 3,
        SharingNotPermitted = 4,
    }

    #endregion

}


