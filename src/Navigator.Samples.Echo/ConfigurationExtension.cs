using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace Navigator.Samples.Echo
{
    public static class ConfigurationExtension
    {
        public static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
        
        public static ILogger LoadLogger(IConfiguration configuration)
        {
            var loggerConf = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console();
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                loggerConf.MinimumLevel.Verbose();
            }
            else
            {
                loggerConf.MinimumLevel.Debug();
            }

            return loggerConf.CreateLogger();
        }
    }
}