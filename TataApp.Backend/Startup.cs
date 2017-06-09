using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TataApp.Backend.Startup))]
namespace TataApp.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
