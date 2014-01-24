using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KatanaGettingStarted
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class HelloWorldComponent
    {
        // Naxt component in the pipeline
        AppFunc _next;

        public HelloWorldComponent(AppFunc next)
        {
            _next = next;

        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            await _next(environment);
        }
    }
}