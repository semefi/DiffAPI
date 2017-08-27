using DiffAPI;
using DiffAPI.Filters;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace DiffAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            httpConfiguration.Formatters.Add(new BrowserJsonFormatter());
            httpConfiguration.Filters.Add(new HandleExceptionFilterAttribute());
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }
}
