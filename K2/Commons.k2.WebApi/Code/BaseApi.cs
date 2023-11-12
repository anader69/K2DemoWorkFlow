using Microsoft.AspNetCore.Mvc;

namespace k2.API.Code
{
    public class BaseApi : Controller
    {
        protected readonly string DefaultConnectionStringK2Client;
        protected readonly string DefaultConnectionStringK2Management;

        IConfiguration _configuration;

        public BaseApi(IConfiguration iConfig)
        {
            _configuration = iConfig;
            DefaultConnectionStringK2Client = _configuration.GetConnectionString("K2Client");

            DefaultConnectionStringK2Management = _configuration.GetConnectionString("K2Management");
        }
    }
}