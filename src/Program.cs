using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace csplogger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var path = conf.GetValue<string>("LogPath");
            
            CreateHostBuilder(args, path).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string path) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .WriteTo.Console()
                    .WriteTo.File(path,
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
