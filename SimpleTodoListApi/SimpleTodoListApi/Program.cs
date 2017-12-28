using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SimpleTodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
    }
}
