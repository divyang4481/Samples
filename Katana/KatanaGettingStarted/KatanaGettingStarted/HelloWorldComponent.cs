using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KatanaGettingStarted
{
    using Owin;
using System.IO;
using AppFunc = Func<IDictionary<string, object>, Task>;

    public class HelloWorldComponent
    {
        // Naxt component in the pipeline
        AppFunc _next;

        public HelloWorldComponent(AppFunc next)
        {
            _next = next;

        }

        public /*async*/ Task Invoke(IDictionary<string, object> environment)
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("Hello!!");
            }

            //await _next(environment);
        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }
}