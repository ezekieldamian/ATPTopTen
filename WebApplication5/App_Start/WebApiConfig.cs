using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

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

            var appXmlType = configuration.Formatters.XmlFormatter
                .SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");

            configuration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}