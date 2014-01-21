using KatanaGettingStarted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinHost
{
    class Program
    {
        static void Main()
        {
            const int port = 5000;

            using(Microsoft.Owin.Hosting.WebApp.Start<Startup>("http://localhost:" + port))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Listening on port {0}", port);
                Console.WriteLine("Press [enter] to quit");
                Console.ResetColor();
                Console.ReadLine();
            }
        }
    }
}
