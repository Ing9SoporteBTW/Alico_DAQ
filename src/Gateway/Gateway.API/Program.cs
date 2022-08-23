namespace Gateway.API
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    //using Serilog;

    public class Program
    {

        public static void Main(string[] args)
        {
            //Log.Logger = CreateSerilogLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                   WebHost.CreateDefaultBuilder(args)
                    .ConfigureLogging(logging =>
                    {
                        logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Trace);
                        logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Trace);
                    })
                       .ConfigureAppConfiguration((hostingContext, config) =>
                       {
                           config
                               .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
                                   optional: true, reloadOnChange: true)
                               .AddJsonFile($"appsettings.local.json", optional: true, reloadOnChange: true)
                               .AddJsonFile("ocelot.json")
                               .AddEnvironmentVariables();
                       })
                       .UseStartup<Startup>();

        //private static Serilog.ILogger CreateSerilogLogger()
        //{
        //    return new LoggerConfiguration()
        //        .MinimumLevel.Verbose()
        //        .Enrich.WithProperty("ApplicationContext", "IdentityService")
        //        .Enrich.FromLogContext()
        //        .WriteTo.Console()
        //        .WriteTo.File(@"LogsSerilog/log_process_.log", Serilog.Events.LogEventLevel.Debug, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
        //        .CreateLogger();
        //}
    }
}
