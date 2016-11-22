using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(CookBookAPI.Startup))]

namespace CookBookAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
