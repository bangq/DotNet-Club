using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Club.Startup))]
namespace Club
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
