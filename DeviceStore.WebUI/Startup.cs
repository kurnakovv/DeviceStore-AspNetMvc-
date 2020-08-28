using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeviceStore.WebUI.Startup))]
namespace DeviceStore.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
