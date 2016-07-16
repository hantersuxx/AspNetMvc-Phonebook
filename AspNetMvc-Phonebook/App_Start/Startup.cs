using Microsoft.Owin;
using Owin;
using AspNetMvc_Phonebook.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(AspNetMvc_Phonebook.Startup))]

namespace AspNetMvc_Phonebook
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //configurable context and manager
            app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}