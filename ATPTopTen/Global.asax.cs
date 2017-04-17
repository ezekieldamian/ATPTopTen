using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace ATPTopTen
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Mapper.Initialize(x => x.AddProfile<MappingProfile>());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}