using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.AwsCloudWatch;
using Serilog.Configuration;
using Serilog.Exceptions;
using Serilog.Sinks.Email;
using TruthyExtension;

namespace app_clients_searcher
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args).Build().Run();
        

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog(LoggerConfig()); 

        public static Action<WebHostBuilderContext, LoggerConfiguration> LoggerConfig() =>
            (hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()                    
                    .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
                    .WriteTo.Console()
                    .WriteTo.Logger(ActivateMonitoring(hostingContext.Configuration["ACTIVATE_MONITORING"].ToTruthy()));

        public static Action<LoggerConfiguration> ActivateMonitoring(bool activate) =>
            lc =>
                {
                    //Etapa de monitoramento
                    if(!activate) return;
                    
                    lc.MinimumLevel.Error()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console()
                    .WriteTo.Email(new EmailConnectionInfo(){
                        FromEmail = "no-reply@mycompany.com",
                        ToEmail = "rafael.miceli@hotmail.com",
                        MailServer = "localhost",
                        Port = 1025
                    });
                };
    }
}
