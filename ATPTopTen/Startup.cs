using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ATPTopTen.Startup))]
namespace ATPTopTen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
