using Country_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Country_API.Controllers
{
    [Authorize]
    [ApiController]
    public class CountriesAPI : BaseController
    {
        private readonly IConfiguration _config;
        public CountriesAPI(IConfiguration _config, System.Net.Http.IHttpClientFactory httpClientFactory) : base(_config, httpClientFactory)
        {
            this._config = _config;
        }
        [HttpGet]
        [Route("api/ByName/{name}")]
        public ActionResult<List<ServiceResponse<Class1>>> CountryByName(string name)
        {
            var serviceResponse = new ServiceResponse<List<Class1>>();
            try
            {
                using (var http = httpClient)
                {
                    var response = http.GetAsync($"name/{name}").Result;
                    var result = JsonConvert.DeserializeObject<List<Class1>>(response.Content.ReadAsStringAsync().Result);
                    serviceResponse.Data = result;
                }
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return Ok(serviceResponse);
        }

        [HttpGet]
        [Route("api/byFilters")]
        public ActionResult<List<ServiceResponse<Class1>>> CountryByAllFilters(int? population, float? area, string? language, int Pagination = 100, int sort = 0)
        {
            var serviceResponse = new ServiceResponse<List<Class1>>();
            try
            {
                using (var http = httpClient)
                {
                    HttpResponseMessage? response = new HttpResponseMessage();
                    response = http.GetAsync(language != null ? $"lang/{language}" : $"all").Result;
                    var result = JsonConvert.DeserializeObject<List<Class1>>(response.Content.ReadAsStringAsync().Result);
                    if (result != null)
                    {
                        serviceResponse.Data = result;
                        if (population != null)
                            serviceResponse.Data = serviceResponse.Data.Where(x => x.population == population).ToList();
                        if (area != null)
                            serviceResponse.Data = serviceResponse.Data.Where(x => x.area == area).ToList();
                        if (sort == 0)
                            serviceResponse.Data = serviceResponse.Data.OrderBy(x => x.name.common).ToList();
                        else if (sort == 1)
                            serviceResponse.Data = serviceResponse.Data.OrderByDescending(x => x.name.common).ToList();
                        serviceResponse.Data = serviceResponse.Data.Take(Pagination).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return Ok(serviceResponse);
        }
    }
}
