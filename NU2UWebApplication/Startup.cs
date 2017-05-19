using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NU2UWebApplication.Startup))]
namespace NU2UWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
