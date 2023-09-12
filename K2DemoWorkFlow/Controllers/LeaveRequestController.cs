using Commons.K2.Proxy;
using Framework.Core;
using Framework.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using ZXing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace K2DemoWorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly K2Proxy _k2Proxy;
        public LeaveRequestController(K2Proxy k2Proxy)
        {
            _k2Proxy=k2Proxy;
        }
        // GET: api/<LeaveRequest>
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var id = Guid.NewGuid();
            var result = new ReturnResult<int>();
            var   dataFields = new Dictionary<WorkflowDataFields, object>();
            dataFields.Add(WorkflowDataFields.RequestId,id);
            var wfResult = await _k2Proxy.StartWorkflowAsync(ProcessCategory.anadertestk2, ProcessNames.LeaveRequestWorkFlow, id.ToString(), dataFields);
            if (wfResult.Success != null && !wfResult.Success.Value)
            {
                result.AddErrorItem(String.Empty, "error");
                return Ok(result);
            }

            result.Value = wfResult.Value.To<int>();
            return Ok(result);
        }

        // GET api/<LeaveRequest>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LeaveRequest>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LeaveRequest>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LeaveRequest>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
