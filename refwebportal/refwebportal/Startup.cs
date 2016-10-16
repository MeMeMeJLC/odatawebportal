using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(refwebportal.Startup))]
namespace refwebportal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
