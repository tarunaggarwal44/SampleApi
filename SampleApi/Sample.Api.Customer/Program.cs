using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sample.Api.Common.Logging;
using Serilog;
using Serilog.Events;
using System;
using Microsoft.Extensions.Logging;
using Serilog.Formatting.Json;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Sample.Api.Customer
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
          .AddEnvironmentVariables()
          .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration)
              .WriteTo.File(new JsonFormatter(), "log.txt", shared: true)
              .CreateLogger();

          //  Log.Logger = new LoggerConfiguration()
          //.MinimumLevel.Debug()
          //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
          //.Enrich.WithProperty("Version", "1.0.0")
          //.Enrich.With(new LogEnricher())
          //.Enrich.FromLogContext()
          //.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day,
          // outputTemplate: "{Timestamp:HH:mm} ({Version}) [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
          //.CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
                
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddSerilog();
                });

    }
}
