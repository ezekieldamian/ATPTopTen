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
        }
    }
}