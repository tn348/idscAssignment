using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChartFromCsv.Startup))]
namespace ChartFromCsv
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
