using System.Threading.Tasks;
using Contracts;
using data;
using EasyNetQ;
using Serilog;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace api.ApplicationService
{
    public class ClientProccessorAppService
    {
        private readonly ClientsRepo _clientsRepo;
        private readonly IBus _bus;

        public ClientProccessorAppService(ClientsRepo clientsRepo, IBus bus)
        {
            _clientsRepo = clientsRepo;
            _bus = bus;
        }

        public async Task ProccessClient() =>
            (await _clientsRepo
            .GetAll())
            .ToList()
            .ForEach(client => {
                var clientContract = client.ToClientContract();

                Log.Information($"Enviando cliente {clientContract.FirstName} - {clientContract.LastName} para fila");

                _bus.Send("ClientsToParse",
                    JsonConvert.SerializeObject(clientContract)
                );
            });
        
    }

    public static class ClientMapper
    {
        public static ClientContract ToClientContract(this Client client) =>
            new ClientContract {
                FirstName = client.FirstName,
                LastName = client.LastName
            };
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