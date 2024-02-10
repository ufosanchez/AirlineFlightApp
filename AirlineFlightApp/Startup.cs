using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AirlineFlightApp.Startup))]
namespace AirlineFlightApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
