using Microsoft.Owin;
using Owin;
using System.Data.SqlClient;

[assembly: OwinStartupAttribute(typeof(WebSite2.Startup))]
namespace WebSite2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
