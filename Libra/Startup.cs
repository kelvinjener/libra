using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Libra.Startup))]
namespace Libra
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
