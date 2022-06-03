using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppDELICIAS.Startup))]
namespace WebAppDELICIAS
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
