using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using data;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace app_clients_searcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public ClientsRepo _clientsRepo { get; }
        public JobController(ClientsRepo clientsRepo) =>
            _clientsRepo = clientsRepo;
        

        // POST api/values
        [HttpPost]
        [Route("begin-clients-proccess")]
        public async Task Post()
        {
            var allClients = await _clientsRepo.GetAll();

            var bus = RabbitHutch.CreateBus($"host={RuntimeConfig.RabbitHost}");

            foreach (var client in allClients)
            {
                var clientContract = new ClientContract
                {
                    FirstName = client.FirstName,
                    LastName = client.LastName
                };

                Log.Information($"Enviando cliente {clientContract.FirstName} - {clientContract.LastName} para fila");
                
                bus.Publish(clientContract,
                    c => c
                    .WithQueueName("ClientsToParse")
                    .WithTopic("Clients")
                );
            }
        }
        
    }    
}

namespace Contracts
{
    public class ClientContract
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }    
}
