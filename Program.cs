using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DSM.UI.Api
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var configuration = new ConfigurationBuilder()
        //   .AddCommandLine(args)
        //   .Build();

        //    var host = new WebHostBuilder()
        //        .UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseConfiguration(configuration)
        //        .UseIISIntegration()
        //        .UseStartup<Startup>()
        //        .Build();

        //    host.Run();
        //}
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string localIpAddr = Core.Ops.Extensions.GetLocalIPAddress();
            IList<string> hosts = new List<string>
            {
                $"http://localhost:81",
                $"http://{localIpAddr}:81"
            };
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseIIS();
                    webBuilder.UseUrls(hosts.ToArray());
                });
        }
    }
}
