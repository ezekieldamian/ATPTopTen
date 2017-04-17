using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApplication5
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "DefaultAPI",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            ////always return JSON!!!
            //configuration.Formatters.JsonFormatter
            //    .SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //remove XML as supported media type
            var appXmlType = configuration.Formatters.XmlFormatter
                .SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            configuration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            //change JSON to camel case indented (to consume with Javascript)
            var jsonSettings = configuration.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSettings.Formatting = Formatting.Indented;

        }
    }
}