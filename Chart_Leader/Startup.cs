using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chart_Leader.Startup))]
namespace Chart_Leader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
