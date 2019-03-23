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
using Serilog.Core;
using Newtonsoft.Json;

namespace app_clients_processor
{
    public class Program
    {
        private static IConfigurationRoot Configuration;
        private static ClientsRepo _clientsRepo;

        public static async Task Main(string[] args)
        {
            Configuration = await GetInitializedConfig();
            Log.Logger = await InitializeLogs();

            Log.Information("Logs inicializados...");

            Log.Information("Inicializando App");

            // var bus = RabbitHutch.CreateBus($"host={Configuration["RABBIT:HOST"]}", services => services.Register(c => new MyConventions()));
            var bus = RabbitHutch.CreateBus($"host={Configuration["RABBIT:HOST"]}");
            bus.Advanced.Container.Resolve<IConventions>().ErrorQueueNamingConvention = (con) => "MyErrorQueueNaming";

            InitializeApp(bus);

            Console.Read();
        }

        private static async Task<IConfigurationRoot> GetInitializedConfig() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{GetAppEnvironment()}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        

        private static async Task<string> GetAppEnvironment() =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


        private static async Task<Logger> InitializeLogs() =>
             new LoggerConfiguration()
                .Enrich.FromLogContext()                
                .WriteTo.Console()
                .WriteTo.Logger(lc => 
                    lc.MinimumLevel.Error()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console()
                )
            .CreateLogger();
        

        private static void InitializeApp(IBus bus) =>
             bus.Receive<string>(
                "ClientsToParse", 
                msg => {
                    var client = JsonConvert.DeserializeObject<ClientContract>(msg).ToClient();

                    // Log.Information($"Processando cliente - {client.Name}");
                    Log.Information($"Processando cliente - {client.Name}");
                }
            );
        
    }

    public static class ClientMapper
    {
        public static Client ToClient(this ClientContract clientContract) =>
            new Client
            {
                Name = clientContract.FirstName + clientContract.LastName
            };
    }
}
