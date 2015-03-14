using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DLUProjectMvc.Startup))]
namespace DLUProjectMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
