using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreTestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if NETCOREAPP_3 || NET5
            CreateHostBuilder(args).Build().Run();
#else
            CreateWebHostBuilder(args).Build().Run();
#endif
        }

#if NETCOREAPP_3 || NET5
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
#else
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
#endif
    }
}
