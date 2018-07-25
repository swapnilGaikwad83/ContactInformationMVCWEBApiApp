using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactInformationMVC.Startup))]
namespace ContactInformationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
