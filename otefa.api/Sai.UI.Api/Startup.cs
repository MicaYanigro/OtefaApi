using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Otefa.UI.Api.Startup))]

namespace Otefa.UI.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}