using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class WebApiController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Player>))]
        public IHttpActionResult GetPlayers()
        {
            var results = GetData();

            return Ok(results);
        }

        private async Task<IEnumerable<Player>> GetData()
        {
            var apiBaseAddress = ConfigurationManager.AppSettings["ServiceUrl"];
            var apiKey = ConfigurationManager.AppSettings["Key"];

            var fullUri = apiBaseAddress + "Token/Rankings" + "?token=" + apiKey;

            var client = new HttpClient();

            var response = await client.GetAsync(fullUri);

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var resultObjects = JsonConvert.DeserializeObject<IEnumerable<Player>>(jsonResult);

            return resultObjects;
        }
    }
}