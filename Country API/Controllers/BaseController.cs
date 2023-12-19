using Microsoft.AspNetCore.Mvc;

namespace Country_API.Controllers
{
    public class BaseController : Controller
    {
        public HttpClient httpClient;
        public IConfiguration _config { get; }
        public BaseController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            httpClient = httpClientFactory.CreateClient("Advait");           
        }
    }
}
