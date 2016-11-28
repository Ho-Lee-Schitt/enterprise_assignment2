using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment2MOT.Startup))]
namespace Assignment2MOT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
