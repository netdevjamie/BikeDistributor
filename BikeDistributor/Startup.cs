using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeDistributor.Startup))]
namespace BikeDistributor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
