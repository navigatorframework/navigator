using System;
using System.IO;
using Ele.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using ConfigurationExtensions = Ele.Extensions.Configuration.ConfigurationExtensions;

namespace Navigator.Samples.Echo
{
    public class Program
    {
        private static IConfiguration Configuration { get; } = ConfigurationExtensions.LoadConfiguration(Directory.GetCurrentDirectory());

        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); 
                    webBuilder.UseSerilog();
                });
        
        public static void Main(string[] args)
        {
            Log.Logger = LoggingExtensions.LoadLogger(Configuration);

            try
            {
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting WebApi");

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WebApi terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}