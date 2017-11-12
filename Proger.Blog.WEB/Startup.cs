using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proger.Blog.WEB.Startup))]
namespace Proger.Blog.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
