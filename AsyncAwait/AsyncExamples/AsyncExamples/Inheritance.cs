using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExamples
{
    class BaseClass
    {
        public virtual async Task<int> GetPageSize()
        {
            var webClient = new WebClient();

            Task<string> task = webClient.DownloadStringTaskAsync("http://www.google.pl");

            // ...

            return (await task).Length;
        }
    }

    class SubClass : BaseClass
    {
        public override Task<int> GetPageSize()
        {
            return new Task<int>(() => 2);
        }
    }
}
