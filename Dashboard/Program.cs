using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    class Program
    {
        static void Main(string[] args)
        {
            var nancyHost = new NancyHost(new Uri("http://localhost:8888/nancy/"), new Uri("http://127.0.0.1:8888/nancy/"));
            nancyHost.Start();

            Console.WriteLine("Nancy now listening - navigating to http://localhost:8888/nancy/. Press enter to stop");
           
            Console.ReadKey();

            nancyHost.Stop();

            Console.WriteLine("Stopped. Good bye!");
        }
    }

    public class TestModule : NancyModule
    {
        public TestModule()
        {
            Get["/"] = parameters =>
            {
                return View["staticview", Request.Url];
            };

            Get["/testing"] = parameters =>
            {
                return View["staticview", Request.Url];
            };
        }
    }
}
