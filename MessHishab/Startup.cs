using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MessHishab.Startup))]
namespace MessHishab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
