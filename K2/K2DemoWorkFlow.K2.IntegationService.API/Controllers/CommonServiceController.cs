using DSC.Application;
using DSC.Application.Workflow;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;


namespace DSC.K2.IntegationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonServiceController : ControllerBase
    {
        private readonly UserAppService _userAppService;
        private readonly RequestAppService _requestAppService;
        private readonly NotificationAppService _notificationservice;
        private readonly IConfiguration _configuration;

        public CommonServiceController(UserAppService userAppService, RequestAppService requestAppService, IConfiguration configuration, NotificationAppService notificationservice)
        {
            _userAppService = userAppService;
            _requestAppService = requestAppService;
            //_notificationService = notificationService;
            _configuration = configuration;
            _notificationservice = notificationservice;
        }

        #region Request Status
        [HttpPost("UpdateRequestStatusByStatusCode")]
        public bool UpdateRequestStatusByStatusCode(Guid baseRequestId, string requestStatusCode, int? nextProcessActivityId)
        {
            return _requestAppService.UpdateRequestStatus(baseRequestId, requestStatusCode, nextProcessActivityId);
        }

        #endregion

        #region Notifications
        [HttpPost("SendNotificationOfAssigningTask")]
        public async Task<bool> SendNotificationOfAssigningTask(Guid baseRequestId, string notificationType, string roleName, string userName)
        {
            if (notificationType.ToLower() == MailTypes.Submission.ToString().ToLower())
            {
                //get originator mail and send submission mail template
                await _notificationservice.RequestSubmitted(baseRequestId);
            }
            else if (notificationType.ToLower() == MailTypes.ReEdit.ToString().ToLower())
            {
                //get originator mail and send ReEdit mail template
                await _notificationservice.RequestReedit(baseRequestId);
            }
            else if (notificationType.ToLower() == MailTypes.Approval.ToString().ToLower())
            {
                //get originator mail and send Approval mail template
                await _notificationservice.RequestApproved(baseRequestId);
            }
            else if (notificationType.ToLower() == MailTypes.Rejection.ToString().ToLower())
            {
                //get originator mail and send Rejection mail template
                await _notificationservice.RequestRejected(baseRequestId);
            }
            else if (notificationType.ToLower() == MailTypes.Closed.ToString().ToLower())
            {
                //get originator mail and send Rejection mail template
                await _notificationservice.RequestClosed(baseRequestId, userName);
            }
            else
            {
                //get users mails by roleName mail and send new Task mail template
                await _notificationservice.NewTaskAssignd(baseRequestId, roleName);
            }
            return true;
        }

        #endregion

        #region Users and Roles
        [HttpGet]
        [Route("GetGroupUsers/{roleName}")]
        public string GetGroupUsers(string roleName)
        {
            var userNames = _userAppService.FindUsersInRoleAsync(roleName).Result;
            var domainName = _configuration.GetSection("K2Label");
            var _label = $"{domainName.Value}:";
            StringBuilder strBuilder = new StringBuilder();
            foreach (var name in userNames)
            {
                strBuilder.Append(_label + name + ';');
            }

            return strBuilder.ToString();
        }
        #endregion
    }

    public enum MailTypes
    {
        Submission,
        ReEdit,
        Approval,
        Rejection,
        Closed
    }
}