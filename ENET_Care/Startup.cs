using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ENET_Care.Startup))]
namespace ENET_Care
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
