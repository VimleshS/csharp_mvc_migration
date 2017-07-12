using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_migration.Startup))]
namespace mvc_migration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
