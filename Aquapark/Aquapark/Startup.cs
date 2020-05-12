using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aquapark.Startup))]
namespace Aquapark
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
