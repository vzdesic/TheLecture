using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Enciklopedija.Startup))]
namespace Enciklopedija
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
