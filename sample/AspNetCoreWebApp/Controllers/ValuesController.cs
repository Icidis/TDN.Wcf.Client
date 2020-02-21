using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TDN.Wcf.Client.Abstractions;
using WCFServiceContracts;

namespace AspNetCoreWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IWcfClientFactory _wcfClientFactory;

        public ValuesController(ILogger<ValuesController> logger, IWcfClientFactory wcfClientFactory)
        {
            _logger = logger;
            _wcfClientFactory = wcfClientFactory;
        }

        [HttpGet]
        public CompositeType Get()
        {
            var client = _wcfClientFactory.CreateClient<IService1>("BindingBasicHttp", "http://localhost:51677/Service1.svc");

            return client.GetDataUsingDataContract(new CompositeType()
            {
                BoolValue = true,
                StringValue = "Test from WCF"
            });
        }
    }
}
