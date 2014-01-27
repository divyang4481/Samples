using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(KatanaGettingStarted.Startup))]

namespace KatanaGettingStarted
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(async (environment, next) =>
            //{
            //    foreach (var pair in environment.Environment)
            //    {
            //        Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            //    }

            //    await next();
            //});

            app.Use(async (environment, next) =>
            {
                Console.WriteLine("Requesting: {0}", environment.Request.Path);
                await next();
                Console.WriteLine("Response: {0}", environment.Response.StatusCode);
            });

            ConfigureWebApi(app);

            app.UseHelloWorld();

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //app.Run(context =>
            //{
            //    context.Response.ContentType = "text/plain";
            //    return context.Response.WriteAsync("Hello world");
            //});

            //app.Run(environment =>
            //{
            //    return environment.Response.WriteAsync("Hello!!!");
            //});
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            app.UseWebApi(config);
        }
    }
}
