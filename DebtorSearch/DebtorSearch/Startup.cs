using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DebtorSearch.Startup))]
namespace DebtorSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
