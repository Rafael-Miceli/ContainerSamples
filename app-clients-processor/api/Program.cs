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
using EasyNetQ;
using Contracts;

namespace app_clients_processor
{
    public class Program
    {
        private static IConfigurationRoot Configuration;
        private static ClientsRepo _clientsRepo;

        public static void Main(string[] args)
        {
            InitializeConfig();

            InitializeLogs();
            Log.Information("Logs inicializados...");

            Log.Information("Inicializando App");
            InitializeApp();

            Console.Read();
        }

        private static void InitializeConfig()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        private static void InitializeLogs()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()                
                .WriteTo.Console()
                .WriteTo.Logger(lc => 
                    lc.MinimumLevel.Error()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console()
                )
            .CreateLogger();
        }

        private static void InitializeApp()
        {            
            var bus = RabbitHutch.CreateBus($"host={Configuration["RABBIT:HOST"]}");

             bus.Subscribe<ClientContract>(
                "Clients", 
                msg => {
                    Console.WriteLine("Processando cliente - " + msg.FirstName);
                    _clientsRepo.Add(new Client{Name = msg.FirstName + msg.LastName});
                },
                x => x
                .WithQueueName("ClientsToParse")
                .WithTopic("Clients")
            );
        }
    }
}
