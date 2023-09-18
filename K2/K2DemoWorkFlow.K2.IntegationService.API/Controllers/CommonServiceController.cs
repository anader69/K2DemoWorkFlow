//using DSC.Application;
//using DSC.Application.Workflow;
using Framework.Identity.Data.Services;
using K2DemoWorkFlow.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DSC.K2.IntegationService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonServiceController : ControllerBase
    {
        private readonly UserAppService _userAppService;
        private readonly WorkFlowContext _dbcontext;
        //private readonly RequestAppService _requestAppService;
        //private readonly NotificationAppService _notificationservice;
        private readonly IConfiguration _configuration;

        public CommonServiceController(UserAppService userAppService,/* RequestAppService requestAppService,*/ IConfiguration configuration/*, NotificationAppService notificationservice*/, WorkFlowContext dbcontext)
        {
            _userAppService = userAppService;
            //_requestAppService = requestAppService;
            //_notificationService = notificationService;
            _configuration = configuration;
            //_notificationservice = notificationservice;
            _dbcontext = dbcontext;
        }



        #region ChangeStatus
        [HttpPost("ChangeStatus")]
        public async Task<bool> ChangeStatus(Guid requestId, string requestStatusCode, int? nextProcessActivityId)
        {
            var statusId = GetRequestStatusIdByCode(requestStatusCode);

            var baseRequest =await _dbcontext.Tasks.Include(s => s.ProcessActionTrackings).FirstOrDefaultAsync(s=>s.Id==requestId);
            if (baseRequest != null)
            {
                baseRequest.TaskStatusId = statusId;
                baseRequest.ProcessActivityId = nextProcessActivityId;
                //_dbcontext.Tasks.Update()

                var lastTakenAction = baseRequest.ProcessActionTrackings.OrderByDescending(s => s.CreatedOn).FirstOrDefault();
                lastTakenAction.TaskStatusId = statusId;
                _dbcontext.ProcessActionTrackings.Update(lastTakenAction);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #endregion

        #region Private Methods
        private int GetRequestStatusIdByCode(string requestStatusCode)
        {
            return _dbcontext.TaskStatus.AsNoTracking().First(r => r.Code == requestStatusCode).Id;
        }
        #endregion


    }

}