using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DLUDeptProjectMvc.Startup))]
namespace DLUDeptProjectMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
