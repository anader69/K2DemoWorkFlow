using Commons.K2.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using K2DemoWorkFlow.Infrastructure.DTO;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Runtime;
using K2DemoWorkFlow.Application.WorkFlowServices;

namespace K2DemoWorkFlow.Infrastructure.WorkFlowImplementation
{
    public class WorkFlowRestAPI : IWorkFlowProcess
    {
        private readonly IHttpClientFactory httpClientFactory;
        private Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;
        public WorkFlowRestAPI(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
            _settings = new Lazy<Newtonsoft.Json.JsonSerializerSettings>(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                settings.DateFormatString = "dd/MM/yyyy";
                return settings;
            });
        }


        public async Task<ApiResponseOfK2Worklist> GetTasksAsync(List<string> processNamesList, string UserName)
        {
            string operationEndPoint = @"https://03k2sure.sure.com.sa/api/workflow/preview/tasks";
            var resultCall = await CallK2Api(operationEndPoint, HttpMethod.Get, "mhanna", "M@RCOhunter2110");
            var response_ = resultCall.Item1;
            var headers_ = resultCall.Item2;
            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result_ = default(ApiResponseOfK2Worklist);
            try
            {
                var MyTaskResult = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTaskAPIDTO>(responseData_, _settings.Value);
                result_ = MapTaskData(MyTaskResult);
                result_.Success = true;
                return result_;
            }
            catch (Exception exception_)
            {
                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
            }

        }

        public async Task<ApiResponse> StartWorkflowAsync(ProcessCategory processCategory, ProcessNames processName, string requestId, Dictionary<WorkflowDataFields, object> dataFields = null)
        {
            int workflowID = (int)processCategory;
            string operationEndPoint = @"https://03k2sure.sure.com.sa/api/workflow/preview/workflows/" + workflowID;
            WorkflowInstance wfInstance = new WorkflowInstance();
            wfInstance.Folio = requestId;
            wfInstance.Priority = 1;
            wfInstance.DataFields = dataFields;

            var wfInstanceJson = new StringContent(
        JsonSerializer.Serialize(wfInstance),
        Encoding.UTF8, "application/json");

            var resultCall = await CallK2Api(operationEndPoint, HttpMethod.Post, "anader", "P@ssw0rd@2023", wfInstanceJson);
            var response_ = resultCall.Item1;
            var headers_ = resultCall.Item2;

            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result_ = default(ApiResponse);
            try
            {
                result_ = new ApiResponse();
                result_.Value = responseData_;
                //Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(responseData_, _settings.Value);
                result_.Success = true;
                return result_;
            }
            catch (Exception exception_)
            {
                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
            }
        }

        public async Task<ApiResponseOfK2WorklistItem> TakeActionOnWorkflowAsync(string taskSerialNumber, string actionName, string username, Dictionary<WorkflowDataFields, object> dataFields = null)
        {
            string operationEndPoint = @"https://03k2sure.sure.com.sa/api/workflow/preview/tasks/" + taskSerialNumber + "/actions/" + actionName;
            var resultCall = await CallK2Api(operationEndPoint, HttpMethod.Post, "mhanna", "M@RCOhunter2110");
            var response_ = resultCall.Item1;
            var headers_ = resultCall.Item2;


            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result_ = default(ApiResponseOfK2WorklistItem);
            try
            {
                //  var MyTaskResult = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTaskAPIDTO>(responseData_, _settings.Value);
                // result_ = MapTaskData(MyTaskResult);
                result_.Success = true;
                return result_;
            }
            catch (Exception exception_)
            {
                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
            }


        }
        #region helper

        private ApiResponseOfK2Worklist MapTaskData(MyTaskAPIDTO data)
        {
            ApiResponseOfK2Worklist model = new ApiResponseOfK2Worklist();
            model.Value = new List<K2WorklistItem>();
            model.Success = true;

            foreach (var worklistItem in data.K2Tasks)
            {
                model.Value.Add(new K2WorklistItem()
                {
                    Id = (int)worklistItem.WorkflowID,
                    ProcessFullName = worklistItem.WorkflowDisplayName,
                    Folio = worklistItem.WorkflowInstanceFolio,
                    ActivityName = worklistItem.ActivityName,
                    Url = worklistItem.ViewFlowURL,
                    OriginatorName = worklistItem.Originator.Username,
                    SerialNumber = worklistItem.SerialNumber,
                    CreatedOn = DateTime.Parse(worklistItem.TaskStartDate),
                    AllocatedUser = "",
                    EventName = worklistItem.EventName,
                    ProcessInstanceId = (int)worklistItem.WorkflowInstanceID,
                    TaskDate = DateTime.Parse(worklistItem.TaskStartDate),
                    UserName = worklistItem.Originator.DisplayName,
                });
            }
            return model;
        }


        private async Task<(HttpResponseMessage, Dictionary<string, IEnumerable<string>>)> CallK2Api(string operationEndPoint, HttpMethod type, string username, string password, object Content = null)
        {
            var httpClient = httpClientFactory.CreateClient();
            HttpRequestMessage request = new HttpRequestMessage(type, operationEndPoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password)));
            if (Content != null)
            {
                request.Content = (HttpContent)Content;

            }
            var response_ = await httpClient.SendAsync(request);
            var headers_ = response_.Headers.ToDictionary(h_ => h_.Key, h_ => h_.Value);
            if (response_.Content != null && response_.Content.Headers != null)
            {
                foreach (var item_ in response_.Content.Headers)
                    headers_[item_.Key] = item_.Value;
            }
            var status_ = ((int)response_.StatusCode).ToString();
            if (status_ == "200" || status_ == "204")
            {
                return (response_, headers_);
            }
            else
            {
                var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
            }
        }

        public Task<ApiResponseOfK2Worklist> getUserHistory(string username)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
