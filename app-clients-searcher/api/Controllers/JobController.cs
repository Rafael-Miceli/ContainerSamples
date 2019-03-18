using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ApplicationService;
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
        private readonly ClientProccessorAppService _clientProccessorAppService;

        public JobController(ClientProccessorAppService clientProccessorAppService) =>
            _clientProccessorAppService = clientProccessorAppService;
    
        
        // POST api/values
        [HttpPost]
        [Route("begin-clients-proccess")]
        public async Task Post() =>
            await _clientProccessorAppService.ProccessClient();
        
    }    
}
