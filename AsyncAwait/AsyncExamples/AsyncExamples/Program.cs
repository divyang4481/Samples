using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExamples
{
    class Program
    {
        static void Main()
        {
            MainAsync().Wait();
        }
        
        static async Task MainAsync()
        {
            try
            {
                //LookupHostName();
                //DumpWebPageAsync("http://www.google.pl");
                //TwoDownloads();
                var sampleClass = new SampleClass();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("End");
        }

        // If exception occures in first call, then it is propagated and second method is never called
        private static async void TwoDownloads()
        {
            var webClient1 = new WebClient();
            var webClient2 = new WebClient();

            Task<string> firstTask = webClient1.DownloadStringTaskAsync("http://oreilly.com");
            Task<string> secondTask = webClient2.DownloadStringTaskAsync("http://simpletalk.com");

            Thread.Sleep(10000);

            string firstPage = await firstTask;
            string secondPage = await secondTask;
        }

        // async method
        // void means "fire and forget" - for event-like operations
        private static async void DumpWebPageAsync(string uri)
        {
            var webClient = new WebClient();
            string page = await webClient.DownloadStringTaskAsync(uri);

            //var webClient = new WebClient();
            //Task<string> myTask = webClient.DownloadStringTaskAsync("http://www.google.pl");
            //// Do something here
            //string page = await myTask;

            Console.WriteLine(page);
        }

        // Using Task without async
        // Starting an operation returns a Task.
        // Use .ContinueWith to register a callback
        private static void LookupHostName()
        {
            Task<IPAddress[]> ipAddressesPromise = Dns.GetHostAddressesAsync("oreilly.com");
            ipAddressesPromise.ContinueWith(_ =>
                {
                    IPAddress[] ipAddresses = ipAddressesPromise.Result;
                    
                    foreach (var ipAddress in ipAddresses)
                    {
                        Console.WriteLine(ipAddress.ToString());
                    }
                });
        }
    }
}
