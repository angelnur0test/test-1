using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using RVCA_base2.Models;

[assembly: OwinStartupAttribute(typeof(RVCA_base2.Startup))]
namespace RVCA_base2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
